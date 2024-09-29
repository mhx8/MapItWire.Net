using MapItWire.Net.Stores;
using MapItWire.Net.Utils;

namespace MapItWire.Net.Rest;

internal class RestHttpMessageHandler(IRequestIdentifierStore store)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        HttpResponseMessage responseMessage = await base.SendAsync(
            request,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(store.RequestIdentifier))
        {
            return responseMessage;
        }

        MapItWireMapping mapping = await CreateMappingAsync(
            request,
            responseMessage);
        MappingUtils.WriteMappingToFile(
            mapping,
            store.RequestIdentifier!);

        return responseMessage;
    }

    private static async Task<MapItWireMapping> CreateMappingAsync(
        HttpRequestMessage request,
        HttpResponseMessage response)
        => new()
        {
            Request = new WireMockRequest
            {
                Methods = [request.Method.Method],
                Path = Uri.UnescapeDataString(request.RequestUri?.AbsolutePath ?? string.Empty)
            },
            Response = new WireMockResponse
            {
                Status = (int)response.StatusCode,
                Body = await response.Content.ReadAsStringAsync(),
            }
        };
}

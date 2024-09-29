using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using MapItWire.Net.Stores;
using MapItWire.Net.Utils;

namespace MapItWire.Net.Soap;

internal class SoapMessageInspector(
    IRequestIdentifierStore store) : IClientMessageInspector
{
    [SuppressMessage(
        "Minor Code Smell",
        "S1075:URIs should not be hardcoded",
        Justification = "Only for integration testing purpose")]
    private const string Url = "http://localhost:8080/";

    private const string PostMethod = "POST";
    private const string SoapActionHeader = "SOAPAction";
    private const string ExactMatcher = "ExactMatcher";
    private const string ContentType = "text/xml";

    public void AfterReceiveReply(
        ref Message reply,
        object correlationState)
    {
        ArgumentNullException.ThrowIfNull(reply);
        if (string.IsNullOrWhiteSpace(store.RequestIdentifier))
        {
            return;
        }

        string? action = correlationState as string;
        MessageBuffer buffer = reply.CreateBufferedCopy(int.MaxValue);
        Message copy = buffer.CreateMessage();
        reply = buffer.CreateMessage();

        MapItWireMapping mapping = CreateMapping(
            action,
            copy);
        MappingUtils.WriteMappingToFile(
            mapping,
            store.RequestIdentifier);
    }

    private static MapItWireMapping CreateMapping(
        string? action,
        Message copy)
        => new()
        {
            Request = new WireMockRequest
            {
                Methods = [PostMethod],
                Url = Url,
                Headers = new List<WireMockHeader>
                {
                    new()
                    {
                        Name = SoapActionHeader,
                        IgnoreCase = true,
                        Matchers =
                        [
                            new WireMockMatcher
                            {
                                Name = ExactMatcher,
                                IgnoreCase = true,
                                Pattern = $"\"{action}\"",
                            }
                        ]
                    }
                }
            },
            Response = new WireMockResponse
            {
                Body = ExtractResponseBody(copy),
                Status = (int)HttpStatusCode.OK,
                Headers = new WireMockResponseHeader { ContentType = ContentType }
            }
        };

    public object? BeforeSendRequest(
        ref Message request,
        IClientChannel channel)
    {
        return request?.Headers?.Action;
    }

    private static string ExtractResponseBody(
        Message message)
    {
        if (message.IsEmpty)
        {
            return string.Empty;
        }

        using MemoryStream ms = new();
        using XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(
            ms,
            Encoding.UTF8);
        message.WriteMessage(writer);
        writer.Flush();
        ms.Position = 0;
        using StreamReader reader = new(ms);
        return reader.ReadToEnd();
    }
}
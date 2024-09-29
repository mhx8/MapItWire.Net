using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace MapItWire.Net;

internal static class MapItWireServer
{
    private const int WireMockServerDefaultPort = 8080;
    private const int LowPriority = 100;

    private static WireMockServer _server = null!;

    internal static WireMockServer Start()
    {
        _server = WireMockServer.Start(new WireMockServerSettings
        {
            Port = WireMockServerDefaultPort
        });
        AddMatchAnyPathMapping();
        return _server;
    }

    internal static WireMockServer GetServer() => _server;

    internal static void ResetMappings()
    {
        _server.ResetMappings();
        AddMatchAnyPathMapping();
    }

    private static void AddMatchAnyPathMapping()
        => _server.Given(
                Request.Create()
                    .WithPath("/*")
                    .UsingAnyMethod())
            .AtPriority(LowPriority)
            .RespondWith(
                Response.Create().WithSuccess());
}
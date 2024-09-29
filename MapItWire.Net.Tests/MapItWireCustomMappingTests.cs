using MapItWire.Net.Attributes;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MapItWire.Net.Tests;

[Collection("Serial")]
public class MapItWireCustomMappingTests(MapItWireFixture fixture) : IClassFixture<MapItWireFixture>
{
    private readonly WireMockServer _server = fixture.GetServer();

    [Fact]
    [MapItWireMapping("IsAlive")]
    public async Task Verify_IsAliveWithCustomMapping()
    {
        // Arrange
        _server
            .Given(
                Request.Create()
                    .WithPath("/ping")
                    .UsingGet())
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithBody("Hello Ping"));

        HttpClient client = new();

        // Act
        HttpResponseMessage aliveResponseMessage = await client.GetAsync("http://localhost:8080/status/alive");
        string aliveResult = await aliveResponseMessage.Content.ReadAsStringAsync();

        HttpResponseMessage pingResponseMessage = await client.GetAsync("http://localhost:8080/ping");
        string pingResult = await pingResponseMessage.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(
            "Hello World",
            aliveResult);
        Assert.Equal(
            "Hello Ping",
            pingResult);
    }
}

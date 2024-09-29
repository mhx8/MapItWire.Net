using System.Diagnostics.CodeAnalysis;
using MapItWire.Net.Attributes;

namespace MapItWire.Net.Tests;

[Collection("Serial")]
[SuppressMessage(
    "Usage",
    "xUnit1033",
    Justification = "Server is started by the fixture.")]
public class MapItWireMappingTests : IClassFixture<MapItWireFixture>
{
    [Fact]
    [MapItWireMapping("IsAlive")]
    public async Task Verify_IsAlive()
    {
        // Arrange
        HttpClient client = new();

        // Act
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:8080/status/alive");
        string result = await responseMessage.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(
            "Hello World",
            result);
    }

    [Fact]
    [MapItWireMapping("StatusPing")]
    public async Task Verify_StatusPing()
    {
        // Arrange
        HttpClient client = new();

        // Act
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:8080/status/ping");
        string result = await responseMessage.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(
            "Hello Ping",
            result);
    }

    [Fact]
    public async Task Verify_AnyRequestReturnsOk()
    {
        // Arrange
        HttpClient client = new();

        // Act
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:8080/any/request");

        // Assert
        Assert.True(responseMessage.IsSuccessStatusCode);
    }
}

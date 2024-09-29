using MapItWire.Net.Rest;
using MapItWire.Net.Stores;
using MapItWire.Net.Utils;
using Moq;
using Newtonsoft.Json;
using WireMock.Server;
using WireMock.Settings;

namespace MapItWire.Net.Tests.Rest;

[Collection("Serial")]
public class RestHttpMessageHandlerTests
{
    private readonly Mock<IRequestIdentifierStore> _requestIdentifierStoreMock = new();

    public RestHttpMessageHandlerTests()
    {
        _requestIdentifierStoreMock
            .SetupGet(
                mock =>
                    mock.RequestIdentifier)
            .Returns("MyRestTestRequest");
    }

    [Fact]
    public async Task Verify_RestHttpMessageHandler()
    {
        // Arrange
        WireMockServer server = WireMockServer.Start(
            new WireMockServerSettings
            {
                Port = 8080,
            });
        server.ReadStaticMappings(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data"));

        HttpClientHandler handler = new();
        RestHttpMessageHandler wireMockHandler = new(_requestIdentifierStoreMock.Object) { InnerHandler = handler };
        HttpClient client = new(wireMockHandler);

        // Act
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:8080/unit/test");

        // Assert
        Assert.True(responseMessage.IsSuccessStatusCode);

        string staticMappingFolder = FileUtils.GetRequestFolderPath("MyRestTestRequest");

        string staticMappingFilePath = Path.Combine(
            staticMappingFolder,
            "mappings.json");

        Assert.True(File.Exists(staticMappingFilePath));

        // Cleanup
        Directory.Delete(
            staticMappingFolder,
            true);
        server.Stop();
    }

    [Fact]
    public async Task Verify_RestHttpMessageHandler_WithScenario()
    {
        // Arrange
        WireMockServer server = WireMockServer.Start(
            new WireMockServerSettings
            {
                Port = 8080,
            });
        server.ReadStaticMappings(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data"));

        HttpClientHandler handler = new();
        RestHttpMessageHandler wireMockHandler = new(_requestIdentifierStoreMock.Object) { InnerHandler = handler };
        HttpClient client = new(wireMockHandler);

        // Act
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:8080/unit/test");
        HttpResponseMessage responseMessage2 = await client.GetAsync("http://localhost:8080/unit/test");

        // Assert
        Assert.True(responseMessage.IsSuccessStatusCode);
        Assert.True(responseMessage2.IsSuccessStatusCode);

        string staticMappingFolder = FileUtils.GetRequestFolderPath("MyRestTestRequest");
        string staticMappingFilePath = Path.Combine(
            staticMappingFolder,
            "mappings.json");

        Assert.True(File.Exists(staticMappingFilePath));
        List<MapItWireMapping>? mappings =
            JsonConvert.DeserializeObject<List<MapItWireMapping>>(await File.ReadAllTextAsync(staticMappingFilePath));
        
        Assert.NotNull(mappings);
        Assert.Equal(2, mappings.Count);
        Assert.False(string.IsNullOrWhiteSpace(mappings[0].Scenario));

        // Cleanup
        Directory.Delete(
            staticMappingFolder,
            true);
        server.Stop();
    }
}

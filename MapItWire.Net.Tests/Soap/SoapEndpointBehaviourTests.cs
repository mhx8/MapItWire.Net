using System.ServiceModel;
using MapItWire.Net.Soap;
using MapItWire.Net.Stores;
using MapItWire.Net.Tests.SoapResponder;
using MapItWire.Net.Utils;
using Moq;
using WireMock.Server;
using WireMock.Settings;

namespace MapItWire.Net.Tests.Soap;

[Collection("Serial")]
public class SoapEndpointBehaviourTests
{
    private readonly Mock<IRequestIdentifierStore> _requestIdentifierStoreMock = new();

    public SoapEndpointBehaviourTests()
    {
        _requestIdentifierStoreMock
            .SetupGet(
                mock =>
                    mock.RequestIdentifier)
            .Returns("MySoapTestRequest");
    }

    [Fact]
    public async Task Verify_SoapMessageBehavior()
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

        SoapResponderPortTypeClient client = new(
            new BasicHttpBinding(),
            new EndpointAddress("http://localhost:8080/"));

        SoapMessageBehavior behavior = new(new SoapMessageInspector(_requestIdentifierStoreMock.Object));
        client.Endpoint.EndpointBehaviors.Add(behavior);

        // Act
        Method1Response? result = await client.Method1Async("Param1", "Param2");

        // Assert
        Assert.NotNull(result);
        string staticMappingFolder = FileUtils.GetRequestFolderPath("MySoapTestRequest");
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
}
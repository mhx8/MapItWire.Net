using System.Diagnostics.CodeAnalysis;
using MapItWire.Net.Attributes;

namespace MapItWire.Net.Tests;

[Collection("Serial")]
[SuppressMessage(
    "Usage",
    "xUnit1003:Theory methods must have test data",
    Justification = "Test data is provided by the MapItWireRequestData attribute.")]
[SuppressMessage(
    "Usage",
    "xUnit1033",
    Justification = "Server is started by the fixture.")]
public class MapItWireMappingWithDataTest : IClassFixture<MapItWireFixture>
{
    [Theory]
    [MapItWireRequestData("TestData")]
    public void Verify_TestData(TestModel testModel)
    {
        Assert.NotNull(testModel);
        Assert.Equal(
            1,
            testModel.Id);
        Assert.Equal(
            "Test",
            testModel.Name);
    }

    [Theory]
    [MapItWireRequestData("TestData")]
    [MapItWireMapping("StatusPing")]
    public async Task Verify_TestDataWithRequest(TestModel testModel)
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

        Assert.NotNull(testModel);
        Assert.Equal(
            1,
            testModel.Id);
        Assert.Equal(
            "Test",
            testModel.Name);
    }
}

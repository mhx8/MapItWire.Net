using System.Net.Http.Json;
using MapItWire.Net;
using MapItWire.Net.Attributes;
using TestBench.Rest;
using TestBench.Soap;
using WireMock.Logging;
using Xunit;

namespace TestBench.Tests;

public class TestBenchTests(
    CustomWebApplicationFactory<Program> factory, MapItWireFixture mapItWireFixture) : 
    IClassFixture<CustomWebApplicationFactory<Program>>,
    IClassFixture<MapItWireFixture>
{
    [Fact]
    [MapItWireMapping("TestRest")]
    public async Task TestRest()
    {
        // Arrange 
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/rest");

        // Assert
        response.EnsureSuccessStatusCode();
        WeatherResponse? weatherResponse = await response.Content.ReadFromJsonAsync<WeatherResponse>();
        Assert.NotNull(weatherResponse);
    }
    
    [Theory]
    [MapItWireMapping("TestRestWithBody")]
    [MapItWireRequestData("TestRestWithBody")]
    public async Task TestRestWithBody(WeatherRequest weatherRequest)
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync("/rest-with-body", weatherRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        WeatherResponse? weatherResponses = await response.Content.ReadFromJsonAsync<WeatherResponse>();
        Assert.NotNull(weatherResponses);
    }
    
    [Fact]
    [MapItWireMapping("TestSoap")]
    public async Task TestSoap()
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/soap");

        // Assert
        response.EnsureSuccessStatusCode();
        SoapResponse? soapResponse = await response.Content.ReadFromJsonAsync<SoapResponse>();
        Assert.NotNull(soapResponse);
    }
    
    [Fact]
    [MapItWireMapping("Complex")]
    public async Task TestComplex()
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/complex");

        // Assert
        response.EnsureSuccessStatusCode();

        IEnumerable<ILogEntry> entries = mapItWireFixture.GetServer().LogEntries;
        Assert.NotEmpty(entries);
    }
}
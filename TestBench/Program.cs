using MapItWire.Net.Extensions;
using Microsoft.AspNetCore.Mvc;
using TestBench;
using TestBench.Rest;
using TestBench.Soap;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddMapItWire();
builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<SoapClient>();
builder.Services.AddHttpClient<RestClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["RestServiceUrl"]!);
}).AddMapItWireHttpMessageHandler();

WebApplication app = builder.Build();

app.MapGet("/rest", async (
    RestClient restClient) => await restClient.TestRestClient());

app.MapPost("/rest-with-body", async (
    RestClient restClient, [FromBody] WeatherRequest weatherRequest) => await restClient.TestRequestWithBody(weatherRequest));

app.MapGet("/soap", async (SoapClient soapClient) => await soapClient.TestSoapClient());

app.MapGet("/complex", async (
    TestService testService) => await testService.TestAsync());

app.UseMapItWire();
app.Run();

namespace TestBench
{
    public partial class Program { }
}

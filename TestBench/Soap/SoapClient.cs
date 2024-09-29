
using System.ServiceModel;
using MapItWire.Net.Extensions;
using TestBench.SoapResponder;

namespace TestBench.Soap;

public class SoapClient(IServiceProvider serviceProvider, IConfiguration configuration)
{
    public async Task<SoapResponse> TestSoapClient()
    {
        BasicHttpBinding binding = new();
        EndpointAddress endpoint = new(configuration["SoapServiceUrl"]);
        SoapResponderPortTypeClient client = new(binding, endpoint);
        client.Endpoint.AddMapItWireEndpointBehavior(serviceProvider);

        Method1Response? xx =  await client.Method1Async("Hello", "World");
        return new SoapResponse(xx.bstrReturn);
    }
}
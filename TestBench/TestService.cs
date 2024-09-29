using TestBench.Rest;
using TestBench.Soap;

namespace TestBench;

public class TestService(RestClient restClient, SoapClient soapClient)
{
    public async Task TestAsync()
    {
        await restClient.TestRequestWithBody(new WeatherRequest(1));
        await restClient.TestRequestWithBody(new WeatherRequest(2));
        await restClient.TestRequestWithBody(new WeatherRequest(2));
        await restClient.GetBooksAsync();
        await restClient.GetCountriesAsync();

        
        await soapClient.TestSoapClient();
    }
}
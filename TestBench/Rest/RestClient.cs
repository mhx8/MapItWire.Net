namespace TestBench.Rest;

public class RestClient(
    HttpClient httpClient)
{
    public async Task<WeatherResponse?> TestRestClient()
    {
        return await httpClient.GetFromJsonAsync<WeatherResponse>("weathers/1");
    }
    
    public async Task GetBooksAsync()
    {
        await httpClient.GetAsync("books");
    }
    
    public async Task GetCountriesAsync()
    {
        await httpClient.GetAsync("countries");
    }
    
    public async Task<WeatherResponse?> TestRequestWithBody(WeatherRequest weatherRequest)
    {
        return await httpClient.GetFromJsonAsync<WeatherResponse>($"weathers/{weatherRequest.Id}");
    }
}
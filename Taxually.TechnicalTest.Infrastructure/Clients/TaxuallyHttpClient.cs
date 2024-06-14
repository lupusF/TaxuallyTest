namespace Taxually.TechnicalTest.Infrastructure;

public class TaxuallyHttpClient
{
    private readonly HttpClient _httpClient;
    public TaxuallyHttpClient(HttpClient client)
    {
            _httpClient = client;   
    }
    public Task PostAsync<TRequest>(string url, TRequest request)
    {
        // Actual HTTP call removed for purposes of this exercise
        return Task.CompletedTask;
    }
}

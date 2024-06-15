namespace Taxually.TechnicalTest.Infrastructure;

public class ApiVatRegistration : VatRegistrationTemplate
{
    private readonly TaxuallyHttpClient _taxuallyHttpClient;
    private readonly ApiUrlConfig _urlConfig;

    public ApiVatRegistration(TaxuallyHttpClient taxuallyHttpClient, IOptions<ApiUrlConfig> urlConfig)
    {
        _taxuallyHttpClient = taxuallyHttpClient;
        _urlConfig = urlConfig.Value;
    }

    protected override object PrepareData(VatRegistrationRequest request)
    {
        return request;
    }

    protected override async Task SendDataAsync(object data)
    {
        var countryCode = (data as VatRegistrationRequest).Country;
        _urlConfig.Urls.TryGetValue(countryCode, out var urlString);

        await _taxuallyHttpClient.PostAsync(urlString, (VatRegistrationRequest)data);
    }
}
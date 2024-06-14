namespace Taxually.TechnicalTest.Infrastructure;

public class ApiVatRegistration : VatRegistrationTemplate
{
    private readonly TaxuallyHttpClient _taxuallyHttpClient;

    public ApiVatRegistration(TaxuallyHttpClient taxuallyHttpClient)
    {
        _taxuallyHttpClient = taxuallyHttpClient;
    }

    protected override object PrepareData(VatRegistrationRequest request)
    {
        return request;
    }

    protected override async Task SendDataAsync(object data)
    {
        await _taxuallyHttpClient.PostAsync("https://api.uktax.gov.uk", (VatRegistrationRequest)data);
    }
}

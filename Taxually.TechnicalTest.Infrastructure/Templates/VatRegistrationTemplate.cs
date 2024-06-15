namespace Taxually.TechnicalTest.Infrastructure;

public abstract class VatRegistrationTemplate
{
    public async Task RegisterAsync(VatRegistrationRequest request)
    {
        var data = PrepareData(request);
        await SendDataAsync(data);
    }

    protected abstract object PrepareData(VatRegistrationRequest request);

    protected abstract Task SendDataAsync(object data);
}
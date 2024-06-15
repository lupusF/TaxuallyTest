namespace Taxually.TechnicalTest.Infrastructure;

public class CsvVatRegistration : VatRegistrationTemplate
{
    private readonly TaxuallyQueueClient _taxuallyQueueClient;

    public CsvVatRegistration(TaxuallyQueueClient taxuallyQueueClient)
    {
        _taxuallyQueueClient = taxuallyQueueClient;
    }

    protected override object PrepareData(VatRegistrationRequest request)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
        return Encoding.UTF8.GetBytes(csvBuilder.ToString());
    }

    protected override async Task SendDataAsync(object data)
    {
        await _taxuallyQueueClient.EnqueueAsync("vat-registration-csv", (byte[])data);
    }
}
namespace Taxually.TechnicalTest.Infrastructure;

public class XmlVatRegistration : VatRegistrationTemplate
{
    private readonly TaxuallyQueueClient _taxuallyQueueClient;

    public XmlVatRegistration(TaxuallyQueueClient taxuallyQueueClient)
    {
        _taxuallyQueueClient = taxuallyQueueClient;
    }

    protected override object PrepareData(VatRegistrationRequest request)
    {
        using (var stringWriter = new StringWriter())
        {
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringWriter, request);
            return stringWriter.ToString();
        }
    }

    protected override async Task SendDataAsync(object data)
    {
        await _taxuallyQueueClient.EnqueueAsync("vat-registration-xml", (string)data);
    }
}
namespace Taxually.TechnicalTest.Infrastructure;

public class VatRegistrationService : IVatRegistrationService
{
    private readonly Dictionary<string, VatRegistrationTemplate> _templates;
    public VatRegistrationService(Dictionary<string, VatRegistrationTemplate> templates)
    {
        _templates = templates;
    }
    public async Task RegisterVATAsync(VatRegistrationRequest request)
    {
        if (_templates.TryGetValue(request.Country!, out var template))
        {
            await template.RegisterAsync(request);
        }
    }
}

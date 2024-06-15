namespace Taxually.TechnicalTest.Infrastructure;

public class VatRegistrationRequestValidator : AbstractValidator<VatRegistrationRequest>
{
    private readonly VatRegistrationOptions _config;

    public VatRegistrationRequestValidator(IOptions<VatRegistrationOptions> config)
    {
        _config = config.Value;

        var countryIds = _config.Templates.Keys;

        Validate(countryIds);
    }

    private void Validate(Dictionary<string, string>.KeyCollection countryIds)
    {
        RuleFor(r => r.CompanyId).NotNull();
        RuleFor(r => r.CompanyName).NotNull();
        RuleFor(r => r.Country).NotNull();
        RuleFor(r => r.Country).Length(2);
        RuleFor(r => r.Country).Must(i => countryIds.Contains(i))
                               .WithMessage("Please only use: " + String.Join(", ", countryIds));
    }
}
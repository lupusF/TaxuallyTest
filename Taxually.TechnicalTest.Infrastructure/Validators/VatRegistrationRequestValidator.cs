namespace Taxually.TechnicalTest.Infrastructure;

public class VatRegistrationRequestValidator : AbstractValidator<VatRegistrationRequest>
{
    public VatRegistrationRequestValidator()
    {
        RuleFor(r => r.CompanyId).NotNull();
        RuleFor(r => r.CompanyName).NotNull();
        RuleFor(r => r.Country).NotNull();
        RuleFor(r => r.Country).Length(2);

    }
}

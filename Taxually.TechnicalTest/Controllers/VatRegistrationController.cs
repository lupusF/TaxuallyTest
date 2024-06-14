namespace Taxually.TechnicalTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VatRegistrationController : ControllerBase
{
    private readonly IVatRegistrationService _vatRegistrationService;
    private readonly IValidator<VatRegistrationRequest> _vatRegistrationRequestValidator;
    public VatRegistrationController(IVatRegistrationService vatRegistrationService, IValidator<VatRegistrationRequest> vatRegistrationRequestValidator)
    {
        _vatRegistrationService = vatRegistrationService;
        _vatRegistrationRequestValidator = vatRegistrationRequestValidator;
    }
    /// <summary>
    /// Registers a company for a VAT number in a given country
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
    {
        var validationResult = _vatRegistrationRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            await _vatRegistrationService.RegisterVATAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

namespace Taxually.TechnicalTest.UnitTests;

public class VatRegistrationControllerTest
{
    #region testData

    public static readonly object[][] IncorrectData =
        {
        new object[] {
            new VatRegistrationRequest()
            {
                CompanyId = "1",
                CompanyName = "Name",
                 Country = "Ger"
            },
            HttpStatusCode.BadRequest
        }
        };

    public static readonly object[][] CorrectData =
       {
        new object[] {
            new VatRegistrationRequest()
            {
                CompanyId = "1",
                CompanyName = "Name",
                Country = "GB"
            },
            HttpStatusCode.OK
        }
        };

    public static readonly VatRegistrationOptions vatRegistrationConfig = new()
    {
        Templates = new Dictionary<string, string>()

         {
                { "GB", "Taxually.TechnicalTest.Infrastructure.ApiVatRegistration, Taxually.TechnicalTest.Infrastructure" },
                { "FR", "Taxually.TechnicalTest.Infrastructure.CsvVatRegistration, Taxually.TechnicalTest.Infrastructure" },
                { "DE", "Taxually.TechnicalTest.Infrastructure.XmlVatRegistration, Taxually.TechnicalTest.Infrastructure" },
                { "EE", "Taxually.TechnicalTest.Infrastructure.XmlVatRegistration, Taxually.TechnicalTest.Infrastructure" }
            }
    };

    #endregion testData

    private IOptions<VatRegistrationOptions> _options = Options.Create(vatRegistrationConfig);

    [Theory]
    [MemberData(nameof(IncorrectData))]
    public async Task RegisterVAT_IncorrectData_ReturnBadRequest(VatRegistrationRequest request, HttpStatusCode expectedStatusCode)
    {
        var controller = new VatRegistrationController(Mock.Of<IVatRegistrationService>(),
                                                       new VatRegistrationRequestValidator(_options));

        var response = await controller.RegisterVAT(request);

        var statusCode = (HttpStatusCode)response
                            .GetType()
                            .GetProperty("StatusCode")!
                            .GetValue(response, null)!;

        Assert.Equal(expectedStatusCode, statusCode);
    }

    [Theory]
    [MemberData(nameof(CorrectData))]
    public async Task RegisterVAT_CrrectData_ReturnOK(VatRegistrationRequest request, HttpStatusCode expectedStatusCode)
    {
        var controller = new VatRegistrationController(Mock.Of<IVatRegistrationService>(),
                                                       new VatRegistrationRequestValidator(_options));

        var response = await controller.RegisterVAT(request);

        var statusCode = (HttpStatusCode)response
                            .GetType()
                            .GetProperty("StatusCode")!
                            .GetValue(response, null)!;

        Assert.Equal(expectedStatusCode, statusCode);
    }
}
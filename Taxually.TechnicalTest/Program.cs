var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IValidator<VatRegistrationRequest>, VatRegistrationRequestValidator>();
builder.Services.AddHttpClient<TaxuallyHttpClient>();
builder.Services.AddTransient<TaxuallyQueueClient>();

builder.Services.AddScoped<ApiVatRegistration>();
builder.Services.AddScoped<CsvVatRegistration>();
builder.Services.AddScoped<XmlVatRegistration>();

builder.Services.Configure<VatRegistrationOptions>(builder.Configuration.GetSection("VatRegistrationOptions"));
builder.Services.AddScoped<IVatRegistrationService, VatRegistrationService>(provider =>
{
    var options = provider.GetRequiredService<IOptions<VatRegistrationOptions>>().Value;
    var strategies = new Dictionary<string, VatRegistrationTemplate>();

    foreach (var kvp in options.Templates)
    {
        var template = Type.GetType(kvp.Value);
        if (template != null)
        {
            var strategyInstance = provider.GetRequiredService(template) as VatRegistrationTemplate;
            if (strategyInstance != null)
            {
                strategies[kvp.Key] = strategyInstance;
            }
        }
    }
    
    return new VatRegistrationService(strategies);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

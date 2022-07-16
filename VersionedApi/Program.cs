using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opts =>
{
    const string title = "Our Versioned API";
    const string description = "This is a Web API that demonstrates versioning.";
    var terms = new Uri("https://localhost:7112/terms");
    var license = new OpenApiLicense()
    {
        Name = "This is my full license information or a link to it"
    };
    var contact = new OpenApiContact()
    {
        Name = "Nate Thompson Helpdesk",
        Email = "support@azastudio.net",
        Url = new Uri("https://www.azastudio.net")
    };

    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = $"{title} v1 (deprecated)",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });

    opts.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = $"{title} v2",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });
});

// Api Versioning
builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new ApiVersion(1, 0);
    opts.ReportApiVersions = true;
});

// Configures versioning for swagger
builder.Services.AddVersionedApiExplorer(opts =>
{
    // v = version VVV = 1.00
    // ReSharper disable once StringLiteralTypo
    opts.GroupNameFormat = "'v'VVV";
    
    // Tells swagger to use a drop down for versioning
    opts.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Swagger UI configuration
app.UseSwagger();
app.UseSwaggerUI(opts =>
{
    // Placing the newest one first defaults swagger to the first version
    opts.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using ApiProtection.StartupConfig;
using AspNetCoreRateLimit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setting up api response cache
builder.Services.AddResponseCaching();

#region Rate Limiting

builder.Services.AddMemoryCache(); // Per server, this keeps track of the call, for multiple servers, you would want a redis cache like setup
// This can be used for more than rate limiting, so it shouldn't be combined with extension method.
builder.AddRateLimitServices();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

// Enables services
app.UseIpRateLimiting();

app.Run();

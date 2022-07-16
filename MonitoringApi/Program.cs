using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MonitoringApi.HealthChecks;

// https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This can be added to one application to monitor many microservice apis vs installing the client on all the different api's
// https://localhost:7032/healthchecks-ui
builder.Services.AddHealthChecksUI(opts =>
{
    opts.AddHealthCheckEndpoint("api", "/health"); // This is specific to the client
    opts.SetEvaluationTimeInSeconds(5); // This is aggressive for production, might do every 60 seconds or more
    opts.SetMinimumSecondsBetweenFailureNotifications(10); // This is how often between each report the system will report IE (every 5minutes in prod)
}).AddInMemoryStorage(); // This stores the health check in memory, it can be added to dbs if you want

builder.Services.AddHealthChecks()
    .AddCheck<RandomHealthCheck>("Site Health Check")
    .AddCheck<RandomHealthCheck>("Database Health Check");

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
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI();

app.Run();

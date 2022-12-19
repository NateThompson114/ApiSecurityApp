using MinimalApi.Library.DataAccess;
using MinimalApi.Routes;
using MinimalApi.ProgramExtensions.Services;
using MinimalApi.ProgramExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.AddSwaggerGenServices();
builder.AddSecurityRequirementServices();

builder.Services.AddSingleton<IDataAccess, DataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AddCustomerEndpoints();
app.AddOrderEndpoints();
app.AddAuthenticationEndpoints();

app.Run();
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Library.DataAccess;
using MinimalApi.Library.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanReadTodoList", policy =>
        //policy.Requirements.Add(new MinimumAgeRequirement(21))
        policy.RequireClaim("RTL")
    );
});

builder.Services.AddSingleton<IDataAccess, DataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/api/GetCustomers", (IDataAccess data) => 
{
    return Results.Ok(data.GetCustomers());    
});

app.MapGet("api/GetCustomer/{id}", (IDataAccess data, Guid id) =>
{
    return Results.Ok(data.GetCustomer(id));
});

app.MapGet("api/GetCustomerWithOrders/{id}", (IDataAccess data, Guid id) =>
{
    return Results.Ok(data.GetCustomerWithOrders(id));
});

app.MapPost("/api/AddCustomer", (IDataAccess data, [FromBody] CustomerDto dto) =>
{
    var newCustomer = dto.GetCustomer(dto);
    return Results.Ok(data.AddCustomer(newCustomer));
});

app.MapDelete("/api/DeleteCustomer/{id}", (IDataAccess data, Guid id) =>
{
    return Results.Ok(data.RemoveCustomer(id));
});

app.Run();
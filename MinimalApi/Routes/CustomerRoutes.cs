using Microsoft.AspNetCore.Mvc;
using MinimalApi.Library.DataAccess;
using MinimalApi.Library.Models;

namespace MinimalApi.Routes
{
    public static class CustomerRoutes
    {
        public static void AddCustomerEndpoints(this WebApplication app)
        {
            app.MapGet("/api/GetCustomers", async (IDataAccess data) =>
            {
                return Results.Ok(await data.GetCustomers());
            });

            app.MapGet("/api/GetCustomer/{id}", async (IDataAccess data, Guid id) =>
            {
                return Results.Ok(await data.GetCustomer(id));
            });

            app.MapGet("/api/GetCustomerWithOrders/{id}", async (IDataAccess data, Guid id) =>
            {
                return Results.Ok(await data.GetCustomerWithOrders(id));
            });

            app.MapPost("/api/AddCustomer", async (IDataAccess data, [FromBody] CustomerDto dto) =>
            {
                var newCustomer = dto.GetCustomer(dto);
                return Results.Ok(await data.AddCustomer(newCustomer));
            });

            app.MapDelete("/api/DeleteCustomer/{id}", async (IDataAccess data, Guid id) =>
            {
                return Results.Ok(await data.RemoveCustomer(id));
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MinimalApi.Library.DataAccess;
using MinimalApi.Library.Models;

namespace MinimalApi.Routes
{
    public static class CustomerRoutes
    {
        public static void AddCustomerRoutes(this WebApplication app)
        {
            app.MapGet("/api/GetCustomers", (IDataAccess data) =>
            {
                return Results.Ok(data.GetCustomers());
            });

            app.MapGet("/api/GetCustomer/{id}", (IDataAccess data, Guid id) =>
            {
                return Results.Ok(data.GetCustomer(id));
            });

            app.MapGet("/api/GetCustomerWithOrders/{id}", (IDataAccess data, Guid id) =>
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
        }
    }
}

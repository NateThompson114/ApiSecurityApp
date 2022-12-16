using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Library.DataAccess;
using MinimalApi.Library.Models;

namespace MinimalApi.Routes
{
    public static class CustomerRoutes
    {
        public static void AddCustomerEndpoints(this WebApplication app)
        {
            app.MapGet("/api/GetCustomers",[Authorize] async (IDataAccess data) =>
            {
                return Results.Ok(await data.GetCustomers());
            });

            app.MapGet("/api/GetCustomer/{id}", async (IDataAccess data, Guid id) =>
            {
                return Results.Ok(await data.GetCustomer(id));
            }).RequireAuthorization();

            app.MapGet("/api/GetCustomerWithOrders/{id}", async (IDataAccess data, Guid id) =>
            {
                return Results.Ok(await data.GetCustomerWithOrders(id));
            }).RequireAuthorization();

            app.MapPost("/api/AddCustomer", AddCustomer);

            app.MapDelete("/api/DeleteCustomer/{id}", DeleteCustomer);
        }

        [Authorize]
        private async static Task<IResult> AddCustomer(IDataAccess data, [FromBody] CustomerDto dto)
        {
            var newCustomer = dto.GetCustomer(dto);
            return Results.Ok(await data.AddCustomer(newCustomer));
        }

        [AllowAnonymous]
        private async static Task<IResult> DeleteCustomer(IDataAccess data, Guid id)
        {
            return Results.Ok(await data.RemoveCustomer(id));
        }
    }
}

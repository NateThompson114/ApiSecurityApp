using MinimalApi.Library.Constants;
using MinimalApi.Library.DataAccess;

namespace MinimalApi.Routes
{
    public static class OrderRoutes
    {
        public static void AddOrderEndpoints(this WebApplication app)
        {
            app.MapGet("/api/GetOrders", async (IDataAccess data) =>
            {
                return Results.Ok(await data.GetOrders());
            }).RequireAuthorization(policyNames:PolicyConstants.CanGetOrders);

            app.MapGet("/api/GetOrder/{id}", async (IDataAccess data, Guid id) =>
            {
                return Results.Ok(await data.GetOrder(id));
            }).RequireAuthorization(policyNames: PolicyConstants.CanGetOrders);

            app.MapGet("/api/GetCustomerOrders/{id}", async (IDataAccess data, Guid customerId) =>
            {
                return Results.Ok(await data.GetCustomerOrders(customerId));
            }).RequireAuthorization(policyNames: PolicyConstants.CanGetCustomersAndOrders);
        }
    }
}

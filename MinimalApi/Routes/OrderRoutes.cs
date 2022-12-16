using MinimalApi.Library.DataAccess;

namespace MinimalApi.Routes
{
    public static class OrderRoutes
    {
        public static void AddOrderEndpoints(this WebApplication app)
        {
            app.MapGet("/api/GetOrders", (IDataAccess data) =>
            {
                return Results.Ok(data.GetOrders());
            });

            app.MapGet("/api/GetOrder/{id}", (IDataAccess data, Guid id) =>
            {
                return Results.Ok(data.GetOrder(id));
            });

            app.MapGet("/api/GetCustomerOrders/{id}", (IDataAccess data, Guid customerId) =>
            {
                return Results.Ok(data.GetCustomerOrders(customerId));
            });
        }
    }
}

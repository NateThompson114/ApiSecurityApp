using MinimalApi.Library.Models;

namespace MinimalApi.Library.DataAccess
{
    public interface IDataAccess
    {
        Customer AddCustomer(Customer dto);
        Customer? GetCustomer(Guid id);
        IEnumerable<Order> GetCustomerOrders(Guid customerId);
        IEnumerable<Customer> GetCustomers();
        Customer? GetCustomerWithOrders(Guid id);
        Order? GetOrder(Guid id);
        IEnumerable<Order> GetOrders();
    }
}
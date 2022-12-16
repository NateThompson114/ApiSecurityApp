using MinimalApi.Library.Models;

namespace MinimalApi.Library.DataAccess
{
    public interface IDataAccess
    {
        Task<Customer> AddCustomer(Customer dto);
        Task<bool> RemoveCustomer(Guid id);
        Task<Customer?> GetCustomer(Guid id);
        Task<IEnumerable<Order>> GetCustomerOrders(Guid customerId);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer?> GetCustomerWithOrders(Guid id);
        Task<Order?> GetOrder(Guid id);
        Task<IEnumerable<Order>> GetOrders();
    }
}
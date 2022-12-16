using Bogus;
using MinimalApi.Library.Models;

namespace MinimalApi.Library.DataAccess;

public class DataAccess : IDataAccess
{
    private List<Customer> CustomerDb;
    private List<Order> OrderDb;
    public DataAccess()
    {
        Randomizer.Seed = new Random(123456789);
        var ordergenerator = new Faker<Order>()
            .RuleFor(o => o.Id, Guid.NewGuid)
            .RuleFor(o => o.Date, f => f.Date.Past(3))
            .RuleFor(o => o.OrderValue, f => f.Finance.Amount(0, 10000))
            .RuleFor(o => o.Shipped, f => f.Random.Bool(0.9f));
        var customerGenerator = new Faker<Customer>()
            .RuleFor(c => c.Id, Guid.NewGuid)
            .RuleFor(c => c.Name, f => f.Company.CompanyName())
            .RuleFor(c => c.Address, f => f.Address.FullAddress())
            .RuleFor(c => c.City, f => f.Address.City())
            .RuleFor(c => c.Country, f => f.Address.Country())
            .RuleFor(c => c.ZipCode, f => f.Address.ZipCode())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.ContactName, (f, c) => f.Name.FullName());

        CustomerDb = customerGenerator.Generate(100);
        OrderDb = CustomerDb.SelectMany(c =>
            ordergenerator.RuleFor(o => o.CustomerId, c.Id).Generate(Random.Shared.Next(1, 11))
        ).ToList();

    }

    public IEnumerable<Customer> GetCustomers() => CustomerDb;
    public Customer? GetCustomer(Guid id) => CustomerDb.FirstOrDefault(c => c.Id == id);
    public Customer? GetCustomerWithOrders(Guid id)
    {
        var customer = CustomerDb.FirstOrDefault(c => c.Id == id);
        customer.Orders = GetCustomerOrders(customer.Id);
        return customer;
    }
    public Customer AddCustomer(Customer customer)
    {
        CustomerDb.Add(customer);
        return customer;
    }

    public bool RemoveCustomer(Guid id)
    {
        var customer = CustomerDb.FirstOrDefault(c => c.Id == id);
        if (customer == null) return false;
        CustomerDb.Remove(customer);
        OrderDb.Where(o => o.CustomerId == customer.Id).Select(o => OrderDb.Remove(o));

        return true;
    }

    public IEnumerable<Order> GetOrders() => OrderDb;
    public IEnumerable<Order> GetCustomerOrders(Guid customerId) => OrderDb.Where(o => o.CustomerId == customerId);
    public Order? GetOrder(Guid id) => OrderDb.FirstOrDefault(c => c.Id == id);

}

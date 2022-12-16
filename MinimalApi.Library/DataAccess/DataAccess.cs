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

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        await Delay(30);
        return CustomerDb;
    }

    public async Task<Customer?> GetCustomer(Guid id)
    {
        await Delay();
        return CustomerDb.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Customer?> GetCustomerWithOrders(Guid id)
    {
        var customer = CustomerDb.FirstOrDefault(c => c.Id == id);
        await Delay();

        if(customer == null) return null;
        customer.Orders = await GetCustomerOrders(customer.Id);

        return customer;
    }
    public async Task<Customer> AddCustomer(Customer customer)
    {
        await Delay();

        CustomerDb.Add(customer);
        return customer;
    }

    public async Task<bool> RemoveCustomer(Guid id)
    {
        var customer = CustomerDb.FirstOrDefault(c => c.Id == id);
        if (customer == null) return false;
        await Delay();
        CustomerDb.Remove(customer);
        OrderDb.Where(o => o.CustomerId == customer.Id).Select(o => OrderDb.Remove(o));

        return true;
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        await Delay(50);
        return OrderDb;
    }

    public async Task<IEnumerable<Order>> GetCustomerOrders(Guid customerId)
    {
        await Delay();
        return OrderDb.Where(o => o.CustomerId == customerId);
    }

    public async Task<Order?> GetOrder(Guid id)
    {
        await Delay();
        return OrderDb.FirstOrDefault(c => c.Id == id);
    }

    internal async Task Delay(int additionalDelay = 0) => await Task.Delay(Random.Shared.Next(30, 500)+additionalDelay);
}

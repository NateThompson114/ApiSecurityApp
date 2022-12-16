namespace MinimalApi.Library.Models;

public class CustomerDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string ContactName { get; set; }

    public Customer GetCustomer(CustomerDto dto)
    {
        return new Customer
        {
            Id = dto.Id ?? Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            Country = dto.Country,
            ZipCode = dto.ZipCode,
            Phone = dto.Phone,
            Email = dto.Email,
            ContactName = dto.ContactName
        };
    }
}

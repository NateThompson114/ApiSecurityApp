namespace MinimalApi.Library.Models;

public class Order
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal OrderValue { get; set; }
    public bool Shipped { get; set; }
    public Guid CustomerId { get; set; }
}
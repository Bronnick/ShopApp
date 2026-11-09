namespace Orders.Api.Entities;

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int ProductId { get; set; }

    public required string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

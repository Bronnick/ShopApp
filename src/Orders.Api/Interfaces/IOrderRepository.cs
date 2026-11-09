using Orders.Api.Entities;

namespace Orders.Api.Interfaces;

public interface IOrderRepository
{
    void AddOrder(Order order);
    Task<IReadOnlyList<Order>> GetOrdersAsync();
    Task<Order?> GetOrderByIdAsync(string id);
}

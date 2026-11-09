using System.Collections.Concurrent;
using Orders.Api.Entities;
using Orders.Api.Interfaces;

namespace Orders.Api.Data;

public class OrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<string, Order> _orders = new();

    public void AddOrder(Order order)
    {
        _orders.TryAdd(order.Id, order);
    }

    public Task<IReadOnlyList<Order>> GetOrdersAsync()
    {
        IReadOnlyList<Order> orders = _orders.Values
            .OrderByDescending(order => order.CreatedAt)
            .ToList();

        return Task.FromResult(orders);
    }

    public Task<Order?> GetOrderByIdAsync(string id)
    {
        _orders.TryGetValue(id, out var order);

        return Task.FromResult(order);
    }
}

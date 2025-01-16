using System.Collections.Generic;
using System.Threading.Tasks;
using ProiectPSSC.Models;

namespace ProiectPSSC.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<string, Order> _orders = new();

    public Task<Order?> GetOrderByIdAsync(string orderId)
        => Task.FromResult(_orders.TryGetValue(orderId, out var order) ? order : null);

    public Task SaveOrderAsync(Order order)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }
}
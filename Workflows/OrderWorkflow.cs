using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProiectPSSC.Models;
using ProiectPSSC.Repositories;

namespace ProiectPSSC.Workflows;

public class OrderWorkflow
{
    private readonly IOrderRepository _repository;

    public OrderWorkflow(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> CreateOrderAsync(string customerName, List<OrderItem> items)
    {
        var orderId = Guid.NewGuid().ToString();
        var order = new Order(orderId, customerName, items, OrderStatus.Created);
        await _repository.SaveOrderAsync(order);
        return orderId;
    }

    public async Task ConfirmOrderAsync(string orderId)
    {
        var order = await _repository.GetOrderByIdAsync(orderId);
        if (order == null || order.Status != OrderStatus.Created)
        {
            throw new InvalidOperationException("Comanda nu poate fi confirmată.");
        }

        var updatedOrder = order with { Status = OrderStatus.Confirmed };
        await _repository.SaveOrderAsync(updatedOrder);
    }

    public async Task CancelOrderAsync(string orderId)
    {
        var order = await _repository.GetOrderByIdAsync(orderId);
        if (order == null || order.Status == OrderStatus.Canceled)
        {
            throw new InvalidOperationException("Comanda nu poate fi anulată.");
        }

        var updatedOrder = order with { Status = OrderStatus.Canceled };
        await _repository.SaveOrderAsync(updatedOrder);
    }
}
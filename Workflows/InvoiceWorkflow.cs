using System;
using System.Threading.Tasks;
using ProiectPSSC.Models;
using ProiectPSSC.Repositories;

namespace ProiectPSSC.Workflows;

public class InvoiceWorkflow
{
    private readonly IOrderRepository _repository;

    public InvoiceWorkflow(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task GenerateInvoiceAsync(string orderId)
    {
        var order = await _repository.GetOrderByIdAsync(orderId);
        if (order == null || order.Status != OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Factura nu poate fi generată pentru o comandă neconfirmată.");
        }

        Console.WriteLine($"Generare factură pentru comanda {order.Id}:");
        Console.WriteLine($"Client: {order.CustomerName}");
        foreach (var item in order.Items)
        {
            Console.WriteLine($" - {item.ProductName}: {item.Quantity} bucăți");
        }
        Console.WriteLine("Factura a fost generată cu succes.");
    }
}
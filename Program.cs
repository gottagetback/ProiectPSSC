﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProiectPSSC.Models;
using ProiectPSSC.Repositories;
using ProiectPSSC.Workflows;

namespace ProiectPSSC;

public class Program
{
    public static async Task Main(string[] args)
    {
        var repository = new InMemoryOrderRepository();
        var orderWorkflow = new OrderWorkflow(repository);
        var invoiceWorkflow = new InvoiceWorkflow(repository);

        // Creare comandă
        var orderId = await orderWorkflow.CreateOrderAsync("Ion Popescu", new List<OrderItem>
        {
            new OrderItem("Laptop", 1),
            new OrderItem("Mouse", 2)
        });
        Console.WriteLine($"Comandă creată cu ID: {orderId}");

        // Confirmare comandă
        await orderWorkflow.ConfirmOrderAsync(orderId);
        Console.WriteLine("Comanda a fost confirmată.");

        // Generare factură (workflow secundar)
        await invoiceWorkflow.GenerateInvoiceAsync(orderId);
    }
}
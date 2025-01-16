namespace ProiectPSSC.Models;

public record Order(string Id, string CustomerName, List<OrderItem> Items, OrderStatus Status);
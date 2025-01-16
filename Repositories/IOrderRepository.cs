using System.Threading.Tasks;
using ProiectPSSC.Models;

namespace ProiectPSSC.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetOrderByIdAsync(string orderId);
    Task SaveOrderAsync(Order order);
}
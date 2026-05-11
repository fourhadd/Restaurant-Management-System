using FoodieRestaurant.Models;
using FoodieRestaurant.ViewModels;

namespace FoodieRestaurant.Services.Interfaces;

public interface IOrderService
{
    Task<int> PlaceOrderAsync(string userId);
    Task<List<Order>> GetUserOrdersAsync(string userId);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderDetailsAsync(int orderId, string? userId = null, bool isAdmin = false);
    Task UpdateStatusAsync(int orderId, OrderStatus status);
    Task<DashboardViewModel> GetDashboardAsync();
}

using FoodieRestaurant.Data;
using FoodieRestaurant.Models;
using FoodieRestaurant.Services.Interfaces;
using FoodieRestaurant.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FoodieRestaurant.Services;

public class OrderService(ApplicationDbContext context) : IOrderService
{
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await context.Orders
            .Include(o => o.User)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        return new DashboardViewModel
        {
            TotalUsers = await context.Users.CountAsync(),
            TotalFoods = await context.Foods.CountAsync(),
            TotalOrders = await context.Orders.CountAsync(),
            Revenue = await context.Orders
                .Where(o => o.Status == OrderStatus.Completed || o.Status == OrderStatus.Preparing || o.Status == OrderStatus.Pending)
                .SumAsync(o => (decimal?)o.TotalPrice) ?? 0
        };
    }

    public async Task<Order?> GetOrderDetailsAsync(int orderId, string? userId = null, bool isAdmin = false)
    {
        var query = context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Food)
            .AsQueryable();

        if (!isAdmin && !string.IsNullOrWhiteSpace(userId))
        {
            query = query.Where(o => o.UserId == userId);
        }

        return await query.FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<List<Order>> GetUserOrdersAsync(string userId)
    {
        return await context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Food)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<int> PlaceOrderAsync(string userId)
    {
        // Load current cart and convert each cart line into an order line.
        var cartItems = await context.CartItems
            .Include(c => c.Food)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        if (!cartItems.Any())
        {
            throw new InvalidOperationException("Cannot place order with an empty cart.");
        }

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            TotalPrice = cartItems.Sum(c => (c.Food?.Price ?? 0) * c.Quantity),
            OrderItems = cartItems.Select(c => new OrderItem
            {
                FoodId = c.FoodId,
                Quantity = c.Quantity,
                Price = c.Food?.Price ?? 0
            }).ToList()
        };

        await context.Orders.AddAsync(order);
        // Cart must be cleared after a successful order creation.
        context.CartItems.RemoveRange(cartItems);
        await context.SaveChangesAsync();

        return order.OrderId;
    }

    public async Task UpdateStatusAsync(int orderId, OrderStatus status)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order is null) return;
        order.Status = status;
        await context.SaveChangesAsync();
    }
}

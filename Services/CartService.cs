using FoodieRestaurant.Data;
using FoodieRestaurant.Models;
using FoodieRestaurant.Services.Interfaces;
using FoodieRestaurant.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FoodieRestaurant.Services;

public class CartService(ApplicationDbContext context) : ICartService
{
    public async Task AddAsync(string userId, int foodId, int quantity = 1)
    {
        var existing = await context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.FoodId == foodId);
        if (existing is not null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            await context.CartItems.AddAsync(new CartItem
            {
                UserId = userId,
                FoodId = foodId,
                Quantity = quantity
            });
        }

        await context.SaveChangesAsync();
    }

    public async Task ClearAsync(string userId)
    {
        var items = await context.CartItems.Where(c => c.UserId == userId).ToListAsync();
        context.CartItems.RemoveRange(items);
        await context.SaveChangesAsync();
    }

    public async Task<CartViewModel> GetCartAsync(string userId)
    {
        var items = await context.CartItems
            .Include(c => c.Food)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return new CartViewModel
        {
            Items = items.Select(i => new CartLineViewModel
            {
                CartItemId = i.Id,
                FoodId = i.FoodId,
                FoodName = i.Food?.Name ?? string.Empty,
                ImagePath = i.Food?.ImagePath,
                Price = i.Food?.Price ?? 0,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    public async Task RemoveAsync(string userId, int cartItemId)
    {
        var item = await context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == cartItemId);
        if (item is null) return;
        context.CartItems.Remove(item);
        await context.SaveChangesAsync();
    }

    public async Task UpdateQuantityAsync(string userId, int cartItemId, int quantity)
    {
        var item = await context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == cartItemId);
        if (item is null) return;
        item.Quantity = Math.Max(1, quantity);
        await context.SaveChangesAsync();
    }
}

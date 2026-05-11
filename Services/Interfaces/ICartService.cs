using FoodieRestaurant.ViewModels;

namespace FoodieRestaurant.Services.Interfaces;

public interface ICartService
{
    Task<CartViewModel> GetCartAsync(string userId);
    Task AddAsync(string userId, int foodId, int quantity = 1);
    Task UpdateQuantityAsync(string userId, int cartItemId, int quantity);
    Task RemoveAsync(string userId, int cartItemId);
    Task ClearAsync(string userId);
}

using FoodieRestaurant.Models;
using FoodieRestaurant.ViewModels;

namespace FoodieRestaurant.Services.Interfaces;

public interface IFoodService
{
    Task<Food?> GetByIdAsync(int id);
    Task<Food?> GetWithCategoryAsync(int id);
    Task<PagedFoodListViewModel> SearchAsync(string? query, int? categoryId, int page, int pageSize);
    Task CreateAsync(Food food);
    Task UpdateAsync(Food food);
    Task DeleteAsync(int id);
}

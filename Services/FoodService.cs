using FoodieRestaurant.Models;
using FoodieRestaurant.Repositories.Interfaces;
using FoodieRestaurant.Services.Interfaces;
using FoodieRestaurant.ViewModels;

namespace FoodieRestaurant.Services;

public class FoodService(IFoodRepository foodRepository, IGenericRepository<Category> categoryRepository) : IFoodService
{
    public async Task CreateAsync(Food food)
    {
        await foodRepository.AddAsync(food);
        await foodRepository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var food = await foodRepository.GetByIdAsync(id);
        if (food is null) return;
        foodRepository.Delete(food);
        await foodRepository.SaveAsync();
    }

    public async Task<Food?> GetByIdAsync(int id) => await foodRepository.GetByIdAsync(id);

    public async Task<Food?> GetWithCategoryAsync(int id) => await foodRepository.GetWithCategoryAsync(id);

    public async Task<PagedFoodListViewModel> SearchAsync(string? query, int? categoryId, int page, int pageSize)
    {
        var (foods, totalCount) = await foodRepository.SearchAsync(query, categoryId, page, pageSize);
        var categories = await categoryRepository.GetAllAsync();
        return new PagedFoodListViewModel
        {
            Foods = foods,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            SearchQuery = query,
            CategoryId = categoryId,
            Categories = categories
        };
    }

    public async Task UpdateAsync(Food food)
    {
        foodRepository.Update(food);
        await foodRepository.SaveAsync();
    }
}

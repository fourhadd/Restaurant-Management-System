using FoodieRestaurant.Models;

namespace FoodieRestaurant.Repositories.Interfaces;

public interface IFoodRepository : IGenericRepository<Food>
{
    Task<(List<Food> Foods, int TotalCount)> SearchAsync(string? query, int? categoryId, int page, int pageSize);
    Task<Food?> GetWithCategoryAsync(int id);
}

using FoodieRestaurant.Data;
using FoodieRestaurant.Models;
using FoodieRestaurant.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodieRestaurant.Repositories;

public class FoodRepository(ApplicationDbContext context) : GenericRepository<Food>(context), IFoodRepository
{
    public async Task<Food?> GetWithCategoryAsync(int id)
    {
        return await Context.Foods
            .Include(f => f.Category)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<(List<Food> Foods, int TotalCount)> SearchAsync(string? query, int? categoryId, int page, int pageSize)
    {
        var foodsQuery = Context.Foods
            .Include(f => f.Category)
            .Where(f => f.IsAvailable)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim().ToLower();
            foodsQuery = foodsQuery.Where(f =>
                f.Name.ToLower().Contains(query) || f.Description.ToLower().Contains(query));
        }

        if (categoryId.HasValue)
        {
            foodsQuery = foodsQuery.Where(f => f.CategoryId == categoryId.Value);
        }

        var totalCount = await foodsQuery.CountAsync();
        var foods = await foodsQuery
            .OrderByDescending(f => f.Rating)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (foods, totalCount);
    }
}

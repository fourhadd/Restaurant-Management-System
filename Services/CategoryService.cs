using FoodieRestaurant.Models;
using FoodieRestaurant.Repositories.Interfaces;
using FoodieRestaurant.Services.Interfaces;

namespace FoodieRestaurant.Services;

public class CategoryService(IGenericRepository<Category> categoryRepository) : ICategoryService
{
    public async Task CreateAsync(Category category)
    {
        await categoryRepository.AddAsync(category);
        await categoryRepository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null) return;

        categoryRepository.Delete(category);
        await categoryRepository.SaveAsync();
    }

    public async Task<List<Category>> GetAllAsync() => await categoryRepository.GetAllAsync();

    public async Task<Category?> GetByIdAsync(int id) => await categoryRepository.GetByIdAsync(id);

    public async Task UpdateAsync(Category category)
    {
        categoryRepository.Update(category);
        await categoryRepository.SaveAsync();
    }
}

using FoodieRestaurant.Models;

namespace FoodieRestaurant.ViewModels;

public class PagedFoodListViewModel
{
    public List<Food> Foods { get; set; } = new();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string? SearchQuery { get; set; }
    public int? CategoryId { get; set; }
    public List<Category> Categories { get; set; } = new();
}

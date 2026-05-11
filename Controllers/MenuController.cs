using FoodieRestaurant.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodieRestaurant.Controllers;

public class MenuController(IFoodService foodService) : Controller
{
    public async Task<IActionResult> Index(string? search, int? categoryId, int page = 1)
    {
        const int pageSize = 8;
        var model = await foodService.SearchAsync(search, categoryId, page, pageSize);
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var food = await foodService.GetWithCategoryAsync(id);
        if (food is null) return NotFound();
        return View(food);
    }
}

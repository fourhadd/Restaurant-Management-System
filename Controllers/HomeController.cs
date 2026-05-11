using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FoodieRestaurant.Models;
using FoodieRestaurant.Services.Interfaces;
using FoodieRestaurant.ViewModels;

namespace FoodieRestaurant.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFoodService _foodService;
    private readonly ICategoryService _categoryService;

    public HomeController(ILogger<HomeController> logger, IFoodService foodService, ICategoryService categoryService)
    {
        _logger = logger;
        _foodService = foodService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        var popular = await _foodService.SearchAsync(null, null, 1, 6);
        var model = new HomeViewModel
        {
            Categories = categories.Take(6).ToList(),
            PopularFoods = popular.Foods.Take(6).ToList()
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

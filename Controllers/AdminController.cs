using FoodieRestaurant.Models;
using FoodieRestaurant.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodieRestaurant.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(ICategoryService categoryService, IFoodService foodService, IOrderService orderService, IWebHostEnvironment env) : Controller
{
    public async Task<IActionResult> Dashboard()
    {
        var model = await orderService.GetDashboardAsync();
        return View(model);
    }

    public async Task<IActionResult> Categories()
    {
        return View(await categoryService.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (!ModelState.IsValid) return RedirectToAction(nameof(Categories));
        await categoryService.CreateAsync(category);
        TempData["ToastSuccess"] = "Category created.";
        return RedirectToAction(nameof(Categories));
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(Category category)
    {
        if (!ModelState.IsValid) return RedirectToAction(nameof(Categories));
        await categoryService.UpdateAsync(category);
        TempData["ToastSuccess"] = "Category updated.";
        return RedirectToAction(nameof(Categories));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await categoryService.DeleteAsync(id);
        TempData["ToastInfo"] = "Category deleted.";
        return RedirectToAction(nameof(Categories));
    }

    public async Task<IActionResult> Foods(string? search, int? categoryId, int page = 1)
    {
        var model = await foodService.SearchAsync(search, categoryId, page, 10);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> CreateFood()
    {
        ViewBag.Categories = await categoryService.GetAllAsync();
        return View(new Food());
    }

    [HttpPost]
    public async Task<IActionResult> CreateFood(Food food, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await categoryService.GetAllAsync();
            return View(food);
        }

        if (imageFile is not null && imageFile.Length > 0)
        {
            var uploadsPath = Path.Combine(env.WebRootPath, "images", "foods");
            Directory.CreateDirectory(uploadsPath);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var fullPath = Path.Combine(uploadsPath, fileName);
            await using var fs = new FileStream(fullPath, FileMode.Create);
            await imageFile.CopyToAsync(fs);
            food.ImagePath = $"/images/foods/{fileName}";
        }

        await foodService.CreateAsync(food);
        TempData["ToastSuccess"] = "Food created.";
        return RedirectToAction(nameof(Foods));
    }

    [HttpGet]
    public async Task<IActionResult> EditFood(int id)
    {
        var food = await foodService.GetByIdAsync(id);
        if (food is null) return NotFound();
        ViewBag.Categories = await categoryService.GetAllAsync();
        return View(food);
    }

    [HttpPost]
    public async Task<IActionResult> EditFood(Food food, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await categoryService.GetAllAsync();
            return View(food);
        }

        if (imageFile is not null && imageFile.Length > 0)
        {
            var uploadsPath = Path.Combine(env.WebRootPath, "images", "foods");
            Directory.CreateDirectory(uploadsPath);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var fullPath = Path.Combine(uploadsPath, fileName);
            await using var fs = new FileStream(fullPath, FileMode.Create);
            await imageFile.CopyToAsync(fs);
            food.ImagePath = $"/images/foods/{fileName}";
        }

        await foodService.UpdateAsync(food);
        TempData["ToastSuccess"] = "Food updated.";
        return RedirectToAction(nameof(Foods));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteFood(int id)
    {
        await foodService.DeleteAsync(id);
        TempData["ToastInfo"] = "Food deleted.";
        return RedirectToAction(nameof(Foods));
    }

    public async Task<IActionResult> Orders()
    {
        return View(await orderService.GetAllOrdersAsync());
    }

    [HttpPost]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
    {
        await orderService.UpdateStatusAsync(orderId, status);
        TempData["ToastSuccess"] = "Order status updated.";
        return RedirectToAction(nameof(Orders));
    }
}

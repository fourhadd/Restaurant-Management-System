using FoodieRestaurant.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodieRestaurant.Controllers;

[Authorize]
public class CartController(ICartService cartService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var model = await cartService.GetCartAsync(userId);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int foodId, int quantity = 1)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await cartService.AddAsync(userId, foodId, quantity);
        TempData["ToastSuccess"] = "Added to cart.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(int cartItemId, int quantity)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await cartService.UpdateQuantityAsync(userId, cartItemId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int cartItemId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await cartService.RemoveAsync(userId, cartItemId);
        TempData["ToastInfo"] = "Item removed from cart.";
        return RedirectToAction(nameof(Index));
    }
}

using FoodieRestaurant.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodieRestaurant.Controllers;

[Authorize]
public class OrdersController(IOrderService orderService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var orders = await orderService.GetUserOrdersAsync(userId);
        return View(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var orderId = await orderService.PlaceOrderAsync(userId);
            return RedirectToAction(nameof(Success), new { orderId });
        }
        catch (InvalidOperationException ex)
        {
            TempData["ToastError"] = ex.Message;
            return RedirectToAction("Index", "Cart");
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var order = await orderService.GetOrderDetailsAsync(id, userId);
        if (order is null) return NotFound();
        return View(order);
    }

    public IActionResult Success(int orderId)
    {
        ViewBag.OrderId = orderId;
        return View();
    }
}

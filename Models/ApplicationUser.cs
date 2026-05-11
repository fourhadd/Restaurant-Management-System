using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FoodieRestaurant.Models;

public class ApplicationUser : IdentityUser
{
    [StringLength(80)]
    public string? FullName { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

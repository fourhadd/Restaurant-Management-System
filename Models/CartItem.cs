using System.ComponentModel.DataAnnotations;

namespace FoodieRestaurant.Models;

public class CartItem
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }

    [Required]
    public int FoodId { get; set; }
    public Food? Food { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; } = 1;
}

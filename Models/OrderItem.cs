using System.ComponentModel.DataAnnotations;

namespace FoodieRestaurant.Models;

public class OrderItem
{
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    [Required]
    public int FoodId { get; set; }
    public Food? Food { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }

    [Range(0.1, 10000)]
    public decimal Price { get; set; }
}

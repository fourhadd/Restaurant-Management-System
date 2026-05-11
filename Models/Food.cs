using System.ComponentModel.DataAnnotations;

namespace FoodieRestaurant.Models;

public class Food
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(700)]
    public string Description { get; set; } = string.Empty;

    [Range(0.1, 10000)]
    public decimal Price { get; set; }

    [StringLength(300)]
    public string? ImagePath { get; set; }

    [Range(0, 5)]
    public double Rating { get; set; } = 4;

    public bool IsAvailable { get; set; } = true;

    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}

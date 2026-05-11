using System.ComponentModel.DataAnnotations;

namespace FoodieRestaurant.Models;

public class Order
{
    public int OrderId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Range(0, 100000)]
    public decimal TotalPrice { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

namespace FoodieRestaurant.ViewModels;

public class CartViewModel
{
    public List<CartLineViewModel> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(i => i.LineTotal);
    public bool IsEmpty => !Items.Any();
}

public class CartLineViewModel
{
    public int CartItemId { get; set; }
    public int FoodId { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => Price * Quantity;
}

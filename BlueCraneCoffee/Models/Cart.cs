namespace BlueCraneCoffee.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; } = string.Empty;
    public decimal LineTotal => Price * Quantity;
}

public class CartSummary
{
    public const decimal FreeShippingThreshold = 500m;
    public const decimal ShippingFee = 60m;

    public List<CartItem> Items { get; init; } = new();
    public int Count => Items.Sum(i => i.Quantity);
    public decimal Subtotal => Items.Sum(i => i.LineTotal);
    public decimal Shipping => Items.Count == 0 || Subtotal >= FreeShippingThreshold ? 0m : ShippingFee;
    public decimal Total => Subtotal + Shipping;
}

using System.ComponentModel.DataAnnotations;

namespace BlueCraneCoffee.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = new();
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

public class CheckoutForm
{
    [Required(ErrorMessage = "Please enter your name."), StringLength(80)]
    [Display(Name = "Full name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "We need an email for your receipt."), EmailAddress(ErrorMessage = "That email doesn't look right."), StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Add a phone number for the courier."), Phone, StringLength(30)]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter a delivery address."), StringLength(160)]
    [Display(Name = "Street address")]
    public string AddressLine { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter your city."), StringLength(60)]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter a postal code."), StringLength(10)]
    [Display(Name = "Postal code")]
    public string PostalCode { get; set; } = string.Empty;
}

public class CheckoutViewModel
{
    public CheckoutForm Form { get; set; } = new();
    public CartSummary Cart { get; set; } = new();
}

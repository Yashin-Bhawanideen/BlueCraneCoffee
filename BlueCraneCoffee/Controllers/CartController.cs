using BlueCraneCoffee.Data;
using BlueCraneCoffee.Models;
using BlueCraneCoffee.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlueCraneCoffee.Controllers;

public class CartController(AppDbContext db, ILogger<CartController> logger) : Controller
{
    public IActionResult Index() => View(CartSession.Summary(HttpContext.Session));

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Add(int productId, int quantity = 1, string? returnUrl = null)
    {
        var product = ProductData.Find(productId);
        if (product is null) return NotFound();

        quantity = Math.Clamp(quantity, 1, 20);
        var cart = CartSession.Get(HttpContext.Session);
        var line = cart.FirstOrDefault(i => i.ProductId == productId);
        if (line is null)
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity,
                Image = product.Image
            });
        }
        else
        {
            line.Quantity = Math.Min(line.Quantity + quantity, 20);
        }

        CartSession.Save(HttpContext.Session, cart);
        TempData["Added"] = $"{product.Name} was added to your cart.";

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return LocalRedirect(returnUrl);
        return RedirectToAction("Index", "Shop");
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Update(int productId, int quantity)
    {
        var cart = CartSession.Get(HttpContext.Session);
        var line = cart.FirstOrDefault(i => i.ProductId == productId);
        if (line is not null)
        {
            if (quantity <= 0) cart.Remove(line);
            else line.Quantity = Math.Min(quantity, 20);
            CartSession.Save(HttpContext.Session, cart);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Remove(int productId)
    {
        var cart = CartSession.Get(HttpContext.Session);
        cart.RemoveAll(i => i.ProductId == productId);
        CartSession.Save(HttpContext.Session, cart);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = CartSession.Summary(HttpContext.Session);
        if (cart.Items.Count == 0) return RedirectToAction("Index", "Shop");
        return View(new CheckoutViewModel { Cart = cart });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout([Bind(Prefix = "Form")] CheckoutForm form)
    {
        var cart = CartSession.Summary(HttpContext.Session);
        if (cart.Items.Count == 0) return RedirectToAction("Index", "Shop");

        var vm = new CheckoutViewModel { Form = form, Cart = cart };
        if (!ModelState.IsValid) return View(vm);

        var order = new Order
        {
            CustomerName = form.CustomerName,
            Email = form.Email,
            Phone = form.Phone,
            AddressLine = form.AddressLine,
            City = form.City,
            PostalCode = form.PostalCode,
            Total = cart.Total,
            CreatedAtUtc = DateTime.UtcNow,
            Items = cart.Items.Select(i => new OrderItem
            {
                ProductName = i.Name,
                UnitPrice = i.Price,
                Quantity = i.Quantity
            }).ToList()
        };

        try
        {
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not save order");
            ModelState.AddModelError(string.Empty,
                "We couldn't place your order because the database isn't reachable. " +
                "Check the connection string in appsettings.json and run Update-Database.");
            return View(vm);
        }

        CartSession.Clear(HttpContext.Session);
        TempData["OrderId"] = order.Id;
        TempData["OrderTotal"] = order.Total.Money();
        TempData["OrderName"] = order.CustomerName;
        return RedirectToAction(nameof(Success));
    }

    public IActionResult Success()
    {
        if (TempData["OrderId"] is null) return RedirectToAction("Index", "Shop");
        return View();
    }
}

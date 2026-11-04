using BlueCraneCoffee.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueCraneCoffee.Controllers;

public class ShopController : Controller
{
    public IActionResult Index(string? category)
    {
        var items = ProductData.Items.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(category) &&
            ProductData.Categories.Contains(category, StringComparer.OrdinalIgnoreCase))
        {
            items = items.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
        ViewData["Category"] = category;
        return View(items.ToList());
    }
}

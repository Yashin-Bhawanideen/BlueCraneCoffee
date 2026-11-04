using BlueCraneCoffee.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueCraneCoffee.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() =>
        View(ProductData.Items.Where(p => p.Id is 1 or 5 or 9).ToList());

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}

using Microsoft.AspNetCore.Mvc;

namespace BlueCraneCoffee.Controllers;

public class MenuController : Controller
{
    public IActionResult Index() => View();
}

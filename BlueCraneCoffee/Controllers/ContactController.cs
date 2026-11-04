using BlueCraneCoffee.Data;
using BlueCraneCoffee.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueCraneCoffee.Controllers;

public class ContactController(AppDbContext db, ILogger<ContactController> logger) : Controller
{
    [HttpGet]
    public IActionResult Index() => View(new ContactMessage());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([Bind("Name,Email,Message")] ContactMessage model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            model.CreatedAtUtc = DateTime.UtcNow;
            db.ContactMessages.Add(model);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not save contact message");
            ModelState.AddModelError(string.Empty,
                "We couldn't send your message because the database isn't reachable. " +
                "Check the connection string in appsettings.json and run Update-Database.");
            return View(model);
        }

        TempData["Sent"] = "Thanks, your message is on its way. We usually reply within a day.";
        return RedirectToAction(nameof(Index));
    }
}

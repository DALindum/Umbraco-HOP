using Microsoft.AspNetCore.Mvc;

namespace UmbracoHOP.Controllers;

public class AdvancedController : Controller
{
    // GET
    public IActionResult Advanced()
    {
        return View();
    }
}
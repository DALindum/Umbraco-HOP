using Microsoft.AspNetCore.Mvc;

namespace UmbracoHOP.Controllers;

public class ProfileController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Profile()
    {
        return View();
    }
}
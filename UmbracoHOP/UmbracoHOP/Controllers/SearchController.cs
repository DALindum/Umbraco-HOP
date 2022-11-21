using Microsoft.AspNetCore.Mvc;

namespace UmbracoHOP.Controllers;

public class SearchController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Search()
    {
        return View();
    }
}
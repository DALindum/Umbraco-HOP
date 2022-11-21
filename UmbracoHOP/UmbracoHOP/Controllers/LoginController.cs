using Microsoft.AspNetCore.Mvc;
using UmbracoHOP.Models;

namespace UmbracoHOP.Controllers;

public class LoginController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST
    [HttpPost]
    public IActionResult Login(LoginModel login)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Homepage", "Home");
        }

        return View();
    }
}
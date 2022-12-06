using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UmbracoHOP.Models;

namespace UmbracoHOP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // [HttpGet]
    // public IActionResult Homepage()
    // {
    //     return View();
    // }

    public async Task<ActionResult> Homepage()
    {
        string apiUrl = "https://localhost:7209/api/UmbracoInstall/GetAllUmbracoInstalls";

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);


                Debug.WriteLine(data);
                Debug.WriteLine(table);
            }
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
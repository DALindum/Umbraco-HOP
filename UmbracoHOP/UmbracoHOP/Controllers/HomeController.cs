using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

    private const string ApiUrl = "https://localhost:7209/";

    public async Task<ActionResult> Homepage()
    {
        var umbracoInstallsInfo = new List<UmbracoInstallsModel>();
    
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ApiUrl);
            client.DefaultRequestHeaders.Clear();
    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    
            HttpResponseMessage response = await client.GetAsync("api/UmbracoInstall/GetAllUmbracoInstalls");
    
            if (response.IsSuccessStatusCode)
            {
                var umbracoInstallsResponse = response.Content.ReadAsStringAsync().Result;
                
             
                umbracoInstallsInfo = JsonConvert.DeserializeObject<List<UmbracoInstallsModel>>(umbracoInstallsResponse);
            }

            // foreach (var package in umbracoInstallsInfo)
            // {
            //     var jsonResult = (Json(package.Package));
            // }
            return View(umbracoInstallsInfo);
        }
    }

    // public async Task<ActionResult> Homepage()
    // {
    //     string apiUrl = "https://localhost:7209/api/UmbracoInstall/GetAllUmbracoInstalls";
    //
    //     using (HttpClient client = new HttpClient())
    //     {
    //         client.BaseAddress = new Uri(apiUrl);
    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(
    //             new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //
    //         HttpResponseMessage response = await client.GetAsync(apiUrl);
    //         if (response.IsSuccessStatusCode)
    //         {
    //             var data = await response.Content.ReadAsStringAsync();
    //             var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
    //
    //
    //             Debug.WriteLine(data);
    //             Debug.WriteLine(table);
    //         }
    //     }
    //
    //     return View();
    // }

    // public async Task<ActionResult> Homepage()
    // {
    //     // string apiURL = $"https://localhost:7209/api/UmbracoInstall/GetPackageName?packageName={INPUT}";
    //     
    //     using (HttpClient client = new HttpClient())
    //     {
    //         client.BaseAddress = new Uri(apiUrl);
    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(
    //             new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //
    //         HttpResponseMessage response = await client.GetAsync(apiUrl);
    //         if (response.IsSuccessStatusCode)
    //         {
    //             var data = await response.Content.ReadAsStringAsync();
    //             var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
    //
    //
    //             Debug.WriteLine(data);
    //             Debug.WriteLine(table);
    //         }
    //     }
    //     
    //     return View();
    // }

    // public async Task<ActionResult> Homepage()
    // {
    //     string apiUrl =
    //         $"https://localhost:44327/api/UmbracoInstall/GetPackageVersion?packageName={INPUT}&packageVersion={INPUT}";
    //
    //     using (HttpClient client = new HttpClient())
    //     {
    //         client.BaseAddress = new Uri(apiUrl);
    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(
    //             new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //
    //         HttpResponseMessage response = await client.GetAsync(apiUrl);
    //         if (response.IsSuccessStatusCode)
    //         {
    //             var data = await response.Content.ReadAsStringAsync();
    //             var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
    //
    //
    //             Debug.WriteLine(data);
    //             Debug.WriteLine(table);
    //         }
    //     }
    //
    //     return View();
    // }
    //
    // public async Task<ActionResult> Homepage()
    // {
    //     string apiUrl = $"https://localhost:44327/api/UmbracoInstall/GetUmbracoVersion?version={INPUT}";
    //
    //     using (HttpClient client = new HttpClient())
    //     {
    //         client.BaseAddress = new Uri(apiUrl);
    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(
    //             new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //
    //         HttpResponseMessage response = await client.GetAsync(apiUrl);
    //         if (response.IsSuccessStatusCode)
    //         {
    //             var data = await response.Content.ReadAsStringAsync();
    //             var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
    //
    //
    //             Debug.WriteLine(data);
    //             Debug.WriteLine(table);
    //         }
    //     }
    //     
    //     return View();
    // }
    //
    // public async Task<ActionResult> Homepage()
    // {
    //     string apiUrl = $"https://localhost:44327/api/UmbracoInstall/GetCountry?country={INPUT}";
    //
    //     using (HttpClient client = new HttpClient())
    //     {
    //         client.BaseAddress = new Uri(apiUrl);
    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(
    //             new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //
    //         HttpResponseMessage response = await client.GetAsync(apiUrl);
    //         if (response.IsSuccessStatusCode)
    //         {
    //             var data = await response.Content.ReadAsStringAsync();
    //             var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
    //
    //
    //             Debug.WriteLine(data);
    //             Debug.WriteLine(table);
    //         }
    //     }
    //     
    //     return View();
    // }
    //
    // public async Task<ActionResult> Homepage()
    // {
    //     string apiUrl = $"https://localhost:44327/api/UmbracoInstall/GetCity?city={INPUT}";
    //     
    //     using (HttpClient client = new HttpClient())
    //     {
    //         client.BaseAddress = new Uri(apiUrl);
    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(
    //             new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //
    //         HttpResponseMessage response = await client.GetAsync(apiUrl);
    //         if (response.IsSuccessStatusCode)
    //         {
    //             var data = await response.Content.ReadAsStringAsync();
    //             var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
    //
    //
    //             Debug.WriteLine(data);
    //             Debug.WriteLine(table);
    //         }
    //     }
    //     
    //     return View();
    // }
    //
    // public async Task<ActionResult> Homepage()
    // {
    //     string apiUrl = $"https://localhost:44327/api/UmbracoInstall/GetContinent?continent={INPUT}";
    //
    //     using (HttpClient client = new HttpClient())
    //     {
    //         client.BaseAddress = new Uri(apiUrl);
    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(
    //             new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    //
    //         HttpResponseMessage response = await client.GetAsync(apiUrl);
    //         if (response.IsSuccessStatusCode)
    //         {
    //             var data = await response.Content.ReadAsStringAsync();
    //             var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
    //
    //
    //             Debug.WriteLine(data);
    //             Debug.WriteLine(table);
    //         }
    //     }
    //     
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
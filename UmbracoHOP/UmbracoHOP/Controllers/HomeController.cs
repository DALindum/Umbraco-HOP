using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UmbracoHOP.Models;

namespace UmbracoHOP.Controllers;

public class HomeController : Controller
{
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


                umbracoInstallsInfo =
                    JsonConvert.DeserializeObject<List<UmbracoInstallsModel>>(umbracoInstallsResponse);
            }

            var totalContinents = new List<string>();
            var totalCountries = new List<string>();
            var totalCities = new List<string>();

            foreach (var install in umbracoInstallsInfo)
            {
                totalContinents.Add(install.Continent);
                totalCountries.Add(install.Country);
                totalCities.Add(install.City);
            }

            ViewBag.TotalInstances = umbracoInstallsInfo.Count;
            ViewBag.TotalContinents = totalContinents.Distinct().Count();
            ViewBag.TotalCountries = totalCountries.Distinct().Count();
            ViewBag.TotalCities = totalCities.Distinct().Count();

            return View(umbracoInstallsInfo);
        }
    }
}
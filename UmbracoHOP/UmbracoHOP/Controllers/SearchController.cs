using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UmbracoHOP.Models;

namespace UmbracoHOP.Controllers;

public class SearchController : Controller
{
    private const string ApiUrl = "https://localhost:7209/";
    
    private List<UmbracoInstallsModel> exportList = new ();

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        var normalSearchPage = await ApiCaller("api/UmbracoInstall/GetAllUmbracoInstalls");

        ViewBag.TotalInstances = normalSearchPage?.Count ?? 0;

        return View(normalSearchPage);
    }

    public async Task<ActionResult> Search(IFormCollection formCollection)
    {
        // ViewData["SearchString"] = searchString;
        var searchString = formCollection["SearchString"].ToString();
        var searchOption = formCollection["SearchOptions"].ToString();
        var fromDate = formCollection["FromDate"].ToString();
        var toDate = formCollection["ToDate"].ToString();

        List<UmbracoInstallsModel>? searchedOption = null;

        switch (searchOption)
        {
            case "SearchAll":
                searchedOption =
                    await ApiCaller($"api/UmbracoInstall/GetAllUmbracoInstalls?fromDate={fromDate}&toDate={toDate}");
                break;
            case "Country":
                searchedOption =
                    await ApiCaller(
                        $"api/UmbracoInstall/GetCountry?country={searchString}&fromDate={fromDate}&toDate={toDate}");
                break;
            case "City":
                searchedOption =
                    await ApiCaller(
                        $"api/UmbracoInstall/GetCity?city={searchString}&fromDate={fromDate}&toDate={toDate}");
                break;
            case "Continent":
                searchedOption =
                    await ApiCaller(
                        $"api/UmbracoInstall/GetContinent?continent={searchString}&fromDate={fromDate}&toDate={toDate}");
                break;
            case "Version":
                searchedOption =
                    await ApiCaller(
                        $"api/UmbracoInstall/GetUmbracoVersion?version={searchString}&fromDate={fromDate}&toDate={toDate}");
                break;
            case "PackageName":
                searchedOption =
                    await ApiCaller(
                        $"api/UmbracoInstall/GetPackageName?packageName={searchString}&fromDate={fromDate}&toDate{toDate}");
                break;
        }

        ViewBag.TotalInstances = searchedOption?.Count ?? 0;
        ViewBag.SelectedFilter = searchOption ?? "Nothing";
        
        
        return View(searchedOption);
    }


    public IActionResult ExportToCsv(IFormCollection formCollection)
    {
        var export = formCollection["Export"];
        string test;
        
       

        return RedirectToAction("Search");
    }


    private async Task<List<UmbracoInstallsModel>?> ApiCaller(string apiAction)
    {
        var umbracoInstallsInfo = new List<UmbracoInstallsModel>();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ApiUrl);
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiAction);

            if (response.IsSuccessStatusCode)
            {
                var umbracoInstallsResponse = response.Content.ReadAsStringAsync().Result;
                umbracoInstallsInfo =
                    JsonConvert.DeserializeObject<List<UmbracoInstallsModel>>(umbracoInstallsResponse);
            }

            return umbracoInstallsInfo;
        }
    }
}
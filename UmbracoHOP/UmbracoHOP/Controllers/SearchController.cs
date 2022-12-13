using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UmbracoHOP.Models;

namespace UmbracoHOP.Controllers;

public class SearchController : Controller
{
    private const string ApiUrl = "https://localhost:7209/";

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        var normalSearchPage = await ApiCaller("api/UmbracoInstall/GetAllUmbracoInstalls", "");
        
        return View(normalSearchPage);
    }
    
    public async Task<ActionResult> Search(IFormCollection formCollection, string searchString)
    {
        var searchOption = formCollection["SearchOptions"].ToString();
        ViewData["SearchString"] = searchString;
        List<UmbracoInstallsModel>? searchedOption = null;
    
        switch (searchOption)
        {
            case "Country":
                searchedOption = await ApiCaller("api/UmbracoInstall/GetCountry?country=", searchString);
                break;
            case "City":
                searchedOption = await ApiCaller("api/UmbracoInstall/GetCity?city=", searchString);
                break;
            case "Continent":
                searchedOption = await ApiCaller("api/UmbracoInstall/GetContinent?continent=", searchString);
                break;
            case "Version":
                searchedOption = await ApiCaller("api/UmbracoInstall/GetUmbracoVersion?version=", searchString);
                break;
            case "Package":
                // Lidt for tricky
                break;
        }
        
        return View(searchedOption);
    }


    private async Task<List<UmbracoInstallsModel>?> ApiCaller(string apiAction, string search)
    {
        var umbracoInstallsInfo = new List<UmbracoInstallsModel>();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ApiUrl);
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiAction + search);

            if (response.IsSuccessStatusCode)
            {
                var umbracoInstallsResponse = response.Content.ReadAsStringAsync().Result;

                umbracoInstallsInfo =
                    JsonConvert.DeserializeObject<List<UmbracoInstallsModel>>(umbracoInstallsResponse);
            }

            // foreach (var package in umbracoInstallsInfo)
            // {
            //     var jsonResult = (Json(package.Package));
            // }
            return umbracoInstallsInfo;
        }
    }
}
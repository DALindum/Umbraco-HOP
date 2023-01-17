using System.Data;
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
        var normalSearchPage = await ApiCaller("api/UmbracoInstall/GetAllUmbracoInstalls");

        ViewBag.TotalInstances = normalSearchPage?.Count ?? 0;

        return View(normalSearchPage);
    }
    
    public async Task<IActionResult> Search(IFormCollection formCollection)
    {
        var searchString = formCollection["SearchString"].ToString();
        var searchOption = formCollection["SearchOptions"].ToString();
        var fromDate = formCollection["FromDate"].ToString();
        var toDate = formCollection["ToDate"].ToString();
        var exportButton = formCollection["ExportButton"].Count();

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

        if (exportButton != 0)
        {
            string csv = string.Concat(searchedOption.Select(
                model =>
                    $"{model.Continent},{model.Country},{model.City},{model.Package},{model.Version},{model.Date}\n"));

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "UmbracoInstances.csv");
            
            // Mulig løsning
            // DataTable table = new DataTable("Instances");
            //
            // table.Columns.Add("Continent", typeof(string));
            // table.Columns.Add("Country", typeof(string));
            // table.Columns.Add("City", typeof(string));
            // table.Columns.Add("Package", typeof(string));
            // table.Columns.Add("Version", typeof(string));
            // table.Columns.Add("Date", typeof(string));
            //
            //
            // foreach (var instance in searchedOption)
            // {
            //
            //     DataRow row = table.NewRow();
            //     row["Continent"] = instance.Continent;
            //     row["Country"] = instance.Country;
            //     row["City"] = instance.City;
            //     row["Package"] = instance.Package;
            //     row["Version"] = instance.Version;
            //     row["Date"] = instance.Date;
            //
            //     table.Rows.Add(row);
            // }
        }
        ViewBag.TotalInstances = searchedOption?.Count ?? 0;
        ViewBag.SelectedFilter = searchOption ?? "Nothing";


        return View(searchedOption);
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
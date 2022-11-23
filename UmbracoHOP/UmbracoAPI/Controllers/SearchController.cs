using Microsoft.AspNetCore.Mvc;
using UmbracoAPI.Models;

namespace UmbracoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : Controller
{
    //Making 2 string arrays, so it will randomly add the 2 arrays content together
    private static readonly string[] Countries = new[]
    {
        "Denmark", "Sweden", "Norway", "Germany", "England"
    };

    private static readonly string[] Cities = new[]
    {
        "Odense", "Stockholm", "Oslo", "Berlin", "London"
    };


    // GET
    [HttpGet(Name = "GetSearch")]
    public IEnumerable<Search> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Search
        {
            Country = Countries[Random.Shared.Next(Countries.Length)],
            City = Cities[Random.Shared.Next(Cities.Length)],
            Continent = "EU",
            Versions = "Latest",
            Packages = "",
            Instances = 1,
            fromDate = DateTime.Now,
            toDate = new DateTime(2022, 12, 31),
        }).ToArray();
    }
}

// public int ID { get; set; }
// public string Country { get; set; }
// public string City { get; set; }
// public string Continent { get; set; }
// public string Versions { get; set; }
// public string Packages { get; set; }
// public int Instances { get; set; }
// public DateTime fromDate { get; set; }
// public DateTime toDate { get; set; }
// public string searchText { get; set; }
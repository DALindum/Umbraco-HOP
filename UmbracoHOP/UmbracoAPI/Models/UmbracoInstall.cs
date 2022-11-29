using System.Text.Json.Serialization;
using Azure;
using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace UmbracoAPI.Models;

public class UmbracoInstall : ITableEntity
{
    public string PartitionKey { get; set; }

    public string RowKey { get; set; }

    public string Date { get; set; }

    public Package Packages { get; set; }

    public string Version { get; set; }

    public string Continent { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    [property: JsonPropertyName("odata.etag")]
    public ETag ETag { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public UmbracoInstall(string continent, string country, string city)
    {
        this.Continent = continent;
        this.Country = country;
        this.City = city;
    }
    public class UmbracoInstallDBContext : DbContext
    {
        public DbSet<UmbracoInstall> UmbracoInstalls { get; set; }
    }
}
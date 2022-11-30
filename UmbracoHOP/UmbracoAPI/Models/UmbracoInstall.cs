using System.Text.Json.Serialization;
using Azure;
using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace UmbracoAPI.Models;

public class UmbracoInstall : ITableEntity
{
    public string PartitionKey { get; set; }

    public string RowKey { get; set; }

    public DateTime Date { get; set; }

    public string Package { get; set; }

    public string Version { get; set; }

    public string Continent { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    [property: JsonPropertyName("odata.etag")]
    public ETag ETag { get; set; }

    public DateTimeOffset? Timestamp { get; set; }
    
    public UmbracoInstall(DateTime date, string version, string continent, string country, string city, string package)
    {
        this.Date = date;
        this.Version = version;
        this.Continent = continent;
        this.Country = country;
        this.City = city;
        this.Package = package;
    }
    
    public class UmbracoInstallDBContext : DbContext
    {
        public DbSet<UmbracoInstall> UmbracoInstalls { get; set; }
    }
}
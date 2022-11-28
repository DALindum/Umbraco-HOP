using System.Text.Json.Serialization;
using Azure;
using Azure.Data.Tables;

namespace UmbracoAPI.Models;

public class UmbracoInstall : ITableEntity
{
    public string PartitionKey { get; set; }
    
    public string RowKey { get; set; }

    public string Date { get; set; }
    
    public string Packages { get; set; }
    
    public string Version { get; set; }

    public string Continent { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    [property:JsonPropertyName("odata.etag")]
    public ETag ETag { get; set; }

    public DateTimeOffset? Timestamp { get; set; }
}
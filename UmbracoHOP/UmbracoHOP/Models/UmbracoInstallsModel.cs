using System.Text.Json.Nodes;

namespace UmbracoHOP.Models;

public class UmbracoInstallsModel
{
    public int ID { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Continent { get; set; }

    public string Version { get; set; }
    
    public string Package { get; set; }

    public DateTime Date { get; set; }
}
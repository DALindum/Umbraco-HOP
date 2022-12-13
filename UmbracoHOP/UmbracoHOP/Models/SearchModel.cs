namespace UmbracoHOP.Models;

public class SearchModel
{
    public int ID { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Continent { get; set; }
    public string Versions { get; set; }
    public string Package { get; set; }
    public int Instances { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string Type { get; set; }
}

public enum SearchOptions
{
    SearchAll,
    Country,
    City,
    Continent,
    Version,
    PackageName
}
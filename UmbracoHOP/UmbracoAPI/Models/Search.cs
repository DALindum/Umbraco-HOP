namespace UmbracoAPI.Models;

public class Search
{
    public int ID { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Continent { get; set; }
    public string Versions { get; set; }
    public string Packages { get; set; }
    public int Instances { get; set; }
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }
    public string searchText { get; set; }
}
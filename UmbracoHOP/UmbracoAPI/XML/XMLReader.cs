using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace UmbracoAPI.XML;

public class XMLReader
{
    private string xmlPath = @"..\UmbracoAPI\XML\XMLConnection.xml";
    
    public string GetConnectionString()
    {
        XDocument doc = XDocument.Load(xmlPath);

        var elements = doc.Descendants("Config").Elements("connectionString");

        foreach (XElement element in elements)
        {
            return element.Attribute("value").Value;
        }

        return "Failed";
    }
}
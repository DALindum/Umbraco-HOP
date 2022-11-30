using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Table;
using UmbracoAPI.Models;
using UmbracoAPI.XML;

namespace UmbracoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UmbracoInstallController : ControllerBase
    {
        private List<UmbracoInstall> umbracoInstalls = new List<UmbracoInstall>();

        private XMLReader xmlReader = new XMLReader();

        // GET: api/UmbracoInstall
        // [HttpGet]
        // public IEnumerable<UmbracoInstall> Get()
        // {
        //     // GETS THE DATA FROM THE UMBRACO STRAGE DATABASE
        //     string currentDate = DateTime.Now.ToString("yyyyMMdd");
        //     string currentDateFriday = "20221125";
        //     var client = new TableClient(xmlReader.GetConnectionString(), getTableName(currentDateFriday));
        //     // Console.WriteLine($"Getting data for {currentDate}");
        //
        //     var installs = client.Query<UmbracoInstall>().DistinctBy(x => x.PartitionKey);
        //
        //     return installs;
        // }

        // GET: api/UmbracoInstall/5
        [HttpGet("{city}", Name = "Get")]
        public string Get(string city)
        {
            return "";
        }

        [HttpGet]
        public List<UmbracoInstall> GetAllUmbracoInstalls()
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB"))
            {
                connection.Open();

                string LoadVerConCoun = "SELECT date, packagename, packageversion, version, continent, country, city FROM UmbracoInstall inner join Packages P on UmbracoInstall.PK_UmbracoInstallID = P.FK_UmbracoInstallID";
                SqlCommand loadVerConCommand = new SqlCommand(LoadVerConCoun, connection);
                
                SqlDataReader dataReader = loadVerConCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    umbracoInstalls.Add(new UmbracoInstall((DateTime)dataReader[0],(string)dataReader[1],(string)dataReader[2],(string)dataReader[3],(string)dataReader[4],(string)dataReader[5],(string)dataReader[6]));
                    // string date, Package package, string version, string continent, string country, string city
                }

                connection.Close();

                return umbracoInstalls;
            }
        }

        string getTableName(string date)
        {
            return "activeinstallcount" + date;
        }
    }
}
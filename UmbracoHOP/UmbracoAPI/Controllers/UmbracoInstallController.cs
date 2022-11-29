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
        private List<UmbracoInstall> umbracoInstalls;

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
        public string GetUmbracoInstalls()
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB"))
            {
                List<string> Test = new List<string>();
                
                SqlDataReader reader;
                connection.Open();

                string LoadVerConCoun = "SELECT Continent, Country, City FROM UmbracoInstall";

                SqlCommand loadVerConCommand = new SqlCommand(LoadVerConCoun, connection);

                SqlDataReader dataReader = loadVerConCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    umbracoInstalls.Add(new UmbracoInstall((string)dataReader[0], (string)dataReader[1], (string)dataReader[2]));
                }

                return umbracoInstalls.ToString();
            }
        }

        string getTableName(string date)
        {
            return "activeinstallcount" + date;
        }
    }
}
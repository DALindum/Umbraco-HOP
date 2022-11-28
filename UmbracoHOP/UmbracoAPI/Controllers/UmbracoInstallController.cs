using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UmbracoAPI.Models;
using UmbracoAPI.XML;

namespace UmbracoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UmbracoInstallController : ControllerBase
    {
        private XMLReader xmlReader = new XMLReader();
        
        // GET: api/UmbracoInstall
        [HttpGet]
        public IEnumerable<UmbracoInstall> Get()
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string currentDateFriday = "20221125";
            var client = new TableClient(xmlReader.GetConnectionString(), getTableName(currentDateSaturday));
            // Console.WriteLine($"Getting data for {currentDate}");
            
            var installs = client.Query<UmbracoInstall>().DistinctBy(x => x.PartitionKey);
            
            return installs;
        }

        // GET: api/UmbracoInstall/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }



        string getTableName(string date)
        {
            return "activeinstallcount" + date;
        }
        
    }
}
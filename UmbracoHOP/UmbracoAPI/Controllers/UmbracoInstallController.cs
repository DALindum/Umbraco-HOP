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
    [Route("api/[controller]/[action]")]
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

        // GET: api/GetPackageName/GetPackageName=Packagename
        [HttpGet]
        public List<UmbracoInstall> GetPackageName(string packageName)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev"))
            {
                connection.Open();

                string query =
                    $"SELECT uInstall.Date, uInstall.Version, uInstall.Continent, uInstall.Country, uInstall.City, Packages = '[' + CONCAT('', (SELECT c.PackageName, c.PackageVersion FROM Packages c WHERE c.FK_UmbracoInstallID = uInstall.PK_UmbracoInstallID AND c.PackageName = '{packageName}' FOR JSON PATH, WITHOUT_ARRAY_WRAPPER))+']' FROM UmbracoInstall uInstall";

                SqlCommand queryCommand = new SqlCommand(query, connection);


                SqlDataReader dataReader = queryCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    if (dataReader[5].ToString()!.Contains(packageName))
                    {
                        umbracoInstalls.Add(new UmbracoInstall((DateTime)dataReader[0], (string)dataReader[1],
                            (string)dataReader[2], (string)dataReader[3], (string)dataReader[4],
                            (string)dataReader[5]));
                    }
                }

                return umbracoInstalls;
            }
        }
        
        // GET: api/GetPackageVersion/GetPackageVersion=Packagename
        [HttpGet]
        public List<UmbracoInstall> GetPackageVersion(string packageName, string packageVersion)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev"))
            {
                connection.Open();

                string query =
                    $"SELECT uInstall.Date, uInstall.Version, uInstall.Continent, uInstall.Country, uInstall.City, Packages = '[' + CONCAT('', (SELECT c.PackageName, c.PackageVersion FROM Packages c WHERE c.FK_UmbracoInstallID = uInstall.PK_UmbracoInstallID AND c.PackageName = '{packageName}' AND c.PackageVersion = '{packageVersion}' FOR JSON PATH, WITHOUT_ARRAY_WRAPPER))+']' FROM UmbracoInstall uInstall";

                SqlCommand queryCommand = new SqlCommand(query, connection);


                SqlDataReader dataReader = queryCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    if (dataReader[5].ToString()!.Contains(packageName) && dataReader[5].ToString()!.Contains(packageVersion))
                    {
                        umbracoInstalls.Add(new UmbracoInstall((DateTime)dataReader[0], (string)dataReader[1],
                            (string)dataReader[2], (string)dataReader[3], (string)dataReader[4],
                            (string)dataReader[5]));
                    }
                }

                return umbracoInstalls;
            }
        }

        // GET: api/GetUmbracoVersion/GetUmbracoVersion=UmbracoVersion
        [HttpGet]
        public List<UmbracoInstall> GetUmbracoVersion(string version)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev"))
            {
                connection.Open();

                string query =
                    $"SELECT h.Date, h.Version, h.Continent, h.Country, h.City, Packages ='['+ CONCAT('', (SELECT PackageName,PackageVersion FROM Packages c WHERE c.FK_UmbracoInstallID = h.PK_UmbracoInstallID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER))+ ']' FROM UmbracoInstall h WHERE h.Version='{version}'";

                SqlCommand queryCommand = new SqlCommand(query, connection);


                SqlDataReader dataReader = queryCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    umbracoInstalls.Add(new UmbracoInstall((DateTime)dataReader[0], (string)dataReader[1],
                        (string)dataReader[2], (string)dataReader[3], (string)dataReader[4], (string)dataReader[5]));
                }

                return umbracoInstalls;
            }
        }

        // GET: api/GetCity/GetCountry=Country
        [HttpGet]
        public List<UmbracoInstall> GetCountry(string country)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev"))
            {
                connection.Open();

                string query =
                    $"SELECT h.Date, h.Version, h.Continent, h.Country, h.City, Packages ='['+ CONCAT('', (SELECT PackageName,PackageVersion FROM Packages c WHERE c.FK_UmbracoInstallID = h.PK_UmbracoInstallID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER))+ ']' FROM UmbracoInstall h WHERE h.Country='{country}'";
                SqlCommand queryCommand = new SqlCommand(query, connection);

                SqlDataReader dataReader = queryCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    umbracoInstalls.Add(new UmbracoInstall((DateTime)dataReader[0], (string)dataReader[1],
                        (string)dataReader[2], (string)dataReader[3], (string)dataReader[4], (string)dataReader[5]));
                }

                return umbracoInstalls;
            }
        }

        // GET: api/GetCity/GetCity=City
        [HttpGet]
        public List<UmbracoInstall> GetCity(string city)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev"))
            {
                connection.Open();

                string query =
                    $"SELECT h.Date, h.Version, h.Continent, h.Country, h.City, Packages ='['+ CONCAT('', (SELECT PackageName,PackageVersion FROM Packages c WHERE c.FK_UmbracoInstallID = h.PK_UmbracoInstallID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER))+ ']' FROM UmbracoInstall h WHERE h.City='{city}'";
                SqlCommand queryCommand = new SqlCommand(query, connection);

                SqlDataReader dataReader = queryCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    umbracoInstalls.Add(new UmbracoInstall((DateTime)dataReader[0], (string)dataReader[1],
                        (string)dataReader[2], (string)dataReader[3], (string)dataReader[4], (string)dataReader[5]));
                }

                return umbracoInstalls;
            }
        }

        // GET: api/GetCity/GetContinent=Continent
        [HttpGet]
        public List<UmbracoInstall> GetContinent(string continent)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev"))
            {
                connection.Open();

                string query =
                    $"SELECT h.Date, h.Version, h.Continent, h.Country, h.City, Packages ='['+ CONCAT('', (SELECT PackageName,PackageVersion FROM Packages c WHERE c.FK_UmbracoInstallID = h.PK_UmbracoInstallID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER))+ ']' FROM UmbracoInstall h WHERE h.Continent='{continent}'";
                SqlCommand queryCommand = new SqlCommand(query, connection);

                SqlDataReader dataReader = queryCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    umbracoInstalls.Add(new UmbracoInstall((DateTime)dataReader[0], (string)dataReader[1],
                        (string)dataReader[2], (string)dataReader[3], (string)dataReader[4], (string)dataReader[5]));
                }

                return umbracoInstalls;
            }
        }

        // Gets every instance of Umbraco
        [HttpGet]
        public List<UmbracoInstall> GetAllUmbracoInstalls()
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev"))
            {
                connection.Open();

                string LoadVerConCoun =
                    @"SELECT h.Date, h.Version, h.Continent, h.Country, h.City, Packages ='['+ CONCAT('', (SELECT PackageName,PackageVersion FROM Packages c WHERE c.FK_UmbracoInstallID = h.PK_UmbracoInstallID FOR JSON PATH, WITHOUT_ARRAY_WRAPPER))+ ']' FROM UmbracoInstall h";
                SqlCommand loadVerConCommand = new SqlCommand(LoadVerConCoun, connection);

                SqlDataReader dataReader = loadVerConCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    umbracoInstalls.Add(new UmbracoInstall((DateTime)dataReader[0], (string)dataReader[1],
                        (string)dataReader[2], (string)dataReader[3], (string)dataReader[4], (string)dataReader[5]));
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
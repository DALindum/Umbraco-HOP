using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UmbracoHOP.Models;

namespace UmbracoHOP.Controllers;

public class LoginController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST
    [HttpPost]
    public IActionResult Login(LoginModel login)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev"))
            {
                string query = $"SELECT username, password FROM users WHERE username = {login.Username} and password = {login.Password}";
            
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", login.Username);
                    command.Parameters.AddWithValue("@password", login.Password);

                    connection.Open();
                    SqlDataAdapter adpt = new SqlDataAdapter(command);
                    DataSet dts = new DataSet();
                    adpt.Fill(dts);
                    connection.Close();
                }
            }
            
            return RedirectToAction("Homepage", "Home");
        }

        return View();
        
        /*
        if (ModelState.IsValid)
        {
            return RedirectToAction("Homepage", "Home");
        }

        return View();
         */
    }
}
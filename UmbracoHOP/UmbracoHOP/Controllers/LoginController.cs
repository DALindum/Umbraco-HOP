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
            SqlConnection connection = new SqlConnection(@"Server=(localdb)\UmbracoDev");
            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE username = '{login.Username}' and password = HASHBYTES('SHA2_256','{login.Password}')", connection);

            command.Parameters.AddWithValue("@username", login.Username);
            command.Parameters.AddWithValue("@password", login.Password);
            
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();

            if (dt.Rows.Count > 0)
            {
                return RedirectToAction("Homepage", "Home");
            }

            if (dt.Rows.Count == 0)
            {
                ViewBag.ErrorMessage = "Username or Password is incorrect";
            }
            
            
            
            
        }

        return View();
    }
}
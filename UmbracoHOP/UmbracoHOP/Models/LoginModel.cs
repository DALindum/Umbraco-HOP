using System.ComponentModel.DataAnnotations;

namespace UmbracoHOP.Models;

public class LoginModel
{
    public int ID { get; set; }
    
    [Required(ErrorMessage = "The username field cannot be empty")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters long")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "The password field cannot be empty")]
    public string Password { get; set; }
}
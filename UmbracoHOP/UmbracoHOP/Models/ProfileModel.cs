namespace UmbracoHOP.Models;

public class ProfileModel
{
    public int ID { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    public string NewPassword { get; set; }
}
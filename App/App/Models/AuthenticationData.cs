namespace App.Models;

public class AuthenticationData
{
    public string Email { get; set; }
    public string Password { get; set; }

    public AuthenticationData(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
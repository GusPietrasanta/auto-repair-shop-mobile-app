using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Dashboard
{
    private readonly HttpClient _client = new();
    
    public Dashboard()
    {
        InitializeComponent();

    }

    protected override async void OnAppearing()
    {
        var token = (string)Application.Current.Properties["token"];  
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var userName = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")!.Value;
        var mechanicId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "MechanicId")!.Value);

        Application.Current.Properties["userName"] = userName;
        Application.Current.Properties["mechanicId"] = mechanicId;
        
        
        UserName.Text = $"Welcome, {userName}!";
        
        string url = $"{Constants.BaseUrl}/v1/Appointment/Count"; 
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            string jobsCount = response.Content.ReadAsStringAsync().Result;
            JobsCount.Text = $"You have {jobsCount} jobs assigned today.";
        }
        else
        {
            JobsCount.Text = "Error :)";                
        }
    }
}
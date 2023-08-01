using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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
        
        var token = (string)Application.Current.Properties["token"];  
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var userName = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")!.Value;
        var mechanicId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "MechanicId")!.Value);

        Application.Current.Properties["userName"] = userName;
        Application.Current.Properties["mechanicId"] = mechanicId;

        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        _client.DefaultRequestHeaders.Accept.Add(contentType);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 

    }

    protected override async void OnAppearing()
    {
        UserName.Text = $"Welcome, {Application.Current.Properties["userName"]}!";
        
        string messageCountUrl = $"{Constants.BaseUrl}/v1/Appointment/Count"; 
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, messageCountUrl);

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

    private async void PostMessage_OnClicked(object sender, EventArgs e)
    {
        string postMessageUrl = $"{Constants.BaseUrl}/v1/Message";
        
        // Build the object
        MessageModel message = new MessageModel
        {
            Content = Message.Text,
            UserName = Application.Current.Properties["userName"].ToString(),
            PostedOn = DateTime.Now,
            Tag = Tag.SelectedItem.ToString()
        };

        // Build the post request
        StringContent content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json"); 

        var response = await _client.PostAsync(new Uri(postMessageUrl), content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert ("Success", "Message has been successfully sent.", "OK");
            Message.Text = "";
            Tag.SelectedIndex = -1;
        }
        else
        {
            await DisplayAlert ("Error", "There was an error sending your message, please try again.", "OK");
        }
    }

    private async void GoToJobs_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new JobsPage());
    }
}
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using App.Models;
using Xamarin.Forms;

namespace App;

public partial class MainPage
{
    private readonly HttpClient _client = new();
    private readonly string _tokenUrl = "/Authentication/token";
    
        
    public MainPage()
    {
        InitializeComponent();
        
        _client.Timeout = TimeSpan.FromSeconds(10);
    }

    private async void LogInButton_OnClicked(object sender, EventArgs e)
    {
        AuthenticationData userDetails = new AuthenticationData(UsernameEntry.Text, PasswordEntry.Text);

        Uri uri = new Uri(Constants.BaseUrl + _tokenUrl);

        string json = JsonSerializer.Serialize(userDetails);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _client.PostAsync(uri, content);
            
            if (response.IsSuccessStatusCode)
            {
                //Response.Text = response.Content.ReadAsStringAsync().Result;
                Application.Current.Properties["token"] = response.Content.ReadAsStringAsync().Result;
                await Navigation.PushAsync(new Dashboard());
            }
            else
            {
                await DisplayAlert ("Error", "Wrong log in information. Please try again.", "OK");               
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert ("Error", $"There is no connection with the server. {ex.Message}", "OK");
        }
    }
}
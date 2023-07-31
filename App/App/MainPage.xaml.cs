using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using App.Models;
using Xamarin.Forms;

namespace App;

public partial class MainPage
{
    private readonly HttpClient _client = new();
    private readonly string _url = "http://192.168.1.100:45500/api/Authentication/token";
    
        
    public MainPage()
    {
        InitializeComponent();
    }

    private async void LogInButton_OnClicked(object sender, EventArgs e)
    {
        UsernameTest.Text = UsernameEntry.Text;
        PasswordTest.Text = PasswordEntry.Text;

        AuthenticationData userDetails = new AuthenticationData(UsernameEntry.Text, PasswordEntry.Text);

        Uri uri = new Uri(_url);

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
                Response.Text = "Wrong log in information. Please try again.";                
            }
        }
        catch (Exception ex)
        {
            Response.Text = ex.Message;
        }
    }
}
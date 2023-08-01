using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ReportPage
{
    private readonly HttpClient _client = new();
    public ReportPage()
    {
        InitializeComponent();

        
        var token = (string)Application.Current.Properties["token"];  
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        _client.DefaultRequestHeaders.Accept.Add(contentType);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected override async void OnAppearing()
    {
        string vehicleDetailsUrl = $"{Constants.BaseUrl}/v1/Vehicle/{Constants.WorkingOn.VehicleId}";
        
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, vehicleDetailsUrl);
        
        var response = await _client.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;
            Vehicle vehicle = JsonSerializer.Deserialize<Vehicle>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            NumberPlate.Text = vehicle!.NumberPlate;
            MakeAndModel.Text = vehicle.Make + " - " + vehicle.Model;
            Year.Text = vehicle.Year;
            BodyType.Text = vehicle.BodyType;
            Engine.Text = vehicle.EngineDescription;
            FuelType.Text = vehicle.FuelType;
            TransmissionType.Text = vehicle.TransmissionType;
            Cylinders.Text = vehicle.Cylinders;
            Displacement.Text = vehicle.SizeLitres;
            VIN.Text = vehicle.Vin;

            if (vehicle.FirstVisit.ToShortDateString() == "1/1/0001")
            {
                FirstVisitLabel.Text = "First Visit To The Shop.";
                FirstVisitDate.Text = "";
            }
            else
            {
                FirstVisitLabel.Text = "First Visit To The Shop: ";
                FirstVisitDate.Text = vehicle.FirstVisit.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            await DisplayAlert("Error", "There was an error getting vehicle details from the server.", "Ok");
        }
    }
}
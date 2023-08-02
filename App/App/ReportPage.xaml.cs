using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        Session.CurrentReport = new Report
        {
            TimeStarted = DateTime.Now,
            Date = Session.CurrentAppointment.Date,
            VehicleId = Session.CurrentAppointment.VehicleId,
            MechanicId = Session.CurrentAppointment.MechanicId,
            AppointmentId = Session.CurrentAppointment.Id,
            CustomerId = Session.CurrentAppointment.CustomerId
        };
        
        var token = (string)Application.Current.Properties["token"];  
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        _client.DefaultRequestHeaders.Accept.Add(contentType);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected override async void OnAppearing()
    {
        string vehicleDetailsUrl = $"{Session.BaseUrl}/v1/Vehicle/{Session.CurrentAppointment.VehicleId}";
        
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
            Vin.Text = vehicle.Vin;

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


    private async void SaveReport_OnClicked(object sender, EventArgs e)
    {
        const string ok = "Inspected And Ok";
        const string average = "May Require Future Attention";
        const string bad = "Requires Immediate Attention";
        
        Session.CurrentReport.TimeFinished = DateTime.Now;
        
        Session.CurrentReport.Odometer = int.Parse(Odometer.Text);
        Session.CurrentReport.RoadTestComments = RoadTestComments.Text;
        Session.CurrentReport.GeneralComments = GeneralComments.Text;
        Session.CurrentReport.StoredFaultCodes = StoredFaultCodes.Text;

        if (AcOk.IsChecked)
        {
            Session.CurrentReport.AirConditioning = ok;
        }
        else if (AcAverage.IsChecked)
        {
            Session.CurrentReport.AirConditioning = average;
        }
        else if (AcAverage.IsChecked)
        {
            Session.CurrentReport.AirConditioning = bad;
        }
        
        if (LightsOk.IsChecked)
        {
            Session.CurrentReport.Lights = ok;
        }
        else if (LightsAverage.IsChecked)
        {
            Session.CurrentReport.Lights = average;
        }
        else if (LightsBad.IsChecked)
        {
            Session.CurrentReport.Lights = bad;
        }

        Session.CurrentReport.LightsComments = LightsComments.Text;
        
        // AirConditioning
        if (AcOk.IsChecked)
        {
            Session.CurrentReport.AirConditioning = ok;
        }
        else if (AcAverage.IsChecked)
        {
            Session.CurrentReport.AirConditioning = average;
        }
        else if (AcBad.IsChecked)
        {
            Session.CurrentReport.AirConditioning = bad;
        }

        // Lights
        if (LightsOk.IsChecked)
        {
            Session.CurrentReport.Lights = ok;
        }
        else if (LightsAverage.IsChecked)
        {
            Session.CurrentReport.Lights = average;
        }
        else if (LightsBad.IsChecked)
        {
            Session.CurrentReport.Lights = bad;
        }
        Session.CurrentReport.LightsComments = LightsComments.Text;

        // FrontWipers
        if (FrontWipersOk.IsChecked)
        {
            Session.CurrentReport.FrontWipers = ok;
        }
        else if (FrontWipersAverage.IsChecked)
        {
            Session.CurrentReport.FrontWipers = average;
        }
        else if (FrontWipersBad.IsChecked)
        {
            Session.CurrentReport.FrontWipers = bad;
        }

        // RearWiper
        if (RearWiperOk.IsChecked)
        {
            Session.CurrentReport.RearWiper = ok;
        }
        else if (RearWiperAverage.IsChecked)
        {
            Session.CurrentReport.RearWiper = average;
        }
        else if (RearWiperBad.IsChecked)
        {
            Session.CurrentReport.RearWiper = bad;
        }

        // Battery
        if (BatteryOk.IsChecked)
        {
            Session.CurrentReport.Battery = ok;
        }
        else if (BatteryAverage.IsChecked)
        {
            Session.CurrentReport.Battery = average;
        }
        else if (BatteryBad.IsChecked)
        {
            Session.CurrentReport.Battery = bad;
        }
        Session.CurrentReport.BatteryComments = BatteryComments.Text;

        // RHFTyre
        if (RHFTyreOk.IsChecked)
        {
            Session.CurrentReport.RhfTyre = ok;
        }
        else if (RHFTyreAverage.IsChecked)
        {
            Session.CurrentReport.RhfTyre = average;
        }
        else if (RHFTyreBad.IsChecked)
        {
            Session.CurrentReport.RhfTyre = bad;
        }

        // LHFTyre
        if (LHFTyreOk.IsChecked)
        {
            Session.CurrentReport.LhfTyre = ok;
        }
        else if (LHFTyreAverage.IsChecked)
        {
            Session.CurrentReport.LhfTyre = average;
        }
        else if (LHFTyreBad.IsChecked)
        {
            Session.CurrentReport.LhfTyre = bad;
        }

        // RHRTyre
        if (RHRTyreOk.IsChecked)
        {
            Session.CurrentReport.RhrTyre = ok;
        }
        else if (RHRTyreAverage.IsChecked)
        {
            Session.CurrentReport.RhrTyre = average;
        }
        else if (RHRTyreBad.IsChecked)
        {
            Session.CurrentReport.RhrTyre = bad;
        }

        // LHRTyre
        if (LHRTyreOk.IsChecked)
        {
            Session.CurrentReport.LhrTyre = ok;
        }
        else if (LHRTyreAverage.IsChecked)
        {
            Session.CurrentReport.LhrTyre = average;
        }
        else if (LHRTyreBad.IsChecked)
        {
            Session.CurrentReport.LhrTyre = bad;
        }

        Session.CurrentReport.TyresComments = TyresComments.Text;

        // NeedsAlignment
        Session.CurrentReport.NeedsAlignment = NeedsAlignment.IsChecked;

        // AirFilter
        if (AirFilterOk.IsChecked)
        {
            Session.CurrentReport.AirFilter = ok;
        }
        else if (AirFilterAverage.IsChecked)
        {
            Session.CurrentReport.AirFilter = average;
        }
        else if (AirFilterBad.IsChecked)
        {
            Session.CurrentReport.AirFilter = bad;
        }

        // CabinFilter
        if (CabinFilterOk.IsChecked)
        {
            Session.CurrentReport.CabinFilter = ok;
        }
        else if (CabinFilterAverage.IsChecked)
        {
            Session.CurrentReport.CabinFilter = average;
        }
        else if (CabinFilterBad.IsChecked)
        {
            Session.CurrentReport.CabinFilter = bad;
        }

        // DriveBelts
        if (DriveBeltsOk.IsChecked)
        {
            Session.CurrentReport.DriveBelts = ok;
        }
        else if (DriveBeltsAverage.IsChecked)
        {
            Session.CurrentReport.DriveBelts = average;
        }
        else if (DriveBeltsBad.IsChecked)
        {
            Session.CurrentReport.DriveBelts = bad;
        }

        // TimingBelt
        if (TimingBeltOk.IsChecked)
        {
            Session.CurrentReport.TimingBelt = ok;
        }
        else if (TimingBeltAverage.IsChecked)
        {
            Session.CurrentReport.TimingBelt = average;
        }
        else if (TimingBeltBad.IsChecked)
        {
            Session.CurrentReport.TimingBelt = bad;
        }

        // Radiator
        if (RadiatorOk.IsChecked)
        {
            Session.CurrentReport.Radiator = ok;
        }
        else if (RadiatorAverage.IsChecked)
        {
            Session.CurrentReport.Radiator = average;
        }
        else if (RadiatorBad.IsChecked)
        {
            Session.CurrentReport.Radiator = bad;
        }

        // Hoses
        if (HosesOk.IsChecked)
        {
            Session.CurrentReport.Hoses = ok;
        }
        else if (HosesAverage.IsChecked)
        {
            Session.CurrentReport.Hoses = average;
        }
        else if (HosesBad.IsChecked)
        {
            Session.CurrentReport.Hoses = bad;
        }
        Session.CurrentReport.HosesComments = HosesComments.Text;

        // EngineOil
        if (EngineOilOk.IsChecked)
        {
            Session.CurrentReport.EngineOil = ok;
        }
        else if (EngineOilAverage.IsChecked)
        {
            Session.CurrentReport.EngineOil = average;
        }
        else if (EngineOilBad.IsChecked)
        {
            Session.CurrentReport.EngineOil = bad;
        }

        // Coolant
        if (CoolantOk.IsChecked)
        {
            Session.CurrentReport.Coolant = ok;
        }
        else if (CoolantAverage.IsChecked)
        {
            Session.CurrentReport.Coolant = average;
        }
        else if (CoolantBad.IsChecked)
        {
            Session.CurrentReport.Coolant = bad;
        }

        // BrakeFluid
        if (BrakeFluidOk.IsChecked)
        {
            Session.CurrentReport.BrakeFluid = ok;
        }
        else if (BrakeFluidAverage.IsChecked)
        {
            Session.CurrentReport.BrakeFluid = average;
        }
        else if (BrakeFluidBad.IsChecked)
        {
            Session.CurrentReport.BrakeFluid = bad;
        }

        // PowerSteeringFluid
        if (PowerSteeringFluidOk.IsChecked)
        {
            Session.CurrentReport.PowerSteeringFluid = ok;
        }
        else if (PowerSteeringFluidAverage.IsChecked)
        {
            Session.CurrentReport.PowerSteeringFluid = average;
        }
        else if (PowerSteeringFluidBad.IsChecked)
        {
            Session.CurrentReport.PowerSteeringFluid = bad;
        }

        // TransmissionFluid
        if (TransmissionFluidOk.IsChecked)
        {
            Session.CurrentReport.TransmissionFluid = ok;
        }
        else if (TransmissionFluidAverage.IsChecked)
        {
            Session.CurrentReport.TransmissionFluid = average;
        }
        else if (TransmissionFluidBad.IsChecked)
        {
            Session.CurrentReport.TransmissionFluid = bad;
        }

        // WindscreenWasherFluid
        if (WindscreenWasherFluidOk.IsChecked)
        {
            Session.CurrentReport.WindscreenWasherFluid = ok;
        }
        else if (WindscreenWasherFluidAverage.IsChecked)
        {
            Session.CurrentReport.WindscreenWasherFluid = average;
        }
        else if (WindscreenWasherFluidBad.IsChecked)
        {
            Session.CurrentReport.WindscreenWasherFluid = bad;
        }
        Session.CurrentReport.FluidsComments = FluidsComments.Text;

        // SparkPlugs
        if (SparkPlugsOk.IsChecked)
        {
            Session.CurrentReport.SparkPlugs = ok;
        }
        else if (SparkPlugsAverage.IsChecked)
        {
            Session.CurrentReport.SparkPlugs = average;
        }
        else if (SparkPlugsBad.IsChecked)
        {
            Session.CurrentReport.SparkPlugs = bad;
        }

        // FuelFilter
        if (FuelFilterOk.IsChecked)
        {
            Session.CurrentReport.FuelFilter = ok;
        }
        else if (FuelFilterAverage.IsChecked)
        {
            Session.CurrentReport.FuelFilter = average;
        }
        else if (FuelFilterBad.IsChecked)
        {
            Session.CurrentReport.FuelFilter = bad;
        }

        // FrontSuspension
        if (FrontSuspensionOk.IsChecked)
        {
            Session.CurrentReport.FrontSuspension = ok;
        }
        else if (FrontSuspensionAverage.IsChecked)
        {
            Session.CurrentReport.FrontSuspension = average;
        }
        else if (FrontSuspensionBad.IsChecked)
        {
            Session.CurrentReport.FrontSuspension = bad;
        }
        Session.CurrentReport.FrontSuspensionComments = FrontSuspensionComments.Text;

        // RearSuspension
        if (RearSuspensionOk.IsChecked)
        {
            Session.CurrentReport.RearSuspension = ok;
        }
        else if (RearSuspensionAverage.IsChecked)
        {
            Session.CurrentReport.RearSuspension = average;
        }
        else if (RearSuspensionBad.IsChecked)
        {
            Session.CurrentReport.RearSuspension = bad;
        }
        Session.CurrentReport.RearSuspensionComments = RearSuspensionComments.Text;

        // FrontBrakes
        if (FrontBrakesOk.IsChecked)
        {
            Session.CurrentReport.FrontBrakes = ok;
        }
        else if (FrontBrakesAverage.IsChecked)
        {
            Session.CurrentReport.FrontBrakes = average;
        }
        else if (FrontBrakesBad.IsChecked)
        {
            Session.CurrentReport.FrontBrakes = bad;
        }
        Session.CurrentReport.FrontBrakesComments = FrontBrakesComments.Text;

        // RearBrakes
        if (RearBrakesOk.IsChecked)
        {
            Session.CurrentReport.RearBrakes = ok;
        }
        else if (RearBrakesAverage.IsChecked)
        {
            Session.CurrentReport.RearBrakes = average;
        }
        else if (RearBrakesBad.IsChecked)
        {
            Session.CurrentReport.RearBrakes = bad;
        }
        Session.CurrentReport.RearBrakesComments = RearBrakesComments.Text;

        // Exhaust
        if (ExhaustOk.IsChecked)
        {
            Session.CurrentReport.Exhaust = ok;
        }
        else if (ExhaustAverage.IsChecked)
        {
            Session.CurrentReport.Exhaust = average;
        }
        else if (ExhaustBad.IsChecked)
        {
            Session.CurrentReport.Exhaust = bad;
        }
        Session.CurrentReport.ExhaustComments = ExhaustComments.Text;

        // Drivetrain
        if (DrivetrainOk.IsChecked)
        {
            Session.CurrentReport.Drivetrain = ok;
        }
        else if (DrivetrainAverage.IsChecked)
        {
            Session.CurrentReport.Drivetrain = average;
        }
        else if (DrivetrainBad.IsChecked)
        {
            Session.CurrentReport.Drivetrain = bad;
        }

        Session.CurrentReport.DrivetrainComments = DrivetrainComments.Text;

        // OilLeaks
        if (OilLeaksOk.IsChecked)
        {
            Session.CurrentReport.OilLeaks = ok;
        }
        else if (OilLeaksAverage.IsChecked)
        {
            Session.CurrentReport.OilLeaks = average;
        }
        else if (OilLeaksBad.IsChecked)
        {
            Session.CurrentReport.OilLeaks = bad;
        }
        Session.CurrentReport.OilLeaksComments = OilLeaksComments.Text;

        // CoolantLeaks
        if (CoolantLeaksOk.IsChecked)
        {
            Session.CurrentReport.CoolantLeaks = ok;
        }
        else if (CoolantLeaksAverage.IsChecked)
        {
            Session.CurrentReport.CoolantLeaks = average;
        }
        else if (CoolantLeaksBad.IsChecked)
        {
            Session.CurrentReport.CoolantLeaks = bad;
        }
        Session.CurrentReport.CoolantLeaksComments = CoolantLeaksComments.Text;

        // OtherLeaksComments
        Session.CurrentReport.OtherLeaksComments = OtherLeaksComments.Text;

        // Other
        Session.CurrentReport.Other = OtherComments.Text;

        // OtherInspectionComments
        Session.CurrentReport.OtherInspectionComments = OtherInspectionComments.Text;
        
        
        string reportUrl = $"{Session.BaseUrl}/v1/Report";

        // Build the post request
        StringContent content = new StringContent(JsonSerializer.Serialize(Session.CurrentReport), Encoding.UTF8, "application/json"); 

        var reportResponse = await _client.PostAsync(new Uri(reportUrl), content);
        
        // Try to send the report
        if (reportResponse.IsSuccessStatusCode)
        {
            await DisplayAlert ("Success", "Report has been successfully sent.", "OK");
            // If it's successful, mark the appointment as complete
            var markReportAsCompletedUrl = $"{Session.BaseUrl}/v1/Appointment/Completed/{Session.CurrentAppointment.Id}";
            var completedAppointmentResponse = await _client.PutAsync(markReportAsCompletedUrl,  null);

            if (completedAppointmentResponse.IsSuccessStatusCode)
            {
                Session.CurrentReport = new Report();
                await Navigation.PushAsync(new Dashboard());
            }
            else
            {
                await DisplayAlert ("Error", "There was an error marking your report as completed.", "OK");
            }
        }
        else
        {
            await DisplayAlert ("Error", "There was an error sending your report, please try again.", "OK");
        }
    }
}
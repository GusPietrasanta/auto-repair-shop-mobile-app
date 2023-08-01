using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ReportPage : ContentPage
{
    public ReportPage()
    {
        InitializeComponent();

        VehicleModel.Text = Constants.WorkingOn.Make + " " + Constants.WorkingOn.Model;
        AppointmentId.Text = Constants.WorkingOn.Id.ToString();
    }
}
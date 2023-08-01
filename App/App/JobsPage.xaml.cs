﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class JobsPage : ContentPage
{
     private readonly HttpClient _client = new();

    ObservableCollection<DetailedAppointment> _jobs = new ObservableCollection<DetailedAppointment>();
    public ObservableCollection<DetailedAppointment> Jobs { get { return _jobs; }}
    
    public JobsPage()
    {
        InitializeComponent();
        BindingContext = this;
        
        JobsView.ItemsSource = _jobs;

        
        // _jobs.Add(new DetailedAppointment{ FirstName = "Elto"});
        // _jobs.Add(new DetailedAppointment{ FirstName = "Tito"});
        // _jobs.Add(new DetailedAppointment{ FirstName = "Another One"});

        var token = (string)Application.Current.Properties["token"];  
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        _client.DefaultRequestHeaders.Accept.Add(contentType);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
    }

     protected override async void OnAppearing()
     {
         _jobs = new ObservableCollection<DetailedAppointment>();
         
         string getAppointmentsUrl = $"{Constants.BaseUrl}/v1/Appointment/";
         using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, getAppointmentsUrl);
         
         var response = await _client.SendAsync(httpRequestMessage);

         if (response.IsSuccessStatusCode)
         {
             JobsView.ItemsSource = _jobs;
             string json = response.Content.ReadAsStringAsync().Result;
             var newJobs = JsonSerializer.Deserialize<List<DetailedAppointment>>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
             foreach (var n in newJobs)
             {
                 _jobs.Add(n);
             }
         }
     }

     private async void StartJob_OnClicked(object sender, EventArgs e)
     {
         var menuItem = sender as Button;
         int selectedItem = (int)menuItem!.CommandParameter;
         Constants.WorkingOn = _jobs.FirstOrDefault(c => c.Id == selectedItem);
         await Navigation.PushAsync(new ReportPage());
     }
}
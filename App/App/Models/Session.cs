namespace App.Models;

public static class Session
{
    public static string BaseUrl { get; } = "http://ec2-13-239-17-109.ap-southeast-2.compute.amazonaws.com:8000/api";
    
    public static DetailedAppointment CurrentAppointment { get; set; }

    public static Report CurrentReport { get; set; } = new();
}
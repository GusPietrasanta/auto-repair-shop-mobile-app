namespace App.Models;

public static class Session
{
    public static string BaseUrl { get; } = "http://192.168.1.100:45500/api";
    
    public static DetailedAppointment CurrentAppointment { get; set; }

    public static Report CurrentReport { get; set; } = new();
}
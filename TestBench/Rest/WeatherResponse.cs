using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TestBench.Rest;

[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Deserialization")]
public class WeatherResponse
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Temperature { get; set; }
    public string WeatherDescription { get; set; }
    public int Humidity { get; set; }
    public int WindSpeed { get; set; }
    public List<Forecast> Forecast { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Deserialization")]
public class Forecast
{
    public string Date { get; set; }
    public int Temperature { get; set; }
    public string WeatherDescription { get; set; }
    public int Humidity { get; set; }
    public int WindSpeed { get; set; }
}
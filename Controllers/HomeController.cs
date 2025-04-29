using System.Diagnostics;
using AzureMonitorDemo.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;

namespace AzureMonitorDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TelemetryClient telemetryClient;

    public HomeController(ILogger<HomeController> logger, TelemetryClient telemetryClient)
    {
        _logger = logger;
        this.telemetryClient = telemetryClient;
    }

    public IActionResult Index()
    {
        telemetryClient.TrackTrace("Index page visited", SeverityLevel.Information);
        return View();
    }

    public IActionResult Privacy(int seconds)
    {
        telemetryClient.TrackEvent("Privacy page visited 1");
        return View();
    }

    public IActionResult GetWeather(string city)
    {
        telemetryClient.TrackEvent("Weather page visited with properties", new Dictionary<string, string> { { "city", city } });
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string message)
    {
        telemetryClient.TrackTrace("Inside Error Action", SeverityLevel.Error, new Dictionary<string, string> { { "seconds", message } });
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

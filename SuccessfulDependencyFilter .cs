using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.DataContracts;

namespace AzureMonitorDemo;

public class SuccessfulDependencyFilter : ITelemetryProcessor
{
    private ITelemetryProcessor Next { get; set; }

    // next will point to the next TelemetryProcessor in the chain.
    public SuccessfulDependencyFilter(ITelemetryProcessor next)
    {
        Next = next;
    }

    public void Process(ITelemetry item)
    {
        // To filter out an item, return without calling the next processor.
        if (!OKtoSend(item)) { return; }

        Next.Process(item);
    }

    // Example: replace with your own criteria.
    private static bool OKtoSend (ITelemetry item)
    {
        var dependency = item as DependencyTelemetry;
        if (dependency == null) return true;

        return dependency.Success != true;
    }
}
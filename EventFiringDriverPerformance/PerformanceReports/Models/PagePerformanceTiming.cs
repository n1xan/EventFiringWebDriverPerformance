using Newtonsoft.Json;

namespace EventFiringDriverPerformance.PerformanceReports.Models;

/// <summary>
/// Class created by the specification set in Performance Navigation Timing:
/// https://w3c.github.io/navigation-timing/#sec-PerformanceNavigationTiming
/// </summary>
public class PagePerformanceTiming
{
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("entryType", NullValueHandling = NullValueHandling.Ignore)]
    public string EntryType { get; set; }

    [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
    public double StartTime { get; set; }

    [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
    public double Duration { get; set; }

    [JsonProperty("initiatorType", NullValueHandling = NullValueHandling.Ignore)]
    public string InitiatorType { get; set; }

    [JsonProperty("nextHopProtocol", NullValueHandling = NullValueHandling.Ignore)]
    public string NextHopProtocol { get; set; }

    [JsonProperty("workerStart", NullValueHandling = NullValueHandling.Ignore)]
    public double WorkerStart { get; set; }

    [JsonProperty("redirectStart", NullValueHandling = NullValueHandling.Ignore)]
    public double RedirectStart { get; set; }

    [JsonProperty("redirectEnd", NullValueHandling = NullValueHandling.Ignore)]
    public double RedirectEnd { get; set; }

    [JsonProperty("fetchStart", NullValueHandling = NullValueHandling.Ignore)]
    public double FetchStart { get; set; }

    [JsonProperty("domainLookupStart", NullValueHandling = NullValueHandling.Ignore)]
    public double DomainLookupStart { get; set; }

    [JsonProperty("domainLookupEnd", NullValueHandling = NullValueHandling.Ignore)]
    public double DomainLookupEnd { get; set; }

    [JsonProperty("connectStart", NullValueHandling = NullValueHandling.Ignore)]
    public double ConnectStart { get; set; }

    [JsonProperty("connectEnd", NullValueHandling = NullValueHandling.Ignore)]
    public double ConnectEnd { get; set; }

    [JsonProperty("secureConnectionStart", NullValueHandling = NullValueHandling.Ignore)]
    public double SecureConnectionStart { get; set; }

    [JsonProperty("requestStart", NullValueHandling = NullValueHandling.Ignore)]
    public double RequestStart { get; set; }

    [JsonProperty("responseStart", NullValueHandling = NullValueHandling.Ignore)]
    public double ResponseStart { get; set; }

    [JsonProperty("responseEnd", NullValueHandling = NullValueHandling.Ignore)]
    public double ResponseEnd { get; set; }

    [JsonProperty("transferSize", NullValueHandling = NullValueHandling.Ignore)]
    public double TransferSize { get; set; }

    [JsonProperty("encodedBodySize", NullValueHandling = NullValueHandling.Ignore)]
    public double EncodedBodySize { get; set; }

    [JsonProperty("decodedBodySize", NullValueHandling = NullValueHandling.Ignore)]
    public double DecodedBodySize { get; set; }

    [JsonProperty("serverTiming", NullValueHandling = NullValueHandling.Ignore)]
    public object[] ServerTiming { get; set; }

    [JsonProperty("workerTiming", NullValueHandling = NullValueHandling.Ignore)]
    public object[] WorkerTiming { get; set; }

    [JsonProperty("unloadEventStart", NullValueHandling = NullValueHandling.Ignore)]
    public double UnloadEventStart { get; set; }

    [JsonProperty("unloadEventEnd", NullValueHandling = NullValueHandling.Ignore)]
    public double UnloadEventEnd { get; set; }

    [JsonProperty("domInteractive", NullValueHandling = NullValueHandling.Ignore)]
    public double DomInteractive { get; set; }

    [JsonProperty("domContentLoadedEventStart", NullValueHandling = NullValueHandling.Ignore)]
    public double DomContentLoadedEventStart { get; set; }

    [JsonProperty("domContentLoadedEventEnd", NullValueHandling = NullValueHandling.Ignore)]
    public double DomContentLoadedEventEnd { get; set; }

    [JsonProperty("domComplete", NullValueHandling = NullValueHandling.Ignore)]
    public double DomComplete { get; set; }

    [JsonProperty("loadEventStart", NullValueHandling = NullValueHandling.Ignore)]
    public double LoadEventStart { get; set; }

    [JsonProperty("loadEventEnd", NullValueHandling = NullValueHandling.Ignore)]
    public double LoadEventEnd { get; set; }

    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; }

    [JsonProperty("redirectCount", NullValueHandling = NullValueHandling.Ignore)]
    public double RedirectCount { get; set; }
}
namespace EventFiringDriverPerformance.PerformanceReports.Models;

public class PagePerformanceData
{
    public string PageUrl { get; set; }

    public string PageTitle { get; set; }

    public double ReadyMeasure { get; set; }

    public double JSHeapMemoryUsed { get; set; }

    /// <summary>
    /// https://developer.mozilla.org/en-US/docs/Web/API/PerformanceEntry/duration
    /// </summary>
    public double PageLoadTime => PagePerformanceTiming.Duration;

    /// <summary>
    /// https://developer.mozilla.org/en-US/docs/Web/API/PerformanceNavigationTiming/domInteractive
    /// </summary>
    public double DOMInteractive => PagePerformanceTiming.DomInteractive;

    /// <summary>
    /// https://w3c.github.io/navigation-timing/#dom-performancetiming-secureconnectionstart
    /// </summary>
    public double SSLNegotiationTime => PagePerformanceTiming.ResponseStart - PagePerformanceTiming.SecureConnectionStart;

    /// <summary>
    /// https://w3c.github.io/navigation-timing/timestamp-diagram.svg
    /// </summary>
    public double ContentDownloadTime => PagePerformanceTiming.ResponseEnd - PagePerformanceTiming.ResponseStart;

    /// <summary>
    /// https://w3c.github.io/navigation-timing/timestamp-diagram.svg
    /// </summary>
    public double TimeToFirstByte => PagePerformanceTiming.ResponseStart - PagePerformanceTiming.ConnectStart;

    /// <summary>
    /// https://w3c.github.io/navigation-timing/#dom-performancetiming-domcomplete
    /// </summary>
    public double DOMComplete => PagePerformanceTiming.DomComplete;

    /// <summary>
    /// https://w3c.github.io/navigation-timing/#dom-performancetiming-domcontentloadedeventend
    /// </summary>
    public double DOMContentLoadedTime => PagePerformanceTiming.DomContentLoadedEventEnd - PagePerformanceTiming.ConnectStart;

    /// <summary>
    /// https://w3c.github.io/navigation-timing/#sec-PerformanceNavigationTiming
    /// </summary>
    public PagePerformanceTiming PagePerformanceTiming { get; set; }
    //
    // public override string ToString() => this.Stringify();
}
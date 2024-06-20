using EventFiringDriverPerformance.Driver.Cdp;
using EventFiringDriverPerformance.PerformanceReports;
using EventFiringDriverPerformance.PerformanceReports.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace EventFiringDriverPerformance.Driver.Handlers;

public class WebDriverDevToolsEventsHandler
{
    private IWebDriver _driver;
    private DevToolsCommandExecutor _devToolsCommandExecutor;
    private PerformanceReportService _performanceReportService;
    public WebDriverDevToolsEventsHandler(WebDriverService driverService)
    {
        _driver = driverService.GetDriver();
        _devToolsCommandExecutor = driverService.GetDevTools();
        _performanceReportService = driverService.GetPerformanceReportService();
        _performanceReportService.SetReportName("DevTools Events Report");
    }

    public void EventDriverOnNavigated(object? sender, WebDriverNavigationEventArgs e)
    {
        RecordPerformanceMetrics();
    }
    
    public void EventDriverOnElementClicked(object? sender, WebElementEventArgs e)
    {
        RecordPerformanceMetrics();
    }

    private void RecordPerformanceMetrics()
    {
        String url = _driver.Url;
        String title = _driver.Title;
        var resources = _devToolsCommandExecutor.GetPageResources();
        var metrics = _devToolsCommandExecutor.GetPerformanceMetrics();
        
        double sum = resources.Where(r => r.ContentSize.HasValue).Sum(r => r.ContentSize.Value);
        
        // Console.WriteLine(
        //     $"Resources for Page: {url} [{resources.Length}] [Size: {sum/100} KBytes]: \n ->{ String.Join("\n ->", resources.ToList().Select(res => res.Url.ToString() + " | Size: " + res.ContentSize + " | LastModified: " + res.LastModified  + " | Type: " + res.Type))}");
        
        var pagePerformanceTiming = new PagePerformanceTiming();
        pagePerformanceTiming.TransferSize = sum;
        pagePerformanceTiming.DomContentLoadedEventEnd = metrics.DomContentLoaded * 1000;
        
        _performanceReportService.AddDataPoint(url, title, pagePerformanceTiming, (metrics.DomContentLoaded - metrics.NavigationStart) * 1000, metrics.JSHeapUsedSize);
    }
}
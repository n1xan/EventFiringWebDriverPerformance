using EventFiringDriverPerformance.Driver.Cdp;
using EventFiringDriverPerformance.PerformanceReports;
using EventFiringDriverPerformance.PerformanceReports.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace EventFiringDriverPerformance.Driver.Handlers;

public class WebDriverDevToolsEventsHandler
{
    private readonly IWebDriver _driver;
    private readonly DevToolsCommandExecutor _devToolsCommandExecutor;
    private readonly PerformanceReportService _performanceReportService;
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
        
        double transferSize = 
            resources.Where(r => 
                r.ContentSize.HasValue).Sum(r => r.ContentSize!.Value);
        
        var pagePerformanceTiming = new PagePerformanceTiming
        {
            TransferSize = transferSize,
            DomContentLoadedEventEnd = metrics.DomContentLoaded * 1000
        };
        var loadTime = (metrics.DomContentLoaded - metrics.NavigationStart) * 1000;

        _performanceReportService.AddDataPoint(url, title, pagePerformanceTiming, loadTime, metrics.JSHeapUsedSize);
    }
}
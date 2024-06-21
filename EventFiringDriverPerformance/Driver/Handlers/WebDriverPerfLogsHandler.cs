using EventFiringDriverPerformance.PerformanceReports;
using EventFiringDriverPerformance.PerformanceReports.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace EventFiringDriverPerformance.Driver.Handlers;

public class WebDriverPerfLogsHandler
{
    private IWebDriver _driver;
    private PerformanceReportService _performanceReportService;
    public WebDriverPerfLogsHandler(WebDriverService driverService)
    {
        _driver = driverService.GetDriver();
        _performanceReportService = driverService.GetPerformanceReportService();
        _performanceReportService.SetReportName("Driver Performance Logs Report");
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
        var logs = _driver.Manage().Logs.GetLog("performance");
        if (!logs.Any(e => e.Message.Contains("Page.frameStoppedLoading")))
        {
            return;
        }
        
        while (logs.LastOrDefault(e => e.Message.Contains("Page.frameStoppedLoading")) == null)
        {
            Console.WriteLine("Waiting for Page Events");
            logs = _driver.Manage().Logs.GetLog("performance");
        }
        
        var pageLoadTime = 
            logs.LastOrDefault(e => e.Message.Contains("Page.frameStoppedLoading"))!.Timestamp - 
            logs.FirstOrDefault(e => e.Message.Contains("Page.frameStartedLoading"))!.Timestamp;
        
        var perTiming = new PagePerformanceTiming() { };
        
        _performanceReportService.AddDataPoint(url, title, perTiming, pageLoadTime.TotalMilliseconds, 0);
    }
}
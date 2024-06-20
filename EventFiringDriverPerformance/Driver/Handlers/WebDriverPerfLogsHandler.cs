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
        if (!logs.Where(e => e.Message.Contains("Page.frameStoppedLoading")).Any())
        {
            return;
        }
        
        while (logs.Where(e => e.Message.Contains("Page.frameStoppedLoading")).LastOrDefault() == null)
        {
            Console.WriteLine("Waiting for Page Events");
            logs = _driver.Manage().Logs.GetLog("performance");
        }
        
        var timeSpan = (logs.Where(e => e.Message.Contains("Page.frameStoppedLoading")).LastOrDefault().Timestamp - logs.Where(e => e.Message.Contains("Page.frameStartedLoading")).FirstOrDefault().Timestamp);
        var perTiming = new PagePerformanceTiming() { };
        
        _performanceReportService.AddDataPoint(url, title, perTiming, timeSpan.TotalMilliseconds, 0);
    }
}
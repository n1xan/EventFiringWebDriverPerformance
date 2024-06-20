using EventFiringDriverPerformance.PerformanceReports;
using EventFiringDriverPerformance.PerformanceReports.Models;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace EventFiringDriverPerformance.Driver.Handlers;


public class WebDriverJsHandler
{
    private IWebDriver _driver;
    private PerformanceReportService _performanceReportService;

    public WebDriverJsHandler(WebDriverService driverService)
    {
        _driver = driverService.GetDriver();
        _performanceReportService = driverService.GetPerformanceReportService();
        _performanceReportService.SetReportName("JS Performance Timing Report");
    }

    public void EventDriverOnNavigated(object? sender, WebDriverNavigationEventArgs e)
    {
        RecordPerformanceMetrics();
    }
    
    public void EventDriverOnElementClicked(object? sender, WebElementEventArgs e)
    {
        RecordPerformanceMetrics();
    }
    
    private void RecordPerformanceMetrics(string pageLoadFormula = "performance.timing.loadEventEnd - performance.timing.connectStart")
    {
        try
        {
            var jsExecutor = (IJavaScriptExecutor)_driver;
            string pageUrl = _driver.Url;
            var pageTitle = _driver.Title;
            
            string? readyMeasure = jsExecutor.ExecuteScript($"return {pageLoadFormula}").ToString();
            string? jsHeapSize = jsExecutor.ExecuteScript($"return performance.memory.usedJSHeapSize").ToString();
            string? performanceTiming = jsExecutor
                .ExecuteScript(
                    "return JSON.stringify(performance.getEntriesByType('navigation')[performance.getEntriesByType('navigation').length - 1])")
                .ToString();
                
            var readyMeasureTime = double.Parse(readyMeasure ?? "0");
            var jsHeapMemoryUsed = double.Parse(jsHeapSize ?? "0");

            var pagePerformanceTiming = JsonConvert.DeserializeObject<PagePerformanceTiming>(performanceTiming);
            _performanceReportService.AddDataPoint(pageUrl, pageTitle, pagePerformanceTiming, readyMeasureTime, jsHeapMemoryUsed);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(
                $"Failed to fetch performance data from the page due to error: {e.Message}.");
        }
    }
}
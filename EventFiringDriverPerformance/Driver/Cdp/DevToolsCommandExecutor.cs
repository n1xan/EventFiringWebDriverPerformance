using System.Reflection;
using OpenQA.Selenium.DevTools;
using Performance = OpenQA.Selenium.DevTools.V123.Performance;
using Page = OpenQA.Selenium.DevTools.V123.Page;

namespace EventFiringDriverPerformance.Driver.Cdp;

public class DevToolsCommandExecutor(DevToolsSession devTools)
{
    public PagePerformanceMetrics GetPerformanceMetrics()
    {
        devTools.SendCommand(new Performance.EnableCommandSettings());
        var metricsResponse = devTools.SendCommand<Performance.GetMetricsCommandSettings, Performance.GetMetricsCommandResponse>(new Performance.GetMetricsCommandSettings()).Result;
        devTools.SendCommand(new Performance.DisableCommandSettings());
        
        return ParseDevToolsMetricsResponse(metricsResponse.Metrics);
    }

    public Page.FrameResource[] GetPageResources()
    {
        devTools.SendCommand(new Page.EnableCommandSettings());
        var resourcesResponse = devTools.SendCommand<Page.GetResourceTreeCommandSettings, Page.GetResourceTreeCommandResponse>(new Page.GetResourceTreeCommandSettings()).Result;
        devTools.SendCommand(new Page.DisableCommandSettings());
        
        return resourcesResponse.FrameTree.Resources;
    }

    private PagePerformanceMetrics ParseDevToolsMetricsResponse(Performance.Metric[] metrics)
    {
        var pagePerformanceMetrics = new PagePerformanceMetrics();

        foreach (var metric in metrics)
        {
            var modelField = typeof(PagePerformanceMetrics).GetRuntimeFields().First(prop => prop.Name.Equals(metric.Name));
            modelField.SetValue(pagePerformanceMetrics, metric.Value);
        }
        
        return pagePerformanceMetrics;
    }
}
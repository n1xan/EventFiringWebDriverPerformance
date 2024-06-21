using System.Diagnostics;
using EventFiringDriverPerformance.PerformanceReports.Models;

namespace EventFiringDriverPerformance.PerformanceReports;

public class PerformanceReportService
{
    private readonly TestCasePerformanceData _testCasePerformanceData;
    private readonly Stopwatch _stopWatch = new();

    public PerformanceReportService()
    {
        _testCasePerformanceData = new TestCasePerformanceData();
        _stopWatch.Start();
    }

    public void SetReportName(string reportName)
    {
        _testCasePerformanceData.TestName = reportName;
    }
        
    public void AddDataPoint(string pageUrl, string driverTitle, PagePerformanceTiming? pagePerformanceTiming,
        double readyMeasureTime, double jsHeapMemoryUsed)
    {
        if (_testCasePerformanceData.PagePerformanceData.LastOrDefault()?.ReadyMeasure != readyMeasureTime)
        {
            _testCasePerformanceData.PagePerformanceData.Add(
                new PagePerformanceData()
                {
                    PageUrl = pageUrl,
                    PageTitle = driverTitle,
                    PagePerformanceTiming = pagePerformanceTiming!,
                    ReadyMeasure = readyMeasureTime,
                    JSHeapMemoryUsed = jsHeapMemoryUsed,
                });
        }
    }

    public void GenerateReport()
    {
        _stopWatch.Stop();
        _testCasePerformanceData.TestTotalTime = _stopWatch.Elapsed.Seconds;
        if (!_testCasePerformanceData.PagePerformanceData.Any())
        {
            Console.WriteLine("No performance Data available. Cannot Generate Report.");
            return;
        }
            
        string template = $@"<!DOCTYPE html>
<html>
  <head>
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/chartist.js/latest/chartist.min.css"">
    <script src=""https://cdn.jsdelivr.net/chartist.js/latest/chartist.min.js""></script>
    <script src=""https://codeyellowbv.github.io/chartist-plugin-legend/chartist-plugin-legend.js""></script>
    <style>
        table, th, td {{
          border: 1px solid;
        }}
        table {{
          border-collapse: collapse;
        }}
        .test-data-table {{
          padding: 15px;
        }}

       .ct-chart {{
           position: relative;
       }}
       .ct-legend {{
           position: relative;
           z-index: 10;
           list-style: none;
           text-align: center;
       }}
       .ct-legend li {{
           position: relative;
           padding-left: 23px;
           margin-right: 10px;
           margin-bottom: 3px;
           cursor: pointer;
           display: inline-block;
       }}
       .ct-legend li:before {{
           width: 12px;
           height: 12px;
           position: absolute;
           left: 0;
           content: '';
           border: 3px solid transparent;
           border-radius: 2px;
       }}
       .ct-legend li.inactive:before {{
           background: transparent;
       }}
       .ct-legend.ct-legend-inside {{
           position: absolute;
           top: 0;
           right: 0;
       }}
       .ct-legend.ct-legend-inside li{{
           display: block;
           margin: 0;
       }}
       .ct-legend .ct-series-0:before {{
           background-color: #d70206;
           border-color: #d70206;
       }}
       .ct-legend .ct-series-1:before {{
           background-color: #f05b4f;
           border-color: #f05b4f;
       }}
       .ct-legend .ct-series-2:before {{
           background-color: #f4c63d;
           border-color: #f4c63d;
       }}
       .ct-legend .ct-series-3:before {{
           background-color: #d17905;
           border-color: #d17905;
       }}
       .ct-legend .ct-series-4:before {{
           background-color: #453d3f;
           border-color: #453d3f;
       }}
    </style>
   </head>
   <div class=""test-data-wrapper"">
    <h1>{_testCasePerformanceData.TestName}</h1>
    <h2>Test Execution Time: {_testCasePerformanceData.TestTotalTime} s.</h2>
   </div>

   <div class=""test-data-table"">
    <table><thead>
    <tr><th>Page Name</th><th>Page Url</th><th>Page Load Time (ms.)</th></tr>
    </thead><tbody>
    {string.Join("\r\n", _testCasePerformanceData.PagePerformanceData.Select(page => $"<tr><td>{page.PageTitle}</td><td><a href={page.PageUrl}>{page.PageUrl}</a></td><td>{page.ReadyMeasure}</td></tr>"))}
    </tbody></table></div>
    <h2>Total Page Load Time</h2>
    <div class=""ct-chart ct-perfect-fourth total-time"" style=""height: 90vh; width: 90vw; margin: auto 0""></div>
    <h2>Page Load Time Details</h2>
    <div class=""ct-chart ct-perfect-fourth detailed-time"" style=""height: 90vh; width: 90vw; margin: auto 0""></div>
    <h2>Transfer Size</h2>
    <div class=""ct-chart ct-perfect-fourth transfer-size"" style=""height: 90vh; width: 90vw; margin: auto 0""></div>
    <h2>JS Heap Size</h2>
    <div class=""ct-chart ct-perfect-fourth js-heap-size"" style=""height: 90vh; width: 90vw; margin: auto 0""></div>
<script>
        var totalTimeData = {{
            // A labels array that can contain any sort of values
            labels:
            ['{string.Join("', '", _testCasePerformanceData.PagePerformanceData.Select(p => p.PageTitle).ToArray())}'],
            series: [
                {{""name"": ""ReadyMeasure"", 
                  ""data"": [{string.Join(", ", _testCasePerformanceData.PagePerformanceData.Select(p => p.ReadyMeasure).ToArray())}]}},
            ]
            }};
        var detailTimeData = {{
            // A labels array that can contain any sort of values
            labels:
            ['{string.Join("', '", _testCasePerformanceData.PagePerformanceData.Select(p => p.PageTitle).ToArray())}'],
            series: [
                {{ ""name"": ""PageLoadTime"", ""data"": [{string.Join(", ", _testCasePerformanceData.PagePerformanceData.Select(p => p.PageLoadTime).ToArray())}] }},
                {{ ""name"": ""DOMContentLoadedTime"", ""data"": [{string.Join(", ", _testCasePerformanceData.PagePerformanceData.Select(p => p.DOMContentLoadedTime).ToArray())}] }},
                {{ ""name"": ""DOMInteractive"", ""data"": [{string.Join(", ", _testCasePerformanceData.PagePerformanceData.Select(p => p.DOMInteractive).ToArray())}] }},
                {{ ""name"": ""SSLNegotiationTime"", ""data"": [{string.Join(", ", _testCasePerformanceData.PagePerformanceData.Select(p => p.SSLNegotiationTime).ToArray())}] }},
                {{ ""name"": ""DOMComplete"", ""data"": [{string.Join(", ", _testCasePerformanceData.PagePerformanceData.Select(p => p.DOMComplete).ToArray())}] }},
                ]
                }};

        var transferSizeData = {{
            // A labels array that can contain any sort of values
            labels: 
                ['{string.Join("', '", _testCasePerformanceData.PagePerformanceData.Select(p => p.PageTitle).ToArray())}'],
            series: 
                [{{ ""name"": ""TransferSize"", ""data"": [{string.Join(", ", _testCasePerformanceData.PagePerformanceData.Select(p => p.PagePerformanceTiming.TransferSize).ToArray())}] }},]
            }};
        var jsHeapSizeData = {{
            // A labels array that can contain any sort of values
            labels:
            ['{string.Join("', '", _testCasePerformanceData.PagePerformanceData.Select(p => p.PageTitle).ToArray())}'],
            series: [
                {{ ""name"": ""JSHeapMemoryUsed"", 
                   ""data"": [{string.Join(", ", _testCasePerformanceData.PagePerformanceData.Select(p => p.JSHeapMemoryUsed).ToArray())}] }},]
            }};
        var timeOptions = {{
          // Don't draw the line chart points
          showPoint: true,
          // Disable line smoothing
          lineSmooth: true,
          // X-Axis specific configuration
          axisX: {{
            // We can disable the grid for this axis
            showGrid: true,
            // and also don't show the label
            showLabel: true
          }},
          // Y-Axis specific configuration
          axisY: {{
            // Lets offset the chart a bit from the labels
            offset: 100,
            // The label interpolation function enables you to modify the values
            // used for the labels on each axis. Here we are converting the
            // values into million pound.
            labelInterpolationFnc: function(value) {{
              return value + ' ms.';
            }}
          }},
          plugins: [
              Chartist.plugins.legend({{
                  position: 'bottom'
              }})
          ]
        }};
        var sizeOptions = {{
                    // Don't draw the line chart points
                    showPoint: true,
                    // Disable line smoothing
                    lineSmooth: true,
                    // X-Axis specific configuration
                    axisX: {{
                    // We can disable the grid for this axis
                    showGrid: true,
                    // and also don't show the label
                    showLabel: true
                    }},
                    // Y-Axis specific configuration
                    axisY: {{
                    // Lets offset the chart a bit from the labels
                    offset: 60,
                    // The label interpolation function enables you to modify the values
                    // used for the labels on each axis. Here we are converting the
                    // values into million pound.
                    labelInterpolationFnc: function(value) {{
                        return value + ' bytes';
                    }}
                    }},
                    plugins: [
                        Chartist.plugins.legend({{
                            position: 'bottom'
                        }})
                    ]
                }};

    // Create a new line chart object where as first parameter we pass in a selector
    // that is resolving to our chart container element. The Second parameter
    // is the actual data object.
    new Chartist.Line('.ct-chart.total-time', totalTimeData, timeOptions);
    new Chartist.Line('.ct-chart.detailed-time', detailTimeData, timeOptions);
    new Chartist.Line('.ct-chart.transfer-size', transferSizeData, sizeOptions);
    new Chartist.Line('.ct-chart.js-heap-size', jsHeapSizeData, sizeOptions);
</script>
</html>";

        try
        {
            string filePath =
                $".\\PerformanceReport-{_testCasePerformanceData.TestName}-{DateTime.Now:yyyyMMddHHmmss}.html";
            File.WriteAllText(filePath, template);
            Console.WriteLine(
                $"Link to Performance Report: \r\n{Path.Combine(Environment.CurrentDirectory, filePath)}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Failed to generate Performance Report due to error: " + ex.Message);
        }
    }
}
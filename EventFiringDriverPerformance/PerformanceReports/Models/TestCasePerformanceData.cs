namespace EventFiringDriverPerformance.PerformanceReports.Models;

public class TestCasePerformanceData
{
    public TestCasePerformanceData()
    {
        PagePerformanceData = new List<PagePerformanceData>();
    }

    public string TestName { get; set; }
    public double TestTotalTime { get; set; }
    public List<PagePerformanceData> PagePerformanceData { get; set; }
}
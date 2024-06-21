using EventFiringDriverPerformance.Driver.Cdp;
using EventFiringDriverPerformance.Driver.Handlers;
using EventFiringDriverPerformance.PerformanceReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.Events;

namespace EventFiringDriverPerformance.Driver;

public class WebDriverService
{    
    private IWebDriver? _driver;
    private DevToolsCommandExecutor? _devToolsCommandExecutor;
    private readonly PerformanceReportService? _performanceReportService = new();

    private void InitializeDriver(int implicitWait = 5)
    {
        ChromeOptions options = new ChromeOptions();

        #region RequiredForPerfLogsHandler

        options.PerformanceLoggingPreferences = new ChromiumPerformanceLoggingPreferences()
        {
            IsCollectingNetworkEvents = false,
            IsCollectingPageEvents = true
        };
        options.SetLoggingPreference(LogType.Performance, LogLevel.All);
        
        #endregion
        
        _driver = new ChromeDriver(options);

        #region RequiredForDevToolsHandler

        var devToolsSession = ((IDevTools)_driver).GetDevToolsSession();
        _devToolsCommandExecutor = new DevToolsCommandExecutor(devToolsSession);

        #endregion
        
        EventFiringWebDriver eventDriver = new EventFiringWebDriver(_driver);
        _driver = RegisterEventHandlers(eventDriver);
        
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
        _driver.Manage().Window.Maximize();
    }

    private EventFiringWebDriver RegisterEventHandlers(EventFiringWebDriver eventDriver)
    {
        // Uncomment the handler that you want to use for extraction of Performance Data
        
        WebDriverDevToolsEventsHandler webDriverDevToolsEventsHandler = new WebDriverDevToolsEventsHandler(this);
        eventDriver.Navigated += webDriverDevToolsEventsHandler.EventDriverOnNavigated;
        eventDriver.ElementClicked += webDriverDevToolsEventsHandler.EventDriverOnElementClicked;
        
        // WebDriverPerfLogsHandler webDriverPerfLogsHandler = new WebDriverPerfLogsHandler(this);
        // eventDriver.Navigated += webDriverPerfLogsHandler.EventDriverOnNavigated;
        // eventDriver.ElementClicked += webDriverPerfLogsHandler.EventDriverOnElementClicked;
        
        // WebDriverJsHandler webDriverJsHandler = new WebDriverJsHandler(this);
        // eventDriver.Navigated += webDriverJsHandler.EventDriverOnNavigated;
        // eventDriver.ElementClicked += webDriverJsHandler.EventDriverOnElementClicked;
        
        return eventDriver;
    }

    public IWebDriver GetDriver()
    {
        if (_driver == null)
        {
            InitializeDriver();
        }

        return _driver;
    }
    
    public PerformanceReportService GetPerformanceReportService()
    {
        return _performanceReportService;
    }
    
    public DevToolsCommandExecutor GetDevTools()
    {
        if (_devToolsCommandExecutor == null)
        {
            InitializeDriver();
        }

        return _devToolsCommandExecutor;
    }
    
    public void ShutDownDriver()
    {
        if (_driver is not null)
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
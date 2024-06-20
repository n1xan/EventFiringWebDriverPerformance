### Folders Structure

📁 EventFiringDriverPerformance
│
├── 📁 Driver
│   └── 📁 Cdp
│       ├── DevToolsCommandExecutor.cs
│       └── PagePerformanceMetrics.cs
│
├── 📁 Handlers
│   ├── WebDriverDevToolsEventsHandler.cs
│   ├── WebDriverJsHandler.cs
│   ├── WebDriverPerfLogsHandler.cs
│   └── WebDriverService.cs
│
├── 📁 PerformanceReports
│   ├── 📁 Models
│   │   ├── PagePerformanceData.cs
│   │   ├── PagePerformanceTiming.cs
│   │   ├── TestCasePerformanceData.cs
│   └── PerformanceReportService.cs
│
├── 📁 Tests
│   └── ShoppingCartTests.cs
│
├── .gitignore
├── EventFiringDriverPerformance.csproj
├── EventFiringDriverPerformance.sln
└── README.md


### Folder Descriptions

- **Driver**: Contains driver-related files.
  - **Cdp**: Directory for Chrome DevTools Protocol related files.
    - `DevToolsCommandExecutor.cs`: Executes DevTools commands.
    - `PagePerformanceMetrics.cs`: Collects page performance metrics.

- **Handlers**: Includes various handlers for WebDriver events.
  - `WebDriverDevToolsEventsHandler.cs`: Handles events from the Chrome DevTools.
  - `WebDriverJsHandler.cs`: Manages JavaScript execution in WebDriver.
  - `WebDriverPerfLogsHandler.cs`: Logs WebDriver performance metrics.
  - `WebDriverService.cs`: Provides WebDriver services.

- **PerformanceReports**: Contains files related to performance reporting.
  - **Models**: Data models for performance metrics.
    - `PagePerformanceData.cs`: Model for page performance data.
    - `PagePerformanceTiming.cs`: Model for page performance timing data.
    - `TestCasePerformanceData.cs`: Model for test case performance data.
  - `PerformanceReportService.cs`: Service for generating performance reports.

- **Tests**: Contains test classes.
  - `ShoppingCartTests.cs`: Tests related to shopping cart functionality.

- **.gitignore**: Specifies files and directories to be ignored by Git.
- **EventFiringDriverPerformance.csproj**: Project file for the C# project.
- **EventFiringDriverPerformance.sln**: Visual Studio solution file for the project.
- **README.md**: This file, providing an overview and documentation of the project.

### Usage

1. **Driver**: Navigate here for driver-related implementations.
2. **Handlers**: Contains event handlers for WebDriver.
3. **PerformanceReports**: Access this folder for performance reporting models and services.
4. **Tests**: Contains test cases for the project.
5. **.gitignore**: Ensures unnecessary files are not tracked by Git.
6. **EventFiringDriverPerformance.csproj**: Project configuration file.
7. **EventFiringDriverPerformance.sln**: Open this solution file in Visual Studio to start working with the project.


# Project Requirements

## .NET SDK
- Ensure you have the .NET 8.0 SDK installed.
  - [Download .NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## NuGet Packages
- **Microsoft.NET.Test.Sdk (v17.10.0)**: Essential for running and reporting tests within Visual Studio.
- **NUnit (v4.1.0)**: A unit-testing framework for all .NET languages.
- **Selenium.Support (v4.21.0)**: Provides additional support classes for Selenium WebDriver.
- **Selenium.WebDriver (v4.21.0)**: The main Selenium WebDriver package for browser automation.
- **Selenium.WebDriver.ChromeDriver (v125.0.6422.14100)**: ChromeDriver, a separate executable that Selenium WebDriver uses to control Chrome.

## Installation Steps

1. **.NET SDK**:
   - Download and install the .NET 8.0 SDK from the [official .NET website](https://dotnet.microsoft.com/download/dotnet/8.0).

2. **NuGet Packages**:
   - Ensure you have NuGet package manager installed (typically comes with Visual Studio).
   - Restore NuGet packages by running:
     ```sh
     dotnet restore
     ```

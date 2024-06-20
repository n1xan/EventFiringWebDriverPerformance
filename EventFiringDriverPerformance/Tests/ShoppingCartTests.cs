using EventFiringDriverPerformance.Driver;
using NUnit.Framework;
using OpenQA.Selenium;

namespace EventFiringDriverPerformance.Tests;

[TestFixture]
public class ShoppingCartTests
{
    private WebDriverService _driverService;
    const string BaseUrl = "https://demos.bellatrix.solutions/";

    #region credentials

    private const string Username = "info@berlinspaceflowers.com";
    private const string Password = "@purISQzt%%DYBnLCIhaoG6$";

    #endregion

    [SetUp]
    public void BeforeTest()
    {
        _driverService = new WebDriverService();
        _driverService.GetDriver().Navigate().GoToUrl(BaseUrl);
    }

    [TearDown]
    public void AfterTest()
    {
        _driverService.ShutDownDriver();
        _driverService.GetPerformanceReportService().GenerateReport();
    }
    
    [Test]
    public void LoginTest()
    {
        _driverService.GetDriver().Navigate().GoToUrl(BaseUrl);
        IWebElement myAccountLink = _driverService.GetDriver().FindElement(By.LinkText("My account"));
        myAccountLink.Click();

        IWebElement usernameInput = _driverService.GetDriver().FindElement(By.Id("username"));
        IWebElement passwordInput = _driverService.GetDriver().FindElement(By.Id("password"));
        
        usernameInput.SendKeys(Username);
        passwordInput.SendKeys(Password);
        
        IWebElement loginButton = _driverService.GetDriver().FindElement(By.Name("login"));
        loginButton.Click();
        
        IWebElement greetingMessage = _driverService.GetDriver().FindElement(By.XPath("//div[@class='woocommerce-MyAccount-content']/p/strong[1]"));
        Assert.That("Berlin Spaceflowers", Is.EqualTo(greetingMessage.Text));
    }
    
    [Test]
    public void SiteNavigationTest()
    {
        LoginTest();
        
        IWebElement blogLink  = _driverService.GetDriver().FindElement(By.LinkText("Blog"));
        blogLink.Click();
        AssertUrlContains("blog/");
        
        IWebElement cartLink  = _driverService.GetDriver().FindElement(By.LinkText("Cart"));
        cartLink.Click();
        AssertUrlContains("cart/");
        
        IWebElement checkoutLink  = _driverService.GetDriver().FindElement(By.LinkText("Checkout"));
        checkoutLink.Click();
        AssertUrlContains("cart/");
        
        IWebElement contactFormLink  = _driverService.GetDriver().FindElement(By.LinkText("Contact Form"));
        contactFormLink.Click();
        AssertUrlContains("contact-form/");
        
        IWebElement promotionsLink  = _driverService.GetDriver().FindElement(By.LinkText("Promotions"));
        promotionsLink.Click();
        AssertUrlContains("welcome/");
    }

    private void AssertUrlContains(string partialUrl)
    {
        Assert.That(_driverService.GetDriver().Url, Is.EqualTo(BaseUrl + partialUrl));
    }
}
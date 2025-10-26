using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;
using System.Configuration;

namespace SeleniumBDDFramework.Utilities
{
    public static class DriverFactory
    {
        [ThreadStatic]
        private static IWebDriver _driver;

        public static IWebDriver Driver => _driver ?? throw new NullReferenceException("WebDriver instance was not initialized. Call InitBrowser() first.");

        public static void InitBrowser()
        {
            string browser = ConfigurationManager.AppSettings["browser"] ?? "chrome";
            bool headless = Convert.ToBoolean(ConfigurationManager.AppSettings["headless"] ?? "false");

            switch (browser.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    if (headless) chromeOptions.AddArgument("--headless=new");
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddArgument("--disable-notifications");
                    chromeOptions.AddArgument("--disable-popup-blocking");
                    _driver = new ChromeDriver(chromeOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    if (headless) edgeOptions.AddArgument("--headless=new");
                    _driver = new EdgeDriver(edgeOptions);
                    _driver.Manage().Window.Maximize();
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    if (headless) firefoxOptions.AddArgument("--headless");
                    _driver = new FirefoxDriver(firefoxOptions);
                    _driver.Manage().Window.Maximize();
                    break;

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public static string CaptureScreenshot(string testName)
        {
            string screenshotDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "Screenshots");
            Directory.CreateDirectory(screenshotDir);
            string filePath = Path.Combine(screenshotDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            var screenshot = _driver.TakeScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            return filePath;
        }

        public static void CloseBrowser()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                    _driver.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error closing browser: {ex.Message}");
                }
                finally
                {
                    _driver = null;
                }
            }
        }

        public static void ClearCookies()
        {
            try
            {
                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Navigate().Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing cookies: {ex.Message}");
            }
        }

        public static string GetCurrentUrl() => _driver?.Url ?? "Driver not initialized.";

        public static void NavigateTo(string url)
        {
            if (_driver == null)
                throw new NullReferenceException("Driver not initialized. Call InitBrowser() before navigating.");

            _driver.Navigate().GoToUrl(url);
        }
    }
}

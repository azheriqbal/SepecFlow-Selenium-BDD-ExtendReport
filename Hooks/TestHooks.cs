using OpenQA.Selenium;
using SeleniumBDDFramework.Utilities;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System;
using System.IO;

namespace SeleniumBDDFramework.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "Screenshots");
            Directory.CreateDirectory(reportDir);
            Console.WriteLine("===== Test Execution Started =====");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Console.WriteLine($"Starting Scenario: {_scenarioContext.ScenarioInfo.Title}");
            DriverFactory.InitBrowser();
            DriverFactory.ClearCookies();
        }

        [AfterStep]
        public void AfterStep()
        {
            if (_scenarioContext.TestError != null)
            {
                string testName = _scenarioContext.ScenarioInfo.Title.Replace(" ", "_");
                string screenshotPath = DriverFactory.CaptureScreenshot(testName);
                Console.WriteLine($"[ERROR] Step Failed: {_scenarioContext.TestError.Message}");
                Console.WriteLine($"Screenshot saved at: {screenshotPath}");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine($"Completed Scenario: {_scenarioContext.ScenarioInfo.Title}");
            if (_scenarioContext.TestError != null)
            {
                Console.WriteLine($"Scenario Failed: {_scenarioContext.TestError.Message}");
            }
            DriverFactory.CloseBrowser();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("===== Test Execution Completed =====");
        }
    }
}

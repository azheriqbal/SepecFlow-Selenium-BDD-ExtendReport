using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace Source2BDD.StepDefinitions
{
    [Binding]
    public class YoutubeSearchFeatureStepDefinitions
    {
        private IWebDriver driver;

        [Given(@"Open Browser")]
        public void GivenOpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

        }

        [When(@"Enter Url")]
        public void WhenEnterUrl()
        {
            driver.Url = "https://www.youtube.com/";
            Thread.Sleep(3000);
        }

        [Then(@"Seach the video")]
        public void ThenSeachTheVideo()
        {
            driver.FindElement(By.Name("search_query")).SendKeys("Tester Talks");
            driver.FindElement(By.Name("search_query")).SendKeys(Keys.Enter);
            Thread.Sleep(3000);

            driver.Quit();
        }
    }
}


//>livingdoc test-assembly Source2BDD.dll -t TestExecution.json

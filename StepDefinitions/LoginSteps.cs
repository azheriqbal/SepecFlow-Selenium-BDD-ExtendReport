using SeleniumBDDFramework.Pages;
using SeleniumBDDFramework.Utilities;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SeleniumBDDFramework.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        public LoginSteps()
        {
            _driver = DriverFactory.Driver;
            _loginPage = new LoginPage(_driver);
        }

        [Given(@"I navigate to the login page")]
        public void GivenINavigateToTheLoginPage()
        {
            _driver.Navigate().GoToUrl(ConfigReader.BaseUrl);
        }

        [When(@"I enter valid username and password")]
        public void WhenIEnterValidUsernameAndPassword()
        {
            _loginPage.EnterUsername("admin");
            _loginPage.EnterPassword("password123");
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.ClickLogin();
        }

        [Then(@"I should see the dashboard page")]
        public void ThenIShouldSeeTheDashboardPage()
        {
            Assert.That(_loginPage.GetPageTitle(), Does.Contain("Dashboard"));
        }
    }
}

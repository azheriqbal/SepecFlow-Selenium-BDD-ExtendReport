using OpenQA.Selenium;

namespace SeleniumBDDFramework.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // Locators
        private IWebElement Username => _driver.FindElement(By.Id("username"));
        private IWebElement Password => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("loginBtn"));

        // Constructor
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Actions
        public void EnterUsername(string username) => Username.SendKeys(username);
        public void EnterPassword(string password) => Password.SendKeys(password);
        public void ClickLogin() => LoginButton.Click();

        public string GetPageTitle() => _driver.Title;
    }
}

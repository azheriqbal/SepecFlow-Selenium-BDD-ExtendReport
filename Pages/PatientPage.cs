using OpenQA.Selenium;

namespace SeleniumBDDFramework.Pages
{
    public class PatientPage
    {
        private readonly IWebDriver _driver;
        public PatientPage(IWebDriver driver) => _driver = driver;

        private IWebElement FirstName => _driver.FindElement(By.Id("firstName"));
        private IWebElement LastName => _driver.FindElement(By.Id("lastName"));
        private IWebElement Gender => _driver.FindElement(By.Id("gender"));
        private IWebElement Age => _driver.FindElement(By.Id("age"));
        private IWebElement Contact => _driver.FindElement(By.Id("contact"));
        private IWebElement SaveButton => _driver.FindElement(By.Id("savePatient"));
        private IWebElement SuccessMsg => _driver.FindElement(By.CssSelector(".alert-success"));

        public void CreatePatient(string firstName, string lastName, string gender, string age, string contact)
        {
            FirstName.SendKeys(firstName);
            LastName.SendKeys(lastName);
            Gender.SendKeys(gender);
            Age.SendKeys(age);
            Contact.SendKeys(contact);
            SaveButton.Click();
        }

        public string GetSuccessMessage() => SuccessMsg.Text;
    }
}

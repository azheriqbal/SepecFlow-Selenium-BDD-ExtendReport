using SeleniumBDDFramework.Pages;
using SeleniumBDDFramework.Utilities;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SeleniumBDDFramework.StepDefinitions
{
    [Binding]
    public class PatientSteps
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly PatientPage _patientPage;
        private readonly PatientLookupPage _lookupPage;
        private readonly VisitPage _visitPage;

        public PatientSteps()
        {
            _driver = DriverFactory.Driver;
            _loginPage = new LoginPage(_driver);
            _patientPage = new PatientPage(_driver);
            _lookupPage = new PatientLookupPage(_driver);
            _visitPage = new VisitPage(_driver);
        }

        [Given(@"I am logged into the application with valid credentials")]
        public void GivenIAmLoggedIntoTheApplication()
        {
            _driver.Navigate().GoToUrl(ConfigReader.BaseUrl);
            _loginPage.EnterUsername("admin");
            _loginPage.EnterPassword("password123");
            _loginPage.ClickLogin();
        }

        [When(@"I create a new patient with details:")]
        public void WhenICreateANewPatientWithDetails(Table table)
        {
            var data = table.Rows[0];
            _patientPage.CreatePatient(
                data["FirstName"],
                data["LastName"],
                data["Gender"],
                data["Age"],
                data["Contact"]
            );
        }

        [Then(@"I should see a confirmation message ""(.*)""")]
        public void ThenIShouldSeeAConfirmationMessage(string expected)
        {
            Assert.That(_patientPage.GetSuccessMessage(), Does.Contain(expected));
        }

        [When(@"I search for ""(.*)"" in the patient lookup")]
        public void WhenISearchForInThePatientLookup(string name)
        {
            _lookupPage.SearchPatient(name);
        }

        [Then(@"the patient ""(.*)"" should appear in the search results")]
        public void ThenThePatientShouldAppearInTheSearchResults(string name)
        {
            Assert.That(_lookupPage.IsPatientInResults(name), Is.True, $"{name} not found in lookup");
        }

        [When(@"I create a new visit for ""(.*)"" with reason ""(.*)""")]
        public void WhenICreateANewVisitForWithReason(string name, string reason)
        {
            _visitPage.CreateVisit(name, reason);
        }

        [Then(@"the visit should be created successfully")]
        public void ThenTheVisitShouldBeCreatedSuccessfully()
        {
            Assert.That(_visitPage.GetVisitConfirmation(), Does.Contain("Visit created successfully"));
        }
    }
}

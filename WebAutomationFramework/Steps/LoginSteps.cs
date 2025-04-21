using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using WebAutomationFramework.Pages;
using WebAutomationFramework.Support;

namespace WebAutomationFramework.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly SecurePage _securePage;
        private ScenarioContext _scenarioContext;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPage();
            _securePage = new SecurePage();
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _loginPage.NavigateTo();
        }

        [When(@"I enter username ""(.*)""")]
        public void WhenIEnterUsername(string username)
        {
            // 使用配置的用户名，如果是特殊值
            if (username == "default")
            {
                username = Config.TestUsername;
            }
            
            _loginPage.EnterUsername(username);
        }

        [When(@"I enter password ""(.*)""")]
        public void WhenIEnterPassword(string password)
        {
            // 使用配置的密码，如果是特殊值
            if (password == "default")
            {
                password = Config.TestPassword;
            }
            
            _loginPage.EnterPassword(password);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.ClickLoginButton();
        }

        [When(@"I login with default credentials")]
        public void WhenILoginWithDefaultCredentials()
        {
            _loginPage.LoginWithDefaultUser();
        }

        [When(@"I login with username ""(.*)"" and password ""(.*)""")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            // 支持使用默认值
            if (username == "default")
            {
                username = Config.TestUsername;
            }
            
            if (password == "default")
            {
                password = Config.TestPassword;
            }
            
            _loginPage.LoginAs(username, password);
        }

        [Then(@"I should see a successful login message")]
        public void ThenIShouldSeeASuccessfulLoginMessage()
        {
            Assert.IsTrue(_securePage.IsLogoutButtonDisplayed(), "Logout button is not displayed");
            
            string flashMessage = _securePage.GetFlashMessageText();
            Assert.IsTrue(flashMessage.Contains("You logged into a secure area"), 
                $"Flash message does not contain expected text. Actual text: {flashMessage}");
        }
    }
} 
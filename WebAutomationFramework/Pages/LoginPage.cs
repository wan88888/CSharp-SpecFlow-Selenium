using OpenQA.Selenium;
using WebAutomationFramework.Support;

namespace WebAutomationFramework.Pages
{
    public class LoginPage : BasePage
    {
        private static readonly string PagePath = "/login";
        private string Url => $"{Config.BaseUrl}{PagePath}";
        
        private By UsernameInput => By.Id("username");
        private By PasswordInput => By.Id("password");
        private By LoginButton => By.CssSelector("button[type='submit']");
        private By SuccessMessage => By.CssSelector(".flash.success");
        private By ErrorMessage => By.CssSelector(".flash.error");

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(Url);
        }

        public void EnterUsername(string username)
        {
            SendKeys(UsernameInput, username);
        }

        public void EnterPassword(string password)
        {
            SendKeys(PasswordInput, password);
        }

        public void ClickLoginButton()
        {
            Click(LoginButton);
        }

        // 登录助手方法
        public void LoginAs(string username, string password)
        {
            NavigateTo();
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        // 使用默认测试账户登录
        public void LoginWithDefaultUser()
        {
            LoginAs(Config.TestUsername, Config.TestPassword);
        }

        public bool IsSuccessMessageDisplayed()
        {
            return IsElementDisplayed(SuccessMessage);
        }

        public string GetSuccessMessage()
        {
            return GetText(SuccessMessage);
        }

        public bool IsErrorMessageDisplayed()
        {
            return IsElementDisplayed(ErrorMessage);
        }

        public string GetErrorMessage()
        {
            return GetText(ErrorMessage);
        }
    }
} 
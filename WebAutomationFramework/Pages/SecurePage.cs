using OpenQA.Selenium;

namespace WebAutomationFramework.Pages
{
    public class SecurePage : BasePage
    {
        private By SecureAreaHeader => By.CssSelector("h2");
        private By LogoutButton => By.CssSelector(".button.secondary");
        private By FlashMessage => By.Id("flash");

        public bool IsSecureAreaHeaderDisplayed()
        {
            return IsElementDisplayed(SecureAreaHeader);
        }

        public string GetSecureAreaHeaderText()
        {
            return GetText(SecureAreaHeader);
        }

        public bool IsLogoutButtonDisplayed()
        {
            return IsElementDisplayed(LogoutButton);
        }

        public string GetFlashMessageText()
        {
            return GetText(FlashMessage);
        }

        public void ClickLogoutButton()
        {
            Click(LogoutButton);
        }
    }
} 
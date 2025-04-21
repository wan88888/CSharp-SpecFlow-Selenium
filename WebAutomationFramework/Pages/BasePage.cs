using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using WebAutomationFramework.Drivers;
using WebAutomationFramework.Support;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace WebAutomationFramework.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver => WebDriverFactory.Driver;
        protected WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(Config.DefaultTimeout));
        protected Actions Actions => new Actions(Driver);

        #region 等待方法
        protected void WaitForElementVisible(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected void WaitForElementClickable(By locator)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        protected void WaitForTextPresent(By locator, string text)
        {
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

        protected void WaitForElementNotVisible(By locator)
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }
        #endregion

        #region 元素操作
        protected void Click(By locator)
        {
            WaitForElementClickable(locator);
            Driver.FindElement(locator).Click();
        }

        protected void SendKeys(By locator, string text)
        {
            WaitForElementVisible(locator);
            var element = Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            WaitForElementVisible(locator);
            return Driver.FindElement(locator).Text;
        }

        protected string GetAttribute(By locator, string attributeName)
        {
            WaitForElementVisible(locator);
            return Driver.FindElement(locator).GetAttribute(attributeName);
        }

        protected bool IsElementDisplayed(By locator, int timeoutInSeconds = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }

        protected void SelectDropdownByText(By locator, string text)
        {
            WaitForElementVisible(locator);
            var element = Driver.FindElement(locator);
            var select = new SelectElement(element);
            select.SelectByText(text);
        }

        protected void SelectDropdownByValue(By locator, string value)
        {
            WaitForElementVisible(locator);
            var element = Driver.FindElement(locator);
            var select = new SelectElement(element);
            select.SelectByValue(value);
        }

        protected void SelectDropdownByIndex(By locator, int index)
        {
            WaitForElementVisible(locator);
            var element = Driver.FindElement(locator);
            var select = new SelectElement(element);
            select.SelectByIndex(index);
        }
        #endregion

        #region 高级操作
        protected void Hover(By locator)
        {
            WaitForElementVisible(locator);
            var element = Driver.FindElement(locator);
            Actions.MoveToElement(element).Perform();
        }

        protected void DoubleClick(By locator)
        {
            WaitForElementVisible(locator);
            var element = Driver.FindElement(locator);
            Actions.DoubleClick(element).Perform();
        }

        protected void RightClick(By locator)
        {
            WaitForElementVisible(locator);
            var element = Driver.FindElement(locator);
            Actions.ContextClick(element).Perform();
        }

        protected void DragAndDrop(By sourceLocator, By targetLocator)
        {
            WaitForElementVisible(sourceLocator);
            WaitForElementVisible(targetLocator);
            
            var source = Driver.FindElement(sourceLocator);
            var target = Driver.FindElement(targetLocator);
            
            Actions.DragAndDrop(source, target).Perform();
        }

        protected void ScrollToElement(By locator)
        {
            WaitForElementVisible(locator);
            var element = Driver.FindElement(locator);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        protected void ExecuteJavaScript(string script, params object[] args)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript(script, args);
        }
        #endregion

        #region 浏览器操作
        protected void RefreshPage()
        {
            Driver.Navigate().Refresh();
        }

        protected void NavigateBack()
        {
            Driver.Navigate().Back();
        }

        protected void NavigateForward()
        {
            Driver.Navigate().Forward();
        }

        protected string GetCurrentUrl()
        {
            return Driver.Url;
        }

        protected string GetPageTitle()
        {
            return Driver.Title;
        }
        #endregion
    }
} 
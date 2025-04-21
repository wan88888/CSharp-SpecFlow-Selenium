using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WebAutomationFramework.Drivers
{
    public class WebDriverFactory
    {
        private static IWebDriver? _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    InitializeDriver();
                }
                return _driver!;
            }
        }

        public static void InitializeDriver()
        {
            try
            {
                // Setup driver with WebDriverManager to match current Chrome version
                new DriverManager().SetUpDriver(new ChromeConfig());
                
                // Create Chrome options
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-extensions");
                
                // Add headless option if needed (uncomment to run headless)
                // options.AddArgument("--headless=new");
                
                _driver = new ChromeDriver(options);
                _driver.Manage().Window.Maximize();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing ChromeDriver: {ex.Message}");
                throw;
            }
        }

        public static void CloseDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
} 
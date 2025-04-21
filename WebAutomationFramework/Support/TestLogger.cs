using System;
using System.IO;
using OpenQA.Selenium;
using WebAutomationFramework.Drivers;

namespace WebAutomationFramework.Support
{
    public static class TestLogger
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestLogs");
        private static readonly string ScreenshotDirectory = Path.Combine(LogDirectory, "Screenshots");

        static TestLogger()
        {
            EnsureDirectoriesExist();
        }

        private static void EnsureDirectoriesExist()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            if (!Directory.Exists(ScreenshotDirectory))
            {
                Directory.CreateDirectory(ScreenshotDirectory);
            }
        }

        public static void Log(string message)
        {
            string logFile = Path.Combine(LogDirectory, $"TestLog_{DateTime.Now:yyyyMMdd}.log");
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            
            Console.WriteLine(logMessage);
            
            try
            {
                File.AppendAllText(logFile, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"写入日志文件时出错: {ex.Message}");
            }
        }

        public static string? TakeScreenshot(string? screenshotName = null)
        {
            IWebDriver driver = WebDriverFactory.Driver;
            
            if (driver is ITakesScreenshot takesScreenshot)
            {
                try
                {
                    // 如果未指定名称，使用时间戳
                    if (string.IsNullOrEmpty(screenshotName))
                    {
                        screenshotName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss.fff}";
                    }
                    
                    // 确保文件名有效
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        screenshotName = screenshotName.Replace(c, '_');
                    }
                    
                    string screenshotFile = Path.Combine(ScreenshotDirectory, $"{screenshotName}.png");
                    Screenshot screenshot = takesScreenshot.GetScreenshot();
                    screenshot.SaveAsFile(screenshotFile);
                    
                    Log($"截图已保存到: {screenshotFile}");
                    return screenshotFile;
                }
                catch (Exception ex)
                {
                    Log($"截图时出错: {ex.Message}");
                }
            }
            
            return null;
        }
    }
} 
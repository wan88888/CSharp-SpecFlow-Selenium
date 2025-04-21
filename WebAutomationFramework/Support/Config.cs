using System;

namespace WebAutomationFramework.Support
{
    public static class Config
    {
        // 浏览器配置
        public static string Browser => GetEnvironmentVariable("BROWSER", "chrome");
        public static bool Headless => GetEnvironmentVariable("HEADLESS", "true") == "true";
        public static int DefaultTimeout => int.Parse(GetEnvironmentVariable("DEFAULT_TIMEOUT", "10"));
        
        // 应用程序URL
        public static string BaseUrl => GetEnvironmentVariable("BASE_URL", "http://the-internet.herokuapp.com");
        
        // 测试数据
        public static string TestUsername => GetEnvironmentVariable("TEST_USERNAME", "tomsmith");
        public static string TestPassword => GetEnvironmentVariable("TEST_PASSWORD", "SuperSecretPassword!");
        
        // 辅助方法
        private static string GetEnvironmentVariable(string name, string defaultValue)
        {
            return Environment.GetEnvironmentVariable(name) ?? defaultValue;
        }
    }
} 
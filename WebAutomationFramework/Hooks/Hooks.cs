using System;
using System.IO;
using TechTalk.SpecFlow;
using WebAutomationFramework.Drivers;
using WebAutomationFramework.Support;

namespace WebAutomationFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestLogger.Log("开始测试运行");
            
            // 清理旧的测试日志（可选）
            CleanupOldTestLogs();
        }
        
        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestLogger.Log("测试运行结束");
        }
        
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            TestLogger.Log($"开始执行功能: {featureContext.FeatureInfo.Title}");
        }
        
        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            TestLogger.Log($"功能执行完成: {featureContext.FeatureInfo.Title}");
        }
        
        [BeforeScenario]
        public void BeforeScenario()
        {
            TestLogger.Log($"开始执行场景: {_scenarioContext.ScenarioInfo.Title}");
            WebDriverFactory.InitializeDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                // 如果测试失败，捕获截图
                if (_scenarioContext.TestError != null)
                {
                    string screenshotName = $"Error_{_scenarioContext.ScenarioInfo.Title.Replace(" ", "_")}";
                    TestLogger.TakeScreenshot(screenshotName);
                    TestLogger.Log($"场景失败: {_scenarioContext.ScenarioInfo.Title}");
                    TestLogger.Log($"错误: {_scenarioContext.TestError.Message}");
                    TestLogger.Log($"堆栈跟踪: {_scenarioContext.TestError.StackTrace}");
                }
                else
                {
                    TestLogger.Log($"场景成功: {_scenarioContext.ScenarioInfo.Title}");
                }
            }
            finally
            {
                WebDriverFactory.CloseDriver();
                TestLogger.Log($"结束执行场景: {_scenarioContext.ScenarioInfo.Title}");
            }
        }
        
        [BeforeStep]
        public void BeforeStep()
        {
            // 记录每个步骤的开始
            TestLogger.Log($"开始执行步骤: {_scenarioContext.StepContext.StepInfo.Text}");
        }
        
        [AfterStep]
        public void AfterStep()
        {
            // 记录每个步骤的结果
            var stepStatus = _scenarioContext.StepContext.Status;
            TestLogger.Log($"步骤执行结果: {stepStatus}");
            
            // 如果步骤失败，立即截图
            if (stepStatus == ScenarioExecutionStatus.TestError)
            {
                string screenshotName = $"StepError_{_scenarioContext.StepContext.StepInfo.Text.Replace(" ", "_").Substring(0, Math.Min(50, _scenarioContext.StepContext.StepInfo.Text.Length))}";
                TestLogger.TakeScreenshot(screenshotName);
            }
        }
        
        private static void CleanupOldTestLogs()
        {
            // 可以选择删除过旧的日志文件和截图
            try
            {
                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestLogs");
                
                if (Directory.Exists(logDirectory))
                {
                    // 例如删除30天前的日志
                    DateTime cutoffDate = DateTime.Now.AddDays(-30);
                    
                    foreach (string file in Directory.GetFiles(logDirectory, "*.log"))
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        if (fileInfo.CreationTime < cutoffDate)
                        {
                            fileInfo.Delete();
                        }
                    }
                    
                    // 也处理截图
                    string screenshotDirectory = Path.Combine(logDirectory, "Screenshots");
                    if (Directory.Exists(screenshotDirectory))
                    {
                        foreach (string file in Directory.GetFiles(screenshotDirectory, "*.png"))
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            if (fileInfo.CreationTime < cutoffDate)
                            {
                                fileInfo.Delete();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"清理旧测试日志时出错: {ex.Message}");
            }
        }
    }
} 
# C# SpecFlow Selenium Web 自动化测试框架

这是一个基于C#、SpecFlow和Selenium WebDriver构建的Web自动化测试框架，采用Page Object Model (POM)设计模式。

## 功能特点

- **Page Object Model (POM)设计模式**：提高测试代码的可维护性和可重用性
- **多浏览器支持**：支持Chrome、Firefox和Edge浏览器
- **配置系统**：通过环境变量轻松配置测试参数
- **详细日志和自动截图**：包括测试步骤日志和失败时自动截图
- **HTML报告生成**：生成美观的SpecFlow测试报告
- **灵活的命令行选项**：支持不同的运行模式和过滤器

## 示例场景

框架包含一个示例测试场景，用于登录"The Internet"演示网站：http://the-internet.herokuapp.com/login

## 项目结构

```
WebAutomationFramework/
├── Features/          # SpecFlow特性文件
├── Pages/             # 页面对象模型类
├── Steps/             # SpecFlow步骤定义
├── Drivers/           # WebDriver设置和配置
├── Hooks/             # SpecFlow钩子（生命周期事件）
├── Support/           # 辅助类（配置、日志等）
└── TestResults/       # 测试结果和报告
```

## 先决条件

- .NET 8.0 SDK
- Chrome浏览器（默认）或Firefox/Edge

## 如何运行测试

1. 克隆此仓库
2. 进入项目目录
3. 执行以下命令运行测试:

```bash
cd WebAutomationFramework
./run_tests.sh
```

### 自定义测试运行

run_tests.sh脚本支持多种命令行选项：

```bash
# 显示帮助信息
./run_tests.sh --help

# 使用特定浏览器
./run_tests.sh --browser firefox

# 使用可见模式（非Headless）
./run_tests.sh --visible

# 仅运行特定标签的测试
./run_tests.sh --tag login

# 清理测试结果目录
./run_tests.sh --clean
```

## 生成HTML报告

运行测试后，HTML报告将自动生成在TestResults目录中：
- SpecFlow报告: TestResults/TestReport.html
- LivingDoc报告: TestResults/LivingDoc.html

测试日志和截图位于：
- 日志文件: bin/Debug/net8.0/TestLogs/
- 截图文件: bin/Debug/net8.0/TestLogs/Screenshots/

## 扩展框架

### 添加新的页面对象
在Pages目录下创建新的页面类，继承BasePage类：

```csharp
public class NewPage : BasePage
{
    // 定义元素定位器
    private By SomeElement => By.Id("element-id");
    
    // 定义页面操作方法
    public void SomeAction()
    {
        Click(SomeElement);
    }
}
```

### 添加新的测试场景
在Features目录下创建新的.feature文件：

```gherkin
Feature: NewFeature
    As a user
    I want to...
    So that...

Scenario: New test scenario
    Given ...
    When ...
    Then ...
```

然后在Steps目录下实现相应的步骤定义。 
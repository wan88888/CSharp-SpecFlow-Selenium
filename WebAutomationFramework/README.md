# Web Automation Framework

This is a web automation testing framework built with C#, SpecFlow, and Selenium WebDriver.

## Features

- Page Object Model (POM) design pattern
- Selenium WebDriver for browser automation
- SpecFlow for BDD (Behavior-Driven Development)
- HTML report generation

## Example Scenario

The framework includes an example test scenario for logging into "The Internet" demo website at http://the-internet.herokuapp.com/login.

## Project Structure

- **Features/** - Contains SpecFlow feature files with test scenarios
- **Pages/** - Contains Page Object Model classes
- **Steps/** - Contains SpecFlow step definitions
- **Drivers/** - Contains WebDriver setup and configuration
- **Hooks/** - Contains SpecFlow hooks for test setup and teardown

## Prerequisites

- .NET Core SDK
- Chrome browser

## How to Run Tests

1. Clone this repository
2. Navigate to the project directory
3. Run the tests using the following command:

```
dotnet test
```

## Generating HTML Reports

After running the tests, HTML reports will be generated in the TestResults folder.
To generate the HTML report manually, run:

```
dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
livingdoc test-assembly WebAutomationFramework.dll -t TestResults/TestResults.json
``` 
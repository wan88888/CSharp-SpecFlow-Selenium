#!/bin/bash

# Create directory for test results if it doesn't exist
mkdir -p TestResults

# Run the tests
echo "Running tests..."
dotnet test --logger:trx --logger "html;LogFileName=TestResults/TestReport.html"
TEST_RESULT=$?

# Find the generated TestExecution.json file
TEST_EXECUTION_JSON=$(find ./bin -name TestExecution.json -type f | head -1)

if [ -z "$TEST_EXECUTION_JSON" ]; then
    echo "Warning: TestExecution.json file not found"
else
    echo "Found test execution file: $TEST_EXECUTION_JSON"
    
    # Generate SpecFlow Living Doc HTML report
    echo "Generating HTML report..."
    if ! command -v livingdoc &> /dev/null
    then
        echo "Installing SpecFlow.Plus.LivingDoc.CLI tool..."
        dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
    fi
    
    # Copy the test execution file to the expected location
    cp "$TEST_EXECUTION_JSON" TestResults/TestReport.json
    
    # Generate the HTML report
    livingdoc test-assembly $(find ./bin -name WebAutomationFramework.dll | head -1) -t TestResults/TestReport.json -o TestResults/LivingDoc.html
    
    echo "HTML report generated at TestResults/LivingDoc.html"
fi

echo "Test execution completed with status code: $TEST_RESULT"

exit $TEST_RESULT 
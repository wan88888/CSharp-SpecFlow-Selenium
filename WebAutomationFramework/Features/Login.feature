Feature: Login
    As a user
    I want to login to the website
    So that I can access my account

Scenario: Successful login with valid credentials
    Given I am on the login page
    When I enter username "tomsmith"
    And I enter password "SuperSecretPassword!"
    And I click the login button
    Then I should see a successful login message 
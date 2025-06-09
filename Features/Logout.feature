Feature: Logout
 As a registered user
  I want to logout from the application
  So that I can turn off my device safely

  Scenario: Logout after logging in with valid credentials
    Given I am logged in with valid credentials
    When I click the logout button
    Then I should be redirected to the login page
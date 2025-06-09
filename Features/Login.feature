Feature: Login to AutomationExercise
  As a registered user
  I want to log into the application
  So that I can access my account

  Scenario: Valid Login
    Given I am on the login page
    When I login with valid credentials
    Then I should be logged in successfully
Feature: Invalid Login
  As a user
  I want to be prevented from logging in with incorrect credentials
  So that my account remains secure

  Scenario: Login with incorrect email and password
    Given I am on the login page
    When I enter invalid credentials
    And I click the login button
    Then I should see an error message saying "Your email or password is incorrect!"
Feature: Register New User
  As a new visitor
  I want to create an account
  So that I can use the application with my user profile

  Scenario: Successfully registering a new user
    Given I am on the homepage
    When I navigate to the Signup/Login page
    And I enter a valid name and email address
    And I click the Signup button
    And I fill in all required personal and address details
    And I click the Create Account button
    Then I should see a confirmation message saying "Account Created!"
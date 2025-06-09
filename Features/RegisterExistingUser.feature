Feature: Register User With Existing Email
  As a user
  I want to be prevented from registering with an already existing email
  So that duplicate accounts are not created

  Scenario: Attempting to register with an existing email
    Given I am on the homepage
    When I navigate to the Signup/Login page
    And I enter an existing username and email address
    And I click the Signup button
    Then I should see an error message saying "Email Address already exist!"
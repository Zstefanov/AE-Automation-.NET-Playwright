Feature: Verify Test Cases Page
  As a user
  I want to view the test cases page
  So that I can verify its contents

  Scenario: Navigate to Test Cases page
    Given I am on the homepage
    When I click the Test Cases link
    Then I should be on the Test Cases page
Feature: Verify Subscription In Home Page
  As a visitor
  I want to subscribe to the newsletter
  So that I can receive updates from the website

  Scenario: Successful subscription from homepage
    Given I am on the homepage
    And the homepage is fully loaded
    When I scroll to the footer
    And I enter a random email and click subscribe
    Then I should see a subscription success message
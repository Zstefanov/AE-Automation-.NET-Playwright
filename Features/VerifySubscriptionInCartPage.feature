Feature: Verify Subscription In Cart Page
  As a user on the cart page
  I want to subscribe to the newsletter
  So that I can receive email updates

  Scenario: Successful subscription from cart page
    Given I am on the homepage
    And the homepage is fully loaded
    When I navigate to the Cart page
    And I scroll to the footer
    And I enter a random email and click subscribe
    Then I should see a subscription success message
Feature: Add To Cart From Recommended Items
  As a user
  I want to add a recommended item to the cart
  So that I can purchase it easily

  Scenario: Successfully add a recommended item to the cart
    Given I am on the homepage
    When I scroll to the footer
    Then I should see the "recommended items" section
    When I add the first recommended item to the cart
    And I view the cart
    Then I should see the recommended product in the cart
Feature: Place Order While Registering During Checkout
  As a new user
  I want to register during checkout
  So that I can place an order successfully

  Scenario: Register during checkout and place an order successfully
    Given I am on the homepage
    When I add a product to the cart
    And I proceed to checkout
    And I choose to register during checkout
    And I complete registration
    Then I should be logged in after registration
    When I proceed to checkout again
    Then delivery and billing addresses should match
    When I place the order with payment details
    Then the order should be confirmed
    And I delete the account
    Then the account should be deleted successfully
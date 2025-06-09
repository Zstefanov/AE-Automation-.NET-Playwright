Feature: Place Order After Registering Before Checkout
  As a user
  I want to register before checking out
  So that I can place an order with my newly created account

  Scenario: Register before checkout and place an order successfully
    Given I am on the homepage
    When I register a new user
    Then I should be logged in successfully
    When I add a product to the cart
    And I proceed to checkout
    Then delivery and billing addresses should match
    When I place the order with payment details
    Then the order should be confirmed
    And I delete the account
    Then the account should be deleted successfully

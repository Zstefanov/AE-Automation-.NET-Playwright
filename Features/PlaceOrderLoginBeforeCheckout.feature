Feature: Place Order After Login
  As a registered user
  I want to log in before checkout
  So that I can place an order successfully

  Scenario: Place order after logging in
    Given I am on the homepage
    And I log in with valid credentials
    When I add a product to the cart
    And I proceed to checkout
    Then delivery and billing addresses should match
    When I place the order with saved user payment details
    Then the order should be confirmed
Feature: Download Invoice After Purchase Order
  As a registered user
  I want to download an invoice after placing an order
  So that I can have a record of my purchase

  Scenario: Successfully download invoice after placing an order
    Given I am on the homepage
    When I add a product to the cart
    And I proceed to checkout
    And I navigate to the registration page
    And I register a new user
    Then I should be logged in as that user
    When I go to the cart and proceed to checkout
    Then delivery and billing addresses should match
    When I place the order with payment details
    Then I should be able to download the invoice
    And I should see "Account Deleted!" confirmation after deleting my account
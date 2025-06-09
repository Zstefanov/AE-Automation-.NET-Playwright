Feature: Add Products To Cart
  As a logged-in user
  I want to add products to the cart
  So that I can proceed to checkout with accurate pricing and quantities

  Scenario: Successfully add two products to cart and verify details
    Given I am logged in as a valid user
    And I am on the homepage
    When I navigate to the Products page
    And I add two different products to the cart
    And I view the cart
    Then I should see both products in the cart
    And the product details and total price should be correct
Feature: Verify Product Quantity In Cart
  As a user
  I want to set a specific quantity before adding a product to the cart
  So that I can verify it reflects correctly in the cart

  Scenario: Add a product with quantity 4 and verify it appears correctly in the cart
    Given I am on the homepage
    When I view the first product
    And I set the quantity to 4
    And I add the product to the cart
    And I view the cart
    Then the product should appear with quantity 4 in the cart
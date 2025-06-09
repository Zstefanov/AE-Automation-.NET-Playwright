Feature: Remove Products From Cart
  As a user
  I want to remove products from my cart
  So that I can manage the contents before checking out

  Scenario: Remove a product after adding multiple to the cart
    Given I am on the homepage
    When I add multiple products to the cart
    And I view the cart
    Then the cart should contain the correct number of products
    When I remove the first product from the cart
    Then the product should no longer be visible in the cart
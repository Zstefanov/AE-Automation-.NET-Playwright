Feature: Search Products And Verify Cart After Login
  As a user, I want to search for products and ensure my cart is retained after logging in.

  Scenario: Search for a product and verify it remains in cart after login
    Given I am on the homepage
    When I go to the Products page
    Then I should see the All Products page
    When I search for "Men Tshirt"
    Then I should see search results for "Men Tshirt"
    When I add the product with id "2" to the cart
    And I view the cart
    And I log in with valid credentials
    When I go to the Cart page
    Then I should see "Men Tshirt" in the cart
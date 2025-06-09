Feature: Verify All Products And Product Detail Page
  As a user
  I want to view the products list and detailed product information
  So that I can verify product visibility and information accuracy

  Scenario: Navigate to All Products and verify product details
    Given I am on the homepage
    When I click the Products link
    Then I should see the All Products page
    And I should see a list of products
    When I click the View Product link for the first product
    Then I should be on the Product Detail page
    And I should see product details like name, category, price, availability, condition, and brand
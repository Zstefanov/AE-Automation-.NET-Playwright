Feature: View Category Products
  As a user
  I want to view products by category
  So that I can browse relevant items

  Scenario: User views products in different categories
    Given I am on the homepage
    When I expand the "Women" category
    And I select the "Dress" subcategory
    Then I should see products for "WOMEN - DRESS PRODUCTS"
    When I expand the "Men" category
    And I select the "Jeans" subcategory
    Then I should see products for "MEN - JEANS PRODUCTS"
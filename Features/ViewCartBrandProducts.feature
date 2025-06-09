Feature: View Cart Brand Products
  As a user, I want to view products by brand so I can filter and browse items more easily.

  Scenario: User views products by selecting different brand filters
    Given I am on the homepage
    When I go to the Products page
    Then I should see the Brands section
    When I select the "H&M" brand
    Then I should see products filtered by brand "H&M"
    When I select the "POLO" brand
    Then I should see products filtered by brand "POLO"
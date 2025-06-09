Feature: Search Product
  As a user
  I want to search for products by name
  So that I can quickly find specific items

  Scenario Outline: Static search using known product names
    Given I am on the homepage
    When I navigate to the Products page
    And I search for "<ProductName>"
    Then the search results should contain "<ProductName>"

    Examples:
      | ProductName          |
      | Blue Top             |
      | Men Tshirt           |
      | Lace Top For Women   |

  Scenario: Search using a random visible product name
    Given I am on the homepage
    When I navigate to the Products page
    And I search for a randomly selected product
    Then the search results should contain that product

  Scenario Outline: Data-driven search from external JSON
    Given I am on the homepage
    When I navigate to the Products page
    And I search for "<SearchTerm>" from external data
    Then the product "<SearchTerm>" should be <ExpectedResult>

    Examples:
      | SearchTerm       | ExpectedResult |
      | Blue Top         | true           |
      | NonexistentItem  | false          |
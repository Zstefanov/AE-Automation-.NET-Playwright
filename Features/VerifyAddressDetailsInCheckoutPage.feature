Feature: Verify address details in checkout page

  Scenario: Delivery and billing address details should match
    Given I am on the homepage
    When I register a new user for checkout verification
    Then I should be logged in as that user
    When I add a product to the cart
    And I proceed to checkout
    Then delivery and billing addresses should match
    When I delete my account
    Then I should see "Account Deleted!" confirmation
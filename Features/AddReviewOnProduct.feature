Feature: Add Review On Product
  As a user
  I want to write a review on a product
  So that I can share my feedback

  Scenario: Successfully submit a review on a product
    Given I am on the homepage
    When I go to the Products page
    Then I should see the All Products page
    When I click on any "View Product" link
    Then I should see the "Write Your Review" section
    When I fill in the review form with name, email, and comment
    And I submit the review
    Then I should see a success message saying "Thank you for your review."
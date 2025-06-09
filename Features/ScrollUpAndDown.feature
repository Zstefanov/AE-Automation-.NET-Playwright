Feature: Scroll Up And Scroll Down
  As a user
  I want to scroll to the bottom and back to the top using the scroll arrow
  So that I can confirm page navigation via scroll works properly

  Scenario: Scroll up using arrow button and verify top content
    Given I am on the homepage
    When I scroll to the bottom of the page
    Then I should see the "Subscription" section
    When I click the scroll up arrow
    Then I should see the top message "Full-Fledged practice website for Automation Engineers"
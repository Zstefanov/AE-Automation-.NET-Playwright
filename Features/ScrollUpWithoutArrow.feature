Feature: Scroll Up Without Using Arrow Button

  Scenario: Scroll up using browser's scroll functionality and verify top content
    Given I am on the homepage
    When I scroll to the bottom of the page
    Then I should see the "Subscription" section
    When I scroll to the top of the page
    Then I should see the top message "Full-Fledged practice website for Automation Engineers"
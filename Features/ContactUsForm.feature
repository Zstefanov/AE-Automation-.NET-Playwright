Feature: Contact Us Form
  As a user
  I want to submit the contact form with my message and file
  So that I can communicate with the website

  Scenario: Successfully submit the Contact Us form
    Given I am on the homepage
    When I navigate to the Contact Us page
    And I fill out the contact form with valid details
    And I upload a file
    And I submit the contact form
    Then I should see the success message "Success! Your details have been submitted successfully."
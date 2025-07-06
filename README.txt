This project is an automation project built with C#, NUnit, Playwright.

The project is deployed to Jenkins, Docker (locally), and GitHub Actions.

It has parameterized launch options for all types of browsers via CL:
$env:BROWSER="browserType"; dotnet test

The project uses BOGUS for generating test data.

The project has different types of test loading - static data [TestCase] attribute, dynamic data, 
data-driven approach with JSON file uploads.

There are different helpers created for user registration, test data generation, etc. 

A BDD approach is implemented using: SpecFlow / folders - Features, Hooks, StepDefinitions.
A "CommonSteps.cs" file is created to avoid ambiguity of repeating steps in feature files.

Based on the fact that all login credentials and data is created purely for test purposes, 
config files, appsettings with login credentials are uploaded as well. 

ORM Implementation -> EFC used to create a local db, save data into it using API calls.

Load testing to be implemented with Nbomber in a sister project.

The project covers a test website: https://automationexercise.com and the following test cases:
Test Case 1-26 are covered by a feature file (SpecFlow) located in the features dir.

Test Case 1: Register User  
Test Case 2: Login User with correct email and password  
Test Case 3: Login User with incorrect email and password  
Test Case 4: Logout User  
Test Case 5: Register User with existing email  
Test Case 6: Contact Us Form  
Test Case 7: Verify Test Cases Page  
Test Case 8: Verify All Products and product detail page  
Test Case 9: Search Product  
Test Case 10: Verify Subscription in home page  
Test Case 11: Verify Subscription in Cart page  
Test Case 12: Add Products in Cart  
Test Case 13: Verify Product quantity in Cart  
Test Case 14: Place Order: Register while Checkout  
Test Case 15: Place Order: Register before Checkout  
Test Case 16: Place Order: Login before Checkout  
Test Case 17: Remove Products From Cart  
Test Case 18: View Category Products  
Test Case 19: View & Cart Brand Products  
Test Case 20: Search Products and Verify Cart After Login  
Test Case 21: Add review on product  
Test Case 22: Add to cart from Recommended items  
Test Case 23: Verify address details in checkout page  
Test Case 24: Download Invoice after purchase order  
Test Case 25: Verify Scroll Up using 'Arrow' button and Scroll Down functionality  
Test Case 26: Verify Scroll Up without 'Arrow' button and Scroll Down functionality  

It additionally covers 14 API tests for the following:

API 1: Get All Products List  
API 2: POST To All Products List  
API 3: Get All Brands List  
API 4: PUT To All Brands List  
API 5: POST To Search Product  
API 6: POST To Search Product without search_product parameter  
API 7: POST To Verify Login with valid details  
API 8: POST To Verify Login without email parameter  
API 9: DELETE To Verify Login  
API 10: POST To Verify Login with invalid details  
API 11: POST To Create/Register User Account  
API 12: DELETE METHOD To Delete User Account  
API 13: PUT METHOD To Update User Account  
API 14: GET user account detail by email
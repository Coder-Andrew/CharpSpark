@Scot
Feature: Home "Hello World" for Reqnroll and Selenium

A short summary of the feature

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
    And I logout

Scenario: Home page title contains "Home Page - ResuMeta"
	Given I am a visitor
	When I am on the "Home" page
	Then The page title contains "Home Page - ResuMeta"

Scenario: Home page has a Register button
	Given I am a visitor
	When I am on the "Home" page
	Then The page presents a Register button

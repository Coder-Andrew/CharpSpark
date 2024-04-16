@DanielleC
Feature: CHARP-139

We want users to be able to have a central location to track the jobs that they have applied to. 
The user will manually enter and delete these entried in the table.
By doing this, users can keep up to date with all of the details/important deadlines around jobs they are applying to. 


Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
    And I am on the "Home" page
    And I logout


Scenario: User visits the application tracker page
Given I am a user with the first name 'Adrian'
    And I login
    And I am on the "ApplicationTracker" page
Then I will be able to see a table one entry that tells me I have no application trackers

Scenario: User adds job to application tracker
Given I am a user with the first name 'Adrian'
    And I login
    And I am on the "ApplicationTracker" page
When I submit my information in the ApplicationTracker page form
Then I will be able to see a table row containing my ApplicationTracker page form response

Scenario: User adds job to application tracker
Given I am a user with the first name 'Adrian'
    And I login
    And I am on the "ApplicationTracker" page
    And I can see my jobs on the ApplicationTracker page form
When I filter my applications by application deadline ascending
Then I will be able to see the jobs in my table in ascending order of application deadline
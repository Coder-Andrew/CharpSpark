@ZoeR
Feature: CHARP-29
Create a page that acts as a dashboard for all the previously saved resumes. 
Users can click these resumes and be redirected to view them.
Sprint 4 added in the ability to view cover letters to the dashboard as well.
Users can click these cover letters and be redirected to view them.
These tests are to ensure that the dashboard is functioning as expected, with those functions being:
- The current user viewing their saved Resumes
- The current user viewing their saved Cover Letters
- The current user being able to click on a Resume and be redirected to view it
- The current user being able to click on a Cover Letter and be redirected to view it


Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
    And I am on the "Home" page
    And I logout

Scenario: Users can view their saved resumes in a "Your Resume" section
Given I am a user with the first name 'Adrian'
    And I login
    And I am on the "YourDashboard" page
Then I should see a "Your Resume" section
    And I should see a list of my saved resumes with their titles

Scenario: Users can view their saved cover letters in a "Your Cover Letters" section
Given I am a user with the first name 'Adrian'
    And I login
    And I am on the "YourDashboard" page
Then I should see a "Your Cover Letters" section
    And I should see a list of my saved cover letters with their titles

Scenario: Users can click on a resume and be redirected to view it
Given I am a user with the first name 'Adrian'
    And I login
    And I am on the "YourDashboard" page
When I click on a resume
Then I should be redirected to the "YourResume" page

Scenario: Users can click on a cover letter and be redirected to view it
Given I am a user with the first name 'Adrian'
    And I login
    And I am on the "YourDashboard" page
When I click on a cover letter
Then I should be redirected to the "YourCoverLetter" page
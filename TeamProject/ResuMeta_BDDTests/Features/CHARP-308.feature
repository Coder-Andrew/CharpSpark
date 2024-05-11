@AndrewE
Feature: CHARP-308
We want a way for users to be able to improve their resumes based
on a given job description. This will allow users the ability to 
generate resumes which are tailored to the job they would like to
apply to, saving the user time, energy, and stress. 

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
	And I am on the "YourDashboard" page
	And the following users has at least one resume
      | Email              | Resumes |
      | reynoldsa@mail.com | 1       |


Scenario: A user should be able to see the "Improve with AI" button
Given I am a user with the first name 'Adrian'
	And I login
	And I am on the "JobListings" page
When I click on a job listing
Then I should see the "Improve with AI" button


Scenario: A user should be able directed to the improve resume page after clicking the "Improve with AI" button
Given I am a user with the first name 'Adrian'
	And I login
	And I am on the "JobListings" page
	And I click on a job listing
When I click the "Improve with AI" button
Then I should be redirected to the "YourDashboard" page


Scenario: A user should be able to click a resume they'd like to improve after clicking the "Improve with AI" button
Given I am a user with the first name 'Adrian'
	And I login
	And I am on the "JobListings" page
	And I click on a job listing
	And I click the "Improve with AI" button
	And I am redirected to the "YourDashboard" page
When I click on a resume to improve
Then I should be redirected to the "ImproveResume" page

Scenario: A user should be able to see improvements on their resume after selecting a resume they'd like to improve
Given I am a user with the first name 'Adrian'
	And I login
	And I am on the "JobListings" page
	And I click on a job listing
	And I click the "Improve with AI" button
	And I am redirected to the "YourDashboard" page
	And I click on a resume to improve
	And I am redirected to the "ImproveResume" page
When I wait long enough
Then I should see the "Job Description" text-area
	And an improved resume
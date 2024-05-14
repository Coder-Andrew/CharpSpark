@DanielleC
Feature: CHARP-311
Currently, users have to write cover letters by hand for each job they apply to. 
This can become tedious and time consuming when applying to a large number of jobs. 
Currently, after a user clicks on a job from the job listing page, a modal pops up and asks if you applied to the job. In this story, a button will be added to that modal that will say “Create cover letter”. 
Upon clicking the button, the user will be prompted to choose which of their resumes will be fed to AI along the job description from the job listing.

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
	And I am on the "YourDashboard" page
	And the following user has at least one resume
      | Email              | Resumes |
      | reynoldsa@mail.com | 1       |


Scenario: User should be able to see the "Create Cover Letter" button
Given I am a user with the first name 'Adrian'
	And I login
	And I am on the "JobListings" page
When I click on a random job listing
Then I should see a "Create Cover Letter" button

# Scenario: User should be able to click the "Create Cover Letter" button
# Given I am a user with the first name 'Adrian'
# 	And I login
# 	And I am on the "JobListings" page
# When I click on a random job listing
# When I click the "Create Cover Letter" button
# Then I should see a dropdown selection appear
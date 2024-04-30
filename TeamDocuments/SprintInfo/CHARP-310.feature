@RyanH
Feature: CHARP-310
Implementation of Public Profiles & the Ability to Post a Resume For Others to See
So far, ResuMeta offers a lot of functionality and features to its' users, 
however there is no current way for users to be able to find other users, 
or for users to post their resumes for others to see. This feature will enable 
a user to create a public profile in which others can view, as well as the ability 
to choose a resume of theirs that they would like to share with the world. 

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login

Scenario: User will be able to see on the "Profiles" tab on the navbar
Given I am a user with the first name 'Adrian'
   And I login
 When I am on the home page
 Then I should see a tab on the navbar called "Profiles"

Scenario: User will be able to click on the "Profiles" tab on the navbar to be redirected to the Profiles page
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the home page
   And I dont already have a public profile
When I click on the "Profiles" section of the navbar
Then I should see a page letting me know I dont have a public profile
   And I should see a button to create a public profile

Scenario: User will be able to click on the "Create Public Profile" button to be redirected to the Public Profile form
Given I am a user with the first name 'Adrian'
   And I login
   And I dont already have a public profile
   And I am on my public profile page
When I click on the "Create Public Profile" button
Then I should see a form where I can input information for my profile

Scenario: User will be able to see their information displayed on their public profile page
Given I am a user with the first name 'Adrian'
   And I login
   And I have created a public profile
When I click on the "Profiles" section of the navbar
Then I should see the information I used to create my profile displayed on the page
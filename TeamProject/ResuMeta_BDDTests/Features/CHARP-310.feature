@RyanH
Feature: CHARP-310
Implementation of Public Profiles & the Ability to Post a Resume For Others to See
So far, ResuMeta offers a lot of functionality and features to its' users, 
however there is no current way for users to be able to find other users, 
or for users to post their resumes for others to see. This feature will enable 
a user to create a public profile in which others can view, as well as the ability 
to choose a resume of theirs that they would like to share with the world. 

Scenario: User will be able to see on the "Profile" tab on the navbar
Given I am a random user
When I am on the "Home" page
Then I should see a tab on the navbar called "Profile"

Scenario: User will be able to click on the "Profiles" tab on the navbar to be redirected to the Profiles page
Given I am a random user
   And I have created a resume
   And I am on the "Home" page
When I click on the "Profile" tab of the navbar
Then I should see a page where I can create a profile

Scenario: User will be able to create a Profile
Given I am a random user
   And I have created a resume
   And I am on the "Home" page
   And I click on the "Profile" tab of the navbar
When I fill out the form for "CreateProfile"
Then I should be rerouted to the "YourProfile" page

Scenario: User will be able to see their information displayed on their public profile page
Given I am a random user
   And I have created a resume
   And I have created a public profile
   And I am on the "Home" page
When I go to the "YourProfile" page
Then I should see the information I used to create my profile displayed on the page
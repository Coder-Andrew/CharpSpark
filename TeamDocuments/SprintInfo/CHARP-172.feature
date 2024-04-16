@RyanH
Feature: CHARP-172
The CreateResume form has been a feature that has had a lot of work done 
through the lifetime of this project. As of now, it works great, however it 
contains a lot of information in one view that can overwhelm the user and make 
it more difficult to traverse. The goal of this story is to modify this form to 
look more like a “Wizard” that as multiple tabs to enable the user to easily go 
back and forth between tabs and modify information from different sections in the form.

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login

Scenario: User should be able to see and click on the tabs on the CreateResume page
Given I am a user
   And I have logged in
When I am on the “CreateResume” page
Then I should be able to see and click on the 5 tabs named “Education”, “Employment”, “Skills & Achievements”, “Projects” and “Personal Summary”

Scenario: User should see the “Education” tab opened by default on the CreateResume page
Given I am a user
   And I have logged in
When I am on the “CreateResume” page
Then I should see that the first tab opened should be “Education”

Scenario: User should be able to click on a tab and see the form section for that tab
Given I am a user
   And I have logged in
   And I am on the “CreateResume” page
When I click on the “Projects” tab
Then I should see the “Projects” form section displayed

Scenario: User should be able to fill out the form sections for the tabs “Education”, “Employment” and “Projects” and be able to submit the form
Given I am a user
   And I have logged in
   And I am on the “CreateResume” page
   And I have filled out the form sections for the tabs “Education”, “Employment” and “Projects”
When I click on the “Submit” button on the form
Then I should be redirected to the “ViewResume” page with the information I inputted into each tab section of the form displayed in a text editor
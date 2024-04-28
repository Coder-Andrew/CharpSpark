@RyanH
Feature: CHARP-172
The CreateResume form has been a feature that has had a lot of work done 
through the lifetime of this project. As of now, it works great, however it 
contains a lot of information in one view that can overwhelm the user and make 
it more difficult to traverse. The goal of this story is to modify this form to 
look more like a "Wizard" that as multiple tabs to enable the user to easily go 
back and forth between tabs and modify information from different sections in the form.

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
    And I logout

Scenario: User should be able to see all the tabs on the CreateResume page
Given I am a user with the first name 'Adrian'
   And I login
When I am on the "CreateResume" page
Then I should be able to see the 5 tabs named "Education", "Employment", "Skills & Achievements", "Projects" and "Personal Summary"

Scenario: User should be able to see the "Education" opened by default on the CreateResume page
Given I am a user with the first name 'Adrian'
   And I login
When I am on the "CreateResume" page
Then I should see that the first tab opened should be "Education"

Scenario: User should be able to click on the "Employment" tab and see the form section for that tab
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
When I click on the "Employment" tab
Then I should see the "Employment" form section displayed

Scenario: User should be able to click on the "Skills & Achievements" tab and see the form section for that tab
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
When I click on the "Skills & Achievements" tab
Then I should see the "Skills & Achievements" form section displayed

Scenario: User should be able to click on the "Projects" tab and see the form section for that tab
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
When I click on the "Projects" tab
Then I should see the "Projects" form section displayed

Scenario: User should be able to click on the "Personal Summary" tab and see the form section for that tab
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
When I click on the "Personal Summary" tab
Then I should see the "Personal Summary" form section displayed

Scenario: User should be able to traverse the form by using the "Next" button
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
When I click on the "Next" button
Then I should see the "Employment" form section displayed

Scenario: User should be able to traverse the form by using the "Previous" button
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
When I click on the "Next" button
   And I click on the "Previous" button
Then I should see the "Education" form section displayed

Scenario: User should not be able to see the "Previous" button on the "Education" tab
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
When I click on the "Education" tab
Then I should not see the "Previous" button

Scenario: User should not be able to see the "Next" button on the "Personal Summary" tab
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
When I click on the "Personal Summary" tab
Then I should not see the "Next" button

Scenario: User should be able to fill out the form sections for the tabs "Education", "Employment" and "Projects" and be able to submit the form
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
   And I have filled out the form sections for the tabs "Education", "Employment" and "Projects"
When I click on the "Submit" button on the form
Then I should be redirected to the "ViewResume" page with only the information I inputted into each tab section of the form displayed in a text editor
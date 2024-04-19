@RyanH
Feature: CHARP-96
Implement WYSIWYG editor to be able to edit, modify, and customize resumes.
The first sprint set up the functionality to get information from the user, 
    however it only redisplayed that inputted information back to them. 
    As of currently, there is no way for a user to modify the look of their resume.
This feature will allow users to edit, modify, and customize their resumes in a WYSIWYG editor.

We want to implement the basic functionality here:
Information from the “Create Resume” form will be displayed in a somewhat structured manner inside of the WYSIWYG editor after form submission
Users can edit the information inside of the editor as they see fit
Users can save their WYSIWYG content to the applications DB
Will be using the Quill library for implementation
Resumes created will be required to have a title before they can be saved

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
    And I logout


Scenario: User can create a resume and view it in a WYSIWYG editor
Given I am a user with the first name 'Adrian'
     And I login
     And I am on the "CreateResume" page
When I submit my information in the CreateResume page form
Then I will be redirected to the "ViewResume" page with a WYSIWYG editor

Scenario: User can edit their resume in the WYSIWYG editor
Given I am a user with the first name 'Adrian'
     And I login
     And have been redirected to the "ViewResume" page
When I type into the WYSIWYG editor
Then I will be able to see the editor modify the content

Scenario: User can save their resume
Given I am a user with the first name 'Adrian'
     And I login
     And have been redirected to the "ViewResume" page
When I click on the "Save Resume" button
Then I will be able to see a confirmation message letting me know the resume saved successfully

Scenario: User can export their resume to pdf
Given I am a user with the first name 'Adrian'
     And I login
     And have been redirected to the "ViewResume" page
     And I have saved my resume
When I click on the "Export Resume" button
Then I will be able to see a confirmation message letting me know the resume exported successfully
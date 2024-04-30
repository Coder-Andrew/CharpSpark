@DanielleC
Feature: CHARP-202

We want a way for users who have resumes in the system to improve their resumes in a user-friendly way. 
We want a way for that user to indicate that they'd like ChatGPT to improve their resume without overwriting their current one, unless the user wishes to. 
This will entail sending a user's resume to ChatGPT, preview the changes that ChatGPT suggest, and allow the user to either accept or regenerate until they find one they like, and then allow the user to save the new resume to the database. 


Background:
    Given the following users exist
      | UserName               | Email                  | FirstName  | LastName    | Password     |
      | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
    And I am on the "Home" page
    And I logout


Scenario: User can create a resume and see option to improve with AI
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
   And I have filled out the Employment and Projects sections of the form
When I click the "Submit" button on the form
Then I should be redirected to the "ViewResume" page 
Then I will be see a button that says "Improve With AI"

Scenario: Users can view the ImproveResume page and see the option to regenerate
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateResume" page
   And I have filled out the Employment and Projects sections of the form
When I click the "Submit" button on the form
Then I should be redirected to the "ViewResume" page 
When I click the "Improve With AI" button
Then I should be taken to the "ImproveResume" page


# Scenario: Users has the option to regenerate AI generated resume
# Given I am a user with the first name 'Adrian'
#     And I login
#     And I am on the "YourDashboard" page
# When I click on a specific resume
# Then I should be redirected to the "YourResume" page
# When I click a "Improve With AI" button
# Then I should be taken to the "ImproveResume" page
# Then I should see the button that says "Regenerate"



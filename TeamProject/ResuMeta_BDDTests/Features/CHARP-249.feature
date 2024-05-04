@ZoeR
Feature: CHARP-249

We want a way for users who have cover letters in the system to improve their cover letters in a user-friendly way. 
We want a way for that user to indicate that they'd like ChatGPT to improve their cover letter without overwriting their current one, unless the user wishes to. 
This will entail sending a user's cover letter to ChatGPT, preview the changes that ChatGPT suggest, and allow the user to either accept or regenerate until they find one they like, and then allow the user to save the new resume to the database. 


Background:
    Given the following users exist
      | UserName               | Email                  | FirstName  | LastName    | Password     |
      | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
    And I am on the "Home" page
    And I logout


Scenario: User can create a cover letter and see option to improve with AI
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateCoverLetter" page
   And I have filled out the cover letter form
When I click the "Submit" button
Then I should be redirected to the "ViewCoverLetter" page 
Then I will be see a button saying "Improve With AI"

Scenario: Users can view the ImproveCoverLetter page and see the option to regenerate
Given I am a user with the first name 'Adrian'
   And I login
   And I am on the "CreateCoverLetter" page
   And I have filled out the cover letter form
When I click the "Submit" button
Then I should be redirected to the "ViewCoverLetter" page 
When I click "Improve With AI" button
Then I should be taken to the "ImproveCoverLetter" page


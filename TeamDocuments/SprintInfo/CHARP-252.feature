@RyanH
Feature: CHARP-252
Integration of Job Listings and Application Tracker features
In sprint 4, we implemented a web scraper that scrapes job 
listings and redisplays them on our site for the user to be 
able to view and access. The idea behind this feature was to 
enable users to stay within our site to track job applications 
and find job listings to apply for. Now, in this story, we want 
to tie together these two features so that when a user has applied 
for a job via the job listings feature, they can easily track their 
application back over within the application tracker feature.

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login

Scenario: User will get a message box asking if they have sent an application to the job listing they clicked on
Given I am a user
   And I have logged in
   And I am on the “Job Listings” page
When I click on a Job Listing
   And I have redirected back to ResuMeta
Then I will be able to see a message box asking me if I have sent an application to the job listing I clicked on\

Scenario: User will be able to click on the "No" button in the message box to close the message box
Given I am a user
   And I have logged in
   And I am on the “Job Listings” page
   And I have clicked on a Job Listing
When I click on the “No” button in the message box
Then I will see the message box disappears 
  And I will remain on the “Job Listings” page

Scenario: User will be able to click on the "Yes, I applied" button in the message box to be redirected to the Application Tracker page
Given I am a user
   And I have logged in
   And I am on the “Job Listings” page
   And I have clicked on a Job Listing
When I click on the “Yes, I applied” button in the message box
Then I will be redirected to the “ApplicationTracker” page

Scenario: User will see a new Application form partially filled out with some of the information from the job listing they clicked on
Given I am a user
   And I have logged in
   And I am on the “Job Listings” page
   And I have clicked on a Job Listing
   And I clicked on the “Yes, I applied” button in the message box
When I am redirected to the “ApplicationTracker” page
Then I will see a new Application form partially filled out with some of the information from the job listing I clicked on
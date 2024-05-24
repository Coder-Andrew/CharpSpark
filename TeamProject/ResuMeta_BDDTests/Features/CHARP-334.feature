@RyanH
Feature: CHARP-334
In Sprint 6, ResuMeta added the functionality to be able to create a 
profile and share it to the world. In this story, functionality will be 
implemented to allow users to follow others so that they can keep track 
of the profiles they admire or know. On a users public profile, 
logged in users will have the ability to press the “Follow” button 
in order to follow that user, and once they have followed that user, 
they will be given the option to unfollow if they so please. 

Scenario: A user can see their follower and following count
Given I am a random user
    And I have created a resume
    And I have created a public profile
When I go to the "YourProfile" page
Then I can see my follower and following count

Scenario: A user can see the follow button on someone elses profile
Given I am a random user
When I navigate to someone elses profile
Then I can see I have an option to follow them

Scenario: A user can follow someone elses profile
Given I am a random user
And I navigate to someone elses profile
When I click on "Follow"
Then I can see they have one more follower

Scenario: A user can see who follows them
Given I am a random user
   And I have created a resume
   And I have created a public profile
   And I go to the "YourProfile" page
When I click on "Followers"
Then I can see all the users who follow me
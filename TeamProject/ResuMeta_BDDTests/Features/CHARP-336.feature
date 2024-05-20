@RyanH
Feature: CHARP-336
In Sprint 6, ResuMeta added the functionality to be able to 
create a profile and share it to the world. In this story, 
functionality will be implemented to allow users to upvote or 
downvote on other users showcased resume on their public profile. 
This will allow users to give feedback to others about their resume, 
as well as create some motivation for users to create a stronger resume 
that catches the eyes of others.

Scenario: User will be able to see the vote count tied to their resume
Given I am a random user
   And I have created a resume
   And I have created a public profile
When I go to the "YourProfile" page
Then I can see the vote count tied to my resume

Scenario: User will be able to see the vote count tied to someone elses resume
Given I am a random user
When I navigate to someone elses profile
Then I can see I have an option to upvote or downvote their resume

Scenario: User will be able to upvote someone elses resume
Given I am a random user
And I navigate to someone elses profile
When I click on "UpVote"
Then I can see they have one more upvote

Scenario: User will be able to downvote someone elses resume
Given I am a random user
And I navigate to someone elses profile
When I click on "DownVote"
Then I can see they have one more downvote
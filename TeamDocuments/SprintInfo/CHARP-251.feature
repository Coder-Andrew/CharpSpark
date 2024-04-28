@AndrewE
Feature: CHARP-251
We want users and visitors to have the ability to search through and sort
remote job listings. This will entail modifying the scraper’s endpoints 
to allow people to search through the cached job listings effectively 
and easily. We also would like a more cohesive and interactive way 
for people to navigate to other pages of results (pagination).


Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login

Scenario: Visitor can see search bar
Given I am a visitor
When I navigate to the “Job Listings” page
Then I should see a search bar that prompts me to search for jobs

Scenario: User can see search bar
Given I am a user
	And I login
When I navigate to the “Job Listings” page
Then I should see a search bar that prompts me to search for jobs

Scenario: Visitor can search for job listings
Given I am a visitor
	And I am on the “Job Listings” page
When I type “Software Engineer” into the search bar
Then I see only job listings containing the term “Software Engineer”

Scenario: User can search for job listings
Given I am a user
	And I login
	And I am on the “Job Listings” page
When I type “Software Engineer” into the search bar
Then I see only job listings containing the term “Software Engineer”

Scenario: Visitor can see pagination
Given I am visitor
	And I am on the “Job Listings” page
	And I type “Software Engineer” into the search bar
	And I see only job listings containing the term “Software Engineer”
When I scroll down to the bottom of the page
Then I see numbers indicating which page I am on

Scenario: User can see pagination
Given I am user
	And I am on the “Job Listings” page
	And I type “Software Engineer” into the search bar
	And I see only job listings containing the term “Software Engineer”
	And I scroll down to the bottom of the page
	And I see numbers indicating which page I am on
When I click on the “2” page
Then I will see more results containing the term “Software Engineer”
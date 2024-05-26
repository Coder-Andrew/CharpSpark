@AndrewE
Feature: CHARP-331
We have profiles, but no way to effectively view them in any organized fashion.
This user story aims to alleviate that issue with a "trending" page that is
organized based on profile score, which this user story tends to implement.
All anyone has to do to view public profiles is click on the trending page
and be taken to a catalog style page that displays the profiles.

Background: 
Given I am on the "Home" page
When I have accepted cookies
Then I can use the site

Scenario: Anyone can view the trending page
Given I am on the "Home" page
When I click on the "Trending" page
Then I will be taken to the "Trending" page

Scenario: After waiting for the profiles to load, anyone can see the profiles
Given I am on the "Trending" page
When I wait long enough
Then I will see a catalog of profiles
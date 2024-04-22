@AndrewE
Feature: CHARP-256

Given I am a visitor
When I click login
Then I will see the login page
And I will see a “Sign in with Google” button



Given I am a visitor
And I am on the “login” page
When I click the “Sign in with Google” button
I will be logged in to the web application with my Google account
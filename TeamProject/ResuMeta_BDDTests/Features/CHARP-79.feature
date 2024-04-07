@AndrewE
Feature: CHARP-79
We want to be able to test the ability for a user to interact with ChatGPT via our built in chat.
The user needs to be logged in in order to see and interact with our chat box.
These tests will ensure that a vistor will NOT be able interact with the chat box and will ensure
	that a user will be able to. 
We will also test the ability of a user to type in a message and recieve a response from ChatGPT.

Background:
    Given the following users exist
	  | UserName               | Email                  | FirstName  | LastName    | Password     |
	  | reynoldsa@mail.com     | reynoldsa@mail.com     | Adrian     | Reynolds    | Password123! |
    And I am a user with the first name 'Adrian'
    And I login
    And I am on the "Home" page
    And I logout

Scenario: A visitor is not able to see the show-hide-chat button
	Given I am a visitor
	When I am on the "Home" page
	Then I should not see the show-hide-chat button

Scenario: A user should be able to see the show-hide-chat button
	Given I am a user with the first name 'Adrian'
		And I login
	When I am on the "Home" page
	Then I should see the show-hide-chat button

Scenario: A user should be able to type a message into the chat box
	Given I am a user
	When I am on the "Home" page
	And I see the chat box
	And I type "Hello" into the chat box
	Then I should see "Hello" in the chat box

Scenario: A user should be able to recieve a response from ChatGPT when pressing enter
	Given I am a user
		And I have typed "Hello" in the chat box
	When I hit enter
	Then I should see a response from ChatGPT

Scenario: A user should be able to recieve a response from ChatGPT when clicking send
	Given I am a user
		And I have typed "Hello" in the chat box
	When I click the send button
	Then I should see a response from ChatGPT
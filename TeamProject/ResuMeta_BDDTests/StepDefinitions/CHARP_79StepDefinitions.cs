using System;
using Reqnroll;
using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP_79StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePageObject _homePage;
        public CHARP_79StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _homePage = new HomePageObject(browserDriver.Current);
        }
      
        [Then("I should not see the show-hide-chat button")]
        public void ThenIShouldNotSeeTheShow_Hide_ChatButton()
        {
            _homePage.ShowHideChatButton().Should().BeNull();
        }

        [Then("I should see the show-hide-chat button")]
        public void ThenIShouldSeeTheShow_Hide_ChatButton()
        {
            _homePage.ShowHideChatButton().Should().NotBeNull();
        }

        [When("I click the show-hide-chat button")]
        public void WhenIClickTheShow_Hide_ChatButton()
        {
            _homePage.RevealChatBox();
        }

        [Then("I should see the chat box")]
        public void ThenIShouldSeeTheChatBox()
        {
            _homePage.ChatBox.Should().NotBeNull();
        }


        [When("I type {string} into the chat box")]
        public void WhenITypeIntoTheChatBox(string hello)
        {
            _homePage.TypeIntoChatBox(hello);
        }

        [Then("I should see {string} in the chat box")]
        public void ThenIShouldSeeInTheChatBox(string hello)
        {
            _homePage.ChatBoxInputText().Should().Be(hello);
        }

        [Given("I have typed {string} in the chat box")]
        public void GivenIHaveTypedInTheChatBox(string hello)
        {
            _homePage.TypeIntoChatBox(hello);
        }

        [When("I hit enter")]
        public void WhenIHitEnter()
        {
            _homePage.HitEnterInChatBox();
        }

        [Then("I should see a response from ChatGPT")]
        public void ThenIShouldSeeAResponseFromChatGPT()
        {
            _homePage.ChatGPTResponses.Should().NotBeEmpty();
        }

        [When("I click the send button")]
        public void WhenIClickTheSendButton()
        {
            _homePage.ClickSendChat();
        }

        [Given("I see the chat box")]
        public void GivenISeeTheChatBox()
        {
            _homePage.ClickShowHideChatButton();
        }


    }
}

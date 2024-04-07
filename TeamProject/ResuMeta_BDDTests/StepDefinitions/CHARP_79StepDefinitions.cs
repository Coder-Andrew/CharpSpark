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
            _homePage.Should().BeNull();
        }

        [Then("I should see the show-hide-chat button")]
        public void ThenIShouldSeeTheShow_Hide_ChatButton()
        {
            _homePage.Should().NotBeNull();
        }

    }
}

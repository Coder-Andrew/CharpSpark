using System;
using Reqnroll;
using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP_331StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TrendingPageObject _trendingPage;
        private readonly HomePageObject _homePage;
        public CHARP_331StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _trendingPage = new TrendingPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
        }

        [When("I have accepted cookies")]
        public void WhenIHaveAcceptedCookies()
        {
            _homePage.ClickAcceptCookiesButton();
        }

        [Then("I can use the site")]
        public void ThenICanUseTheSite()
        {
        }



        [When("I click on the {string} page")]
        public void WhenIClickOnThePage(string trending)
        {
            _homePage.TrendingButton.Click();
        }

        [Then("I will be taken to the {string} page")]
        public void ThenIWillBeTakenToThePage(string trending)
        {
            _homePage.CurrentPageShouldBe(trending);
        }

        [Then("I will see a catalog of profiles")]
        public void ThenIWillSeeACatalogOfProfiles()
        {
            _trendingPage.TrendingProfiles().Should().NotBeEmpty();
        }
    }
}

using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;
using ResuMeta_BDDTests.Shared;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Linq;
using System;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP249StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CreateCoverLetterPageObject _createCoverLetterPage;
        private readonly YourDashboardPageObject _yourDashboardPage;
        private readonly ImproveCoverLetterPageObject _improveCoverLetterPage;
        private readonly ViewCoverLetterPageObject _viewCoverLetterPage;
        public string _viewCoverLetterUrl;
        public string _yourCoverLetterUrl;


        public CHARP249StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _createCoverLetterPage = new CreateCoverLetterPageObject(browserDriver.Current);
            _yourDashboardPage = new YourDashboardPageObject(browserDriver.Current);
            _improveCoverLetterPage = new ImproveCoverLetterPageObject(browserDriver.Current);
            _viewCoverLetterPage = new ViewCoverLetterPageObject(browserDriver.Current);
        }

        [Given ("I have filled out the cover letter form")]
        public void GivenIHaveFilledOutTheCoverLetterForm()
        {
            Common.ResetPaths();
            _createCoverLetterPage.FillOutForm();
        }

        [When ("I click the \"Submit\" button")]
        public void WhenIClickTheSubmitButton()
        {
            _createCoverLetterPage.SubmitForm();
            _viewCoverLetterUrl = _createCoverLetterPage.GetViewCoverLetterUrl();
            Common.Paths["ViewCoverLetter"] = Common.Paths["ViewCoverLetter"] + _viewCoverLetterUrl;
        }

        [Then("I should be redirected to the \"ViewCoverLetter\" page")]
        public void ThenIShouldBeRedirectedToTheViewCoverLetterPage()
        {
            _viewCoverLetterPage.GoTo("ViewCoverLetter");
        }
        [Then("I will be see a button saying \"Improve With AI\"")]
        public void ThenIWillBeSeeButtonSayingImproveWithAI()
        {
            _viewCoverLetterPage.SaveCoverLetter();
            _viewCoverLetterPage.ImproveWithAIButton().Should().BeTrue();
        }

        [When("I click on a specific cover letter")]
        public void WhenIClickOnASpecificCoverLetter()
        {

            _yourDashboardPage.ClickCoverLetter();
        }
        [Then("I should be redirected to \"YourCoverLetter\" page")]
        public void ThenIShouldBeRedirectedToYourCoverLetterPage()
        {
            var coverLetterId = _yourDashboardPage.GetCoverLetterIds().FirstOrDefault();

            var yourCoverLetterUrl = _yourDashboardPage.GetYourCoverLetterUrl(coverLetterId);
            Common.Paths["YourCoverLetter"] = Common.Paths["YourCoverLetter"] + yourCoverLetterUrl;
            _yourDashboardPage.GoTo("YourCoverLetter");
        }

        [When("I click \"Improve With AI\" button")]
        public void WhenIClickImproveWithAIButton()
        {
            _viewCoverLetterPage.ClickImproveWithAI();
        }
        

        [Then("I should be taken to the \"ImproveCoverLetter\" page")]
        public void ThenIShouldBeTakenToTheImproveCoverLetterPage()
        {
            var coverLetterId = _yourDashboardPage.GetCoverLetterIds().FirstOrDefault();
            var yourCoverLetterUrl = _viewCoverLetterPage.GetYourImprovedCoverLetterUrl(coverLetterId);
            Common.Paths["ImproveCoverLetter"] = Common.Paths["ImproveCoverLetter"] + yourCoverLetterUrl;
            _viewCoverLetterPage.GoTo("ImproveCoverLetter");
        }


        [Then("I should see the button that says \"Regenerate\"")]
        public void ThenIShouldSeeTheButtonThatSaysRegenerate()
        {
            _improveCoverLetterPage.RegenerateButton().Should().BeTrue();
        }
        
    }
}
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
    public class CHARP202StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CreateResumePageObject _createResumePage;
        private readonly YourDashboardPageObject _yourDashboardPage;
        private readonly ImproveResumePageObject _improveResumePage;
        private readonly ViewResumePageObject _viewResumePage;
        public string _viewResumeUrl;
        public string _yourResumeUrl;


        public CHARP202StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _createResumePage = new CreateResumePageObject(browserDriver.Current);
            _viewResumePage = new ViewResumePageObject(browserDriver.Current);
            _yourDashboardPage = new YourDashboardPageObject(browserDriver.Current);
            _improveResumePage = new ImproveResumePageObject(browserDriver.Current);
        }

        [Given ("I have filled out the Employment and Projects sections of the form")]
        public void GivenIHaveFilledOutTheEmploymentAndProjectsSectionsOfTheForm()
        {
            Common.ResetPaths();
            // _createResumePage.FillOutEducationForm();
            _createResumePage.ClickNext();
            _createResumePage.FillOutEmploymentForm();
            _createResumePage.ClickNext();
            _createResumePage.ClickNext();
            _createResumePage.FillOutProjectsForm();
        }

        [When ("I click the \"Submit\" button on the form")]
        public void WhenIClickTheSubmitButtonOnTheForm()
        {
            _createResumePage.SubmitForm();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            Common.Paths["ViewResume"] = Common.Paths["ViewResume"] + _viewResumeUrl;
        }

        [Then("I should be redirected to the \"ViewResume\" page")]
        public void ThenIShouldBeRedirectedToTheViewResumePage()
        {
            _viewResumePage.GoTo("ViewResume");
        }

        [Then("I will be see a button that says \"Improve With AI\"")]
        public void ThenIWillBeSeeButtonThatSaysImproveWithAI()
        {
            _viewResumePage.SaveResume();
            _viewResumePage.ImproveWithAIButton().Should().BeTrue();
        }

        [When("I click on a specific resume")]
        public void WhenIClickOnASpecificResume()
        {
            // if(!_viewResumePage.resumeCreated)
            // {
            //     _yourDashboardPage.GoTo("CreateResume");
                
            // }

            _yourDashboardPage.ClickResume();
        }
        [Then("I should be redirected to \"YourResume\" page")]
        public void ThenIShouldBeRedirectedToYourResumePage()
        {
            var resumeId = _yourDashboardPage.GetResumeIds().FirstOrDefault();

            var yourResumeUrl = _yourDashboardPage.GetYourResumeUrl(resumeId);
            Common.Paths["YourResume"] = Common.Paths["YourResume"] + yourResumeUrl;
            _yourDashboardPage.GoTo("YourResume");
        }

        [When("I click the \"Improve With AI\" button")]
        public void WhenIClickTheImproveWithAIButton()
        {
            _viewResumePage.ClickImproveWithAI();
        }
        

        [Then("I should be taken to the \"ImproveResume\" page")]
        public void ThenIShouldBeTakenToTheImproveResumePage()
        {
            var resumeId = _yourDashboardPage.GetResumeIds().FirstOrDefault();

            var yourResumeUrl = _viewResumePage.GetYourImprovedResumeUrl(resumeId);
            Common.Paths["ImproveResume"] = Common.Paths["ImproveResume"] + yourResumeUrl;
            _viewResumePage.GoTo("ImproveResume");
        }


        [Then("I should see the button that says \"Regenerate\"")]
        public void ThenIShouldSeeTheButtonThatSaysRegenerate()
        {
            _improveResumePage.RegenerateButton().Should().BeTrue();
        }
        
    }
}
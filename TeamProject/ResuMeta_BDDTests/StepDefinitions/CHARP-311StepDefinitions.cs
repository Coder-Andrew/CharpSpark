using System;
using System.Threading;
using Reqnroll;
using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP_311StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly JobListingsPageObject _jobListingPage;
        private readonly YourDashboardPageObject _dashboardPage;
        private readonly ImproveResumePageObject _improveResumePage;
        private readonly CreateResumePageObject _createResumePage;
        private readonly ViewResumePageObject _viewResumePage;
        public CHARP_311StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _jobListingPage = new JobListingsPageObject(browserDriver.Current);
            _dashboardPage = new YourDashboardPageObject(browserDriver.Current);
            _improveResumePage = new ImproveResumePageObject(browserDriver.Current);
            _createResumePage = new CreateResumePageObject(browserDriver.Current);
            _viewResumePage = new ViewResumePageObject(browserDriver.Current);
        }
        [Given("the following user has at least one resume")]
        public void GivenTheFollowingUserHasAtLeastOneResume(DataTable dataTable)
        {

            if (!_dashboardPage.ResumeExists())
            {
                _createResumePage.GoTo("CreateResume");
                for (int i = 0; i < 3; i++)
                {
                    _createResumePage.ClickNext();
                }
                _createResumePage.FillOutProjectsForm();
                _createResumePage.SubmitForm();
                _viewResumePage.SaveResume();

            }
        }


        [When("I click on a random job listing")]
        public void WhenIClickOnARandomJobListing()
        {
            Thread.Sleep(5000);
            _jobListingPage.ClickJobListing();
        }

        [Then("I should see a \"Create Cover Letter\" button")]
        public void ThenIShouldSeeAButton()
        {
            _jobListingPage.ButtonShouldExist("create-cover-letter-ai").Should().NotBeNull();
        }

        [When("I click the \"Create Cover Letter\" button")]
        public void WhenIClickOnTheCreateCoverLetterButton()
        {
            Thread.Sleep(5000);
            _jobListingPage.ClickOnTheCreateCoverLetterButton();
            Thread.Sleep(5000);
        }

        [Then("I should see a dropdown selection appear")]
        public void ThenIShouldSeeADropdownSelectionAppear()
        {
            Thread.Sleep(5000);
            _jobListingPage.IsResumeSelectPresent().Should().BeTrue();
        }
    }
}
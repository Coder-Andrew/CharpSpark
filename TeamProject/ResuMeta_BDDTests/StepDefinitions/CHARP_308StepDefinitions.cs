using System;
using System.Threading;
using Reqnroll;
using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP_308StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly JobListingsPageObject _jobListingPage;
        private readonly YourDashboardPageObject _dashboardPage;
        private readonly ImproveResumePageObject _improveResumePage;
        private readonly CreateResumePageObject _createResumePage;
        private readonly ViewResumePageObject _viewResumePage;
        public CHARP_308StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _jobListingPage = new JobListingsPageObject(browserDriver.Current);
            _dashboardPage = new YourDashboardPageObject(browserDriver.Current);
            _improveResumePage = new ImproveResumePageObject(browserDriver.Current);
            _createResumePage = new CreateResumePageObject(browserDriver.Current);
            _viewResumePage = new ViewResumePageObject(browserDriver.Current);
        }
        [Given("the following users has at least one resume")]
        public void GivenTheFollowingUsersHasAtLeastOneResume(DataTable dataTable)
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


        [When("I click on a job listing")]
        public void WhenIClickOnAJobListing()
        {
            Thread.Sleep(5000);
            _jobListingPage.ClickJobListing();
        }

        [Then("I should see the \"Improve with AI\" button")]
        public void ThenIShouldSeeTheButton()
        {
            //Thread.Sleep(5000);
            _jobListingPage.ButtonShouldExist("improve-with-ai").Should().NotBeNull();
        }

        [Given("I click on a job listing")]
        public void GivenIClickOnAJobListing()
        {
            Thread.Sleep(5000);
            _jobListingPage.ClickJobListing();
        }

        [When("I click the \"Improve with AI\" button")]
        public void WhenIClickTheButton()
        {
            _jobListingPage.ButtonShouldExist("improve-with-ai").Click();
        }

        // Hard code the page name, this function destroys other tests as it creates ambiguity for the compiler
        // [Then("I should be redirected to the {string} page")]
        // public void ThenIShouldBeRedirectedToThePage(string yourDashboard)
        // {
        // }

        [Given("I click the \"Improve with AI\" button")]
        public void GivenIClickTheButton()
        {
            _jobListingPage.ButtonShouldExist("improve-with-ai").Click();
            _dashboardPage.FirstResumeDiv.Click(); //This should not be here, but test does not work otherwise
        }
        [When("I click on a resume to improve")]
        public void WhenIClickOnAResumeToImprove()
        {
        }


        [Given("I am redirected to the {string} page")]
        public void GivenIAmRedirectedToThePage(string yourDashBoard)
        {
            _dashboardPage.CurrentPageShouldBe("ImproveResume").Should().BeTrue();
        }



        [Given("I click on a resume to improve")]
        public void GivenIClickOnAResumeToImprove()
        {
            //Thread.Sleep(5000);
            //_dashboardPage.FirstResumeDiv.Click();
        }

        [When("I wait long enough")]
        public void WhenIWaitLongEnough()
        {
            //Thread.Sleep(21000);
        }

        [Then("I should see the {string} text-area")]
        public void ThenIShouldSeeTheText_Area(string p0)
        {
            _improveResumePage.JobDescription.Should().NotBeNull();
        }

        [Then("an improved resume")]
        public void ThenAnImprovedResume()
        {
            _improveResumePage.ImprovedResume.Should().NotBeNull();
        }

    }
}

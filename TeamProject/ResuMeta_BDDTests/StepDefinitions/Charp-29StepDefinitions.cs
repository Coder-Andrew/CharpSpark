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
    public class CHARP29StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly YourDashboardPageObject _yourDashboardPage;
        private readonly ViewResumePageObject _viewResumePage;
        private readonly CreateResumePageObject _createResumePage;
        private readonly CreateCoverLetterPageObject _createCoverLetterPage;
        private readonly ViewCoverLetterPageObject _viewCoverLetterPage;
        private string _yourResumeUrl;
        private string _yourCoverLetterUrl;
        private string _viewResumeUrl;
        private string _viewCoverLetterUrl;

        public CHARP29StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _yourDashboardPage = new YourDashboardPageObject(browserDriver.Current);
            _viewResumePage = new ViewResumePageObject(browserDriver.Current);
            _createResumePage = new CreateResumePageObject(browserDriver.Current);
            _createCoverLetterPage = new CreateCoverLetterPageObject(browserDriver.Current);
            _viewCoverLetterPage= new ViewCoverLetterPageObject(browserDriver.Current);
        }

        [Given("the following users creates at least one resume")]
        public void GivenTheFollowingUsersCreatesAtLeastOneResume(Table table)
        {
            if(_viewResumePage.resumeCreated)
            {
                _viewResumePage.GoTo("Home");
                return;
            }
            _createResumePage.FillOutForm();
            _createResumePage.SubmitForm();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            Common.Paths["ViewResume"] = Common.Paths["ViewResume"] + _viewResumeUrl;
            Thread.Sleep(1000);
            _viewResumePage.SaveResume();
            _viewResumePage.GoTo("YourDashboard");
        }

        [Given("the following users creates at least one cover letter")]
        public void GivenTheFollowingUsersCreatesAtLeastOneCoverLetter(Table table)
        {
            if(_viewCoverLetterPage.coverLetterCreated)
            {
                _viewCoverLetterPage.GoTo("Home");
                return;
            }

            _createCoverLetterPage.GoTo("CreateCoverLetter");
            _createCoverLetterPage.FillOutForm();
            _createCoverLetterPage.SubmitForm();
            _viewCoverLetterUrl = _createCoverLetterPage.GetViewCoverLetterUrl();
            Common.Paths["ViewCoverLetter"] = Common.Paths["ViewCoverLetter"] + _viewCoverLetterUrl;
            Thread.Sleep(1000);
            _viewCoverLetterPage.SaveCoverLetter();
            _viewCoverLetterPage.GoTo("YourDashboard");
        }

        [Then("I should see a Your Resumes section")]
        public void ThenIShouldSeeAYourResumesSection()
        {
            _yourDashboardPage.ResumeSection.Should().NotBeNull();
        }

        [Then("I should see a list of my saved resumes with their titles")]
        public void ThenIShouldSeeAListOfMySavedResumesWithTheirTitles()
        {
            if(!_viewResumePage.resumeCreated)
            {
                _yourDashboardPage.GoTo("CreateResume");
                GivenTheFollowingUsersCreatesAtLeastOneResume(null);
            }
            _yourDashboardPage.SavedResumes.Should().NotBeNull();
            _yourDashboardPage.SavedResumes.Count.Should().BeGreaterThan(0);

            foreach (var resume in _yourDashboardPage.SavedResumes)
            {
                resume.Title.Should().NotBeNullOrEmpty();
            }
        }

        [Then("I should see a Your Cover Letters section")]
        public void ThenIShouldSeeAYourCoverLettersSection()
        {
            _yourDashboardPage.CoverLetterSection.Should().NotBeNull();
        }

        [Then("I should see a list of my saved cover letters with their titles")]
        public void ThenIShouldSeeAListOfMySavedCoverLettersWithTheirTitles()
        {
            if(!_viewCoverLetterPage.coverLetterCreated)
            {
                _yourDashboardPage.GoTo("CreateCoverLetter");
                GivenTheFollowingUsersCreatesAtLeastOneCoverLetter(null);
            }

            _yourDashboardPage.SavedCoverLetters.Should().NotBeNull();
            _yourDashboardPage.SavedCoverLetters.Count.Should().BeGreaterThan(0);

            foreach (var coverLetter in _yourDashboardPage.SavedCoverLetters)
            {
                coverLetter.Title.Should().NotBeNullOrEmpty();
            }
        }

        [When("I click on a resume")]
        public void WhenIClickOnAResume()
        {
            if(!_viewResumePage.resumeCreated)
            {
                _yourDashboardPage.GoTo("CreateResume");
                GivenTheFollowingUsersCreatesAtLeastOneResume(null);
            }

            _yourDashboardPage.ClickResume();
        }

        [Then("I should be rerouted to the \"YourResume\" page")]
        public void ThenIShouldBeRedirectedToTheYourResumePage()
        {
            var resumeId = _yourDashboardPage.GetResumeIds().FirstOrDefault();

            var yourResumeUrl = _yourDashboardPage.GetYourResumeUrl(resumeId);
            Common.Paths["YourResume"] = Common.Paths["YourResume"] + yourResumeUrl;
            _yourDashboardPage.GoTo("YourResume");
        }

        [When("I click on a cover letter")]
        public void WhenIClickOnACoverLetter()
        {
            if(!_viewCoverLetterPage.coverLetterCreated)
            {
                _yourDashboardPage.GoTo("CreateCoverLetter");
                GivenTheFollowingUsersCreatesAtLeastOneCoverLetter(null);
            }
            
            _yourDashboardPage.ClickCoverLetter();
        }

        [Then("I should be reroute to the \"YourCoverLetter\" page")]
        public void ThenIShouldBeRedirectedToTheYourCoverLetterPage()
        {
            var coverLetterId = _yourDashboardPage.GetCoverLetterIds().FirstOrDefault();

            var yourCoverLetterUrl = _yourDashboardPage.GetYourCoverLetterUrl(coverLetterId);
            Common.Paths["YourCoverLetter"] = Common.Paths["YourCoverLetter"] + yourCoverLetterUrl;
            _yourDashboardPage.GoTo("YourCoverLetter");
        }
    }
}
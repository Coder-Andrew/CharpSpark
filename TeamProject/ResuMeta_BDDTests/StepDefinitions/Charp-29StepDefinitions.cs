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
        private string _yourResumeUrl;
        private string _yourCoverLetterUrl;

        public CHARP29StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _yourDashboardPage = new YourDashboardPageObject(browserDriver.Current);
        }


        [Then("I should see a Your Resumes section")]
        public void ThenIShouldSeeAYourResumesSection()
        {
            _yourDashboardPage.ResumeSection.Should().NotBeNull();
        }

        [Then("I should see a list of my saved resumes with their titles")]
        public void ThenIShouldSeeAListOfMySavedResumesWithTheirTitles()
        {
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
            _yourDashboardPage.ClickResume();
        }

        [Then("I should be redirected to the \"YourResume\" page")]
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
            _yourDashboardPage.ClickCoverLetter();
        }

        [Then("I should be redirected to the \"YourCoverLetter\" page")]
        public void ThenIShouldBeRedirectedToTheYourCoverLetterPage()
        {
            var coverLetterId = _yourDashboardPage.GetCoverLetterIds().FirstOrDefault();

            var yourCoverLetterUrl = _yourDashboardPage.GetYourCoverLetterUrl(coverLetterId);
            Common.Paths["YourCoverLetter"] = Common.Paths["YourCoverLetter"] + yourCoverLetterUrl;
            _yourDashboardPage.GoTo("YourCoverLetter");
        }
    }
}
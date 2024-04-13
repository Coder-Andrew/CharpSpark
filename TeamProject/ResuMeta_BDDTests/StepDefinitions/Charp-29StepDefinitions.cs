using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;
using ResuMeta_BDDTests.Shared;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
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
            _yourDashboardPage.YourResumesSection.Should().NotBeNull();
        }

        [And("I should see a list of my saved resumes with their titles")]
        public void AndIShouldSeeAListOfMySavedResumesWithTheirTitles()
        {
            _yourDashboardPage.SavedResumes.Should().NotBeNull();
            _yourDashboardPage.SavedResumes.Count.Should().BeGreaterThan(0);
        }

        [Then("I should see a Your Cover Letters section")]
        {
            _yourDashboardPage.YourCoverLettersSection.Should().NotBeNull();
        }

        [And("I should see a list of my saved cover letters with their titles")]
        public void AndIShouldSeeAListOfMySavedCoverLettersWithTheirTitles()
        {
            _yourDashboardPage.SavedCoverLetters.Should().NotBeNull();
            _yourDashboardPage.SavedCoverLetters.Count.Should().BeGreaterThan(0);
        }

        [When("When I click on a resume")]
        public void WhenIClickOnAResume()
        {
            _yourDashboardPage.ClickResume();
        }

        [Then("Then I should be redirected to the \"YourResume\" page")]
        public void ThenIShouldBeRedirectedToTheYourResumePage()
        {
            _yourDashboardPage.GoTo("YourResume");
        }

        [When("When I click on a cover letter")]
        public void WhenIClickOnACoverLetter()
        {
            _yourDashboardPage.ClickCoverLetter();
        }

        [Then("Then I should be redirected to the \"YourCoverLetter\" page")]
        public void ThenIShouldBeRedirectedToTheYourCoverLetterPage()
        {
            _yourDashboardPage.GoTo("YourCoverLetter");
        }
    }
}
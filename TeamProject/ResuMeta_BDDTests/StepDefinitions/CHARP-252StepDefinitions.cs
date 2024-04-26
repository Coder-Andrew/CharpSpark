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
    public class CHARP252StepDefinitions
    {        
        private readonly ScenarioContext _scenarioContext;
        private readonly JobListingsPageObject _jobListingsPage;
        private readonly ApplicationTrackerPageObject _appTrackPage;
        public string jobTitle;
        public string companyName;
        public string jobListingUrl;
        public string appliedDate;



        public CHARP252StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _jobListingsPage = new JobListingsPageObject(browserDriver.Current);
            _appTrackPage = new ApplicationTrackerPageObject(browserDriver.Current);
        }

        [Given("I have clicked on a Job Listing"), When("I click on a Job Listing")]
        public void GivenIHaveClickedOnAJobListing()
        {
            Thread.Sleep(3000);

            _jobListingsPage.ClickJobListing();
            var listingInfo = _jobListingsPage.GetListingInfo();
            jobTitle = listingInfo[0];
            companyName = listingInfo[1];
            jobListingUrl = listingInfo[2];
            appliedDate = listingInfo[3];
        }

        [Then("I will be able to see a message box asking me if I have sent an application to the job listing I clicked on")]
        public void ThenIWillSeeTheMessageBox()
        {
            IWebElement messageBox = _jobListingsPage.GetMessageBox();
            messageBox.Should().NotBeNull();
            messageBox.Displayed.Should().BeTrue();
            var messageBoxTitle = messageBox.FindElement(By.Id("modal-label")).Text;
            messageBoxTitle.Should().Contain("Did You Apply For This Job?");
        }

        [When("I click on the {string} button in the message box"), Given("I click on the {string} button in the message box")]
        public void WhenIClickOnTheButtonInTheMessageBox(string option)
        {
            if (option == "No")
            {
                _jobListingsPage.ClickNoButton();
            }
            else
            {
                _jobListingsPage.ClickYesButton();
            }
        }

        [Then("I will see the message box disappears")]
        public void ThenIWillSeeTheMessageBoxDisappears()
        {
            IWebElement messageBox = _jobListingsPage.GetMessageBox();
            messageBox.Displayed.Should().BeFalse();
        }

        [Then("I will remain on the JobListings page")]
        public void ThenIWillRemainOnThePage()
        {
            _jobListingsPage.GetCurrentUrl().Should().Contain("JobListing");
        }

        [Then("I will be redirected to the ApplicationTracker page")]
        public void ThenIWillBeRedirectedToThePage()
        {
            _jobListingsPage.GetCurrentUrl().Should().Contain("ApplicationTracker");
        }

        [When("I am redirected to the ApplicationTracker page")]
        public void WhenIRedirectedToThePage()
        {
            _jobListingsPage.GetCurrentUrl().Should().Contain("ApplicationTracker");
        }

        [Then("I will see a new Application form partially filled out with some of the information from the job listing I clicked on")]
        public void ThenIWillSeeANewApplicationFormPartiallyFilledOutWithSomeOfTheInformationFromTheJobListingIClickedOn()
        {
            var jobInfo = _appTrackPage.GetInfo();

            jobInfo[0].GetAttribute("value").Should().NotBeNullOrEmpty();
            jobInfo[1].GetAttribute("value").Should().NotBeNullOrEmpty();
            jobInfo[2].GetAttribute("value").Should().NotBeNullOrEmpty();
            jobInfo[3].GetAttribute("value").Should().NotBeNullOrEmpty();
            jobInfo[0].GetAttribute("value").Should().Contain(jobTitle);
            jobInfo[1].GetAttribute("value").Should().Contain(companyName);
            jobInfo[2].GetAttribute("value").Should().Contain(jobListingUrl);
            jobInfo[3].GetAttribute("value").Should().Contain(appliedDate);
        }

    }
}
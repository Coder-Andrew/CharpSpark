using System;
using System.Threading;
using Castle.Core.Logging;
using Reqnroll;
using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;
using Microsoft.Extensions.Logging;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP139StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ApplicationTrackerPageObject _applicationTrackerPage;
        public CHARP139StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _applicationTrackerPage = new ApplicationTrackerPageObject(browserDriver.Current);
        }

        [Then("I will be able to see a table with one entry that tells me I have no application trackers")]
        public void ThenIWillBeAbleToSeeATableWithOneEntryThatTellsMeIHaveNoApplicationTrackers()
        {
            _applicationTrackerPage.CheckIfTableHasOneEntry();
        }
        [When("I submit my information in the Application Tracker form")]
        public void WhenISubmitMyInformationInTheApplicationTrackerForm()
        {
            _applicationTrackerPage.FillOutForm();

            _applicationTrackerPage.SubmitForm();
        }

        [Then("I will be able to see a table row containing my ApplicationTracker page form response")]
        public void ThenIWillBeAbleToSeeATableRowContainingMyApplicationTrackerPageFormResponse()
        {
            _applicationTrackerPage.IsJobApplicationAddedToTable().Should().BeTrue();
        }

        [When("I filter my applications by application deadline ascending")]
        public void WhenIFilterMyApplicationsByApplicationDeadlineAscending()
        {
            _applicationTrackerPage.FillOutForm();
            _applicationTrackerPage.FilterByApplicationDeadlineAscending();
        }

        [Then("I will be able to see the jobs in my table in ascending order of application deadline")]
        public void ThenIWillBeAbleToSeeTheJobsInMyTableInAscendingOrderOfApplicationDeadline()
        {
            _applicationTrackerPage.AreJobsSorted().Should().BeTrue();
        }

        [When("I submit my information in the ApplicationTracker page form")]
        public void WhenISubmitMyInformationInTheApplicationTrackerPageForm()
        {
            _applicationTrackerPage.FillOutForm();
            _applicationTrackerPage.SubmitForm();
        }

        [When("I click on the trash can icon next to an entry on my ApplicationTracker table")]
        public void WhenIClickOnTheTrashCanIconNextToAnEntryOnMyApplicationTrackerTable()
        {
            _applicationTrackerPage.FillOutFormForDeleteButoon();
            _applicationTrackerPage.SubmitForm();
             _applicationTrackerPage.DeleteJobApplication();
        }

        [Then("I will not be able to see that entry in my ApplicationTracker table")]
        public void ThenIWillNotBeAbleToSeeThatEntryInMyApplicationTrackerTable()
        {
            _applicationTrackerPage.IsJobApplicationDeleted().Should().BeTrue();
        }
    }
}
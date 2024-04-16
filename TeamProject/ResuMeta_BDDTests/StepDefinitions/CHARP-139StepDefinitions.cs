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
        private readonly ILogger<ApplicationTrackerPageObject> _logger;
        public CHARP139StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver, ILogger<ApplicationTrackerPageObject> logger)
        {
            _logger = logger;
            _scenarioContext = scenarioContext;
            _applicationTrackerPage = new ApplicationTrackerPageObject(browserDriver.Current, _logger);
        }

        [Then(" Then I will be able to see a table with one entries")]
        public void ThenIWillBeAbleToSeeATableWithOneEntry()
        {
            _logger.LogInformation("I am working :)");
            _applicationTrackerPage.CheckIfTableHasOneEntry();
        }
        [When("I submit my information in the Application Tracker form")]
        public void WhenISubmitMyInformationInTheApplicationTrackerForm()
        {
            _applicationTrackerPage.FillOutForm();

            _applicationTrackerPage.SubmitForm();
        }

        [Then("Then the new job application should be added to my application tracker table")]
        public void ThenTheNewJobApplicationShouldBeAddedToMyApplicationTrackerTable()
        {
            _applicationTrackerPage.IsJobApplicationAddedToTable().Should().BeTrue();
        }

        // [When("When I filter my applications by application deadline ascending")]
        // public void WhenIFilterMyApplicationsByApplicationDeadlineAscending()
        // {
        //     _applicationTrackerPage.SortBy();
        // }

        // [Then("Then I will be able to see the jobs in my table in ascending order of application deadline")]
        // public void ThenIWillBeAbleToSeeTheJobsInMyTableInAscendingOrderOfApplicationDeadline()
        // {
        //     _applicationTrackerPage.AreJobsSorted().Should().BeTrue();
        // }

    }
}
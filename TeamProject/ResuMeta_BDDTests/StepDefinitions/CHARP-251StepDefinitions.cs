using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP_251StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly JobListingsPageObject _jobListingPage;
        public CHARP_251StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _jobListingPage = new JobListingsPageObject(browserDriver.Current);
        }

        [Then("I should see a search bar that prompts me to search for jobs")]
        public void ThenIShouldSeeASearchBarThatPromptsMeToSearchForJobs()
        {
            _jobListingPage.RemoteJobSearchInput.Should().NotBeNull();
        }
        [When("I type {string} into the search bar")]
        public void WhenITypeIntoTheSearchBar(string software)
        {
            _jobListingPage.TypeIntoSearchBar(software);
        }

        [Then("I see only job listings containing the term {string}")]
        public void ThenISeeOnlyJobListingsContainingTheTerm(string software)
        {
            foreach (string listing in _jobListingPage.SearchJobsByTerm(software))
            {
                listing.Should().Contain(software);
            };
        }



    }
}

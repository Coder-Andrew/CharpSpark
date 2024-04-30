using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ResuMeta_BDDTests.Shared;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Hangfire.Common;

namespace ResuMeta_BDDTests.PageObjects
{
    public class JobListingsPageObject : PageObject
    {
        public IWebElement RemoteJobSearchInput => _webDriver.FindElement(By.Id("cached-job-title"));
        public IWebElement SearchRemoteJobsButton => _webDriver.FindElement(By.Id("search-cached-job-listings"));
        public JobListingsPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "JobListings";
        }

        public void ClickJobListing()
        {
            string originalWindow = _webDriver.CurrentWindowHandle;
            var jobListing = _webDriver.FindElements(By.ClassName("card-link")).First();
            jobListing.Click();
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(wd => _webDriver.WindowHandles.Count > 1);
            _webDriver.SwitchTo().Window(originalWindow);
            Thread.Sleep(1000);
        }

        public List<string> GetListingInfo()
        {
            var jobTitle = _webDriver.FindElement(By.ClassName("job-listing-title")).Text;
            var companyName = _webDriver.FindElement(By.Id("company")).GetAttribute("value");
            var jobListingUrl = _webDriver.FindElement(By.Id("job-link")).GetAttribute("value");
            var appliedDate = _webDriver.FindElement(By.Id("applied-date")).GetAttribute("value");
            return new List<string> { jobTitle, companyName, jobListingUrl, appliedDate };
        }

        public IWebElement GetMessageBox()
        {
            return _webDriver.FindElement(By.Id("modal"));
        }

        public void ClickNoButton()
        {
            var noButton = _webDriver.FindElement(By.Id("close-modal2"));
            noButton.Click();
        }

        public void ClickYesButton()
        {
            var yesButton = _webDriver.FindElement(By.Id("yes-btn"));
            yesButton.Click();
            Thread.Sleep(3000);
        }

        public string GetCurrentUrl()
        {
            return _webDriver.Url;
        }

        public void TypeIntoSearchBar(string searchTerm)
        {
            Thread.Sleep(5000);
            RemoteJobSearchInput.SendKeys(searchTerm);
            SearchRemoteJobsButton.Click();
            Thread.Sleep(5000);
        }
        public List<string> SearchJobsByTerm(string searchTerm)
        {
            List<string> results = new();
            TypeIntoSearchBar(searchTerm);
            var jobListings = _webDriver.FindElements(By.ClassName("card-title"));
            foreach (var jobListing in jobListings)
            {
                results.Add(jobListing.Text);
            }

            return results;
        }
    }
}
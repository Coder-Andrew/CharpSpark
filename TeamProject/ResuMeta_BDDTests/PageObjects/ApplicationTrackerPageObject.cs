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

namespace ResuMeta_BDDTests.PageObjects
{
    public class ApplicationTrackerPageObject : PageObject
    {
        public ApplicationTrackerPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "ApplicationTracker";
        }


        public bool CheckIfTableHasOneEntry()
        {
            IWebElement table = _webDriver.FindElement(By.Id("app-table"));
            IReadOnlyCollection<IWebElement> rows = table.FindElements(By.TagName("tr"));

            if (rows.Count == 2)
            {
                return true;
            }

            return false;
        }

        public void SubmitForm()
        {
            var submitElement = _webDriver.FindElement(By.Id("add-application-button"));
            Thread.Sleep(500);
            submitElement.Click();
            Thread.Sleep(2000);
        }

        public void FillOutForm()
        {
            _webDriver.Manage().Window.Maximize();
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            _webDriver.FindElement(By.Id("jobTitle")).Click();
            _webDriver.FindElement(By.Id("jobTitle")).SendKeys("Student");
            _webDriver.FindElement(By.Id("companyName")).Click();
            _webDriver.FindElement(By.Id("companyName")).SendKeys("Western Oregon University");
            _webDriver.FindElement(By.Id("jobListingUrl")).Click();
            _webDriver.FindElement(By.Id("jobListingUrl")).SendKeys("wou.edu");
            _webDriver.FindElement(By.Id("appliedDate")).Click();
            _webDriver.FindElement(By.Id("appliedDate")).Click();
            _webDriver.FindElement(By.Id("appliedDate")).Click();
            _webDriver.FindElement(By.Id("appliedDate")).SendKeys("2020-04-02");
            _webDriver.FindElement(By.Id("applicationDeadline")).Click();
            _webDriver.FindElement(By.Id("applicationDeadline")).SendKeys("2024-06-16");
            _webDriver.FindElement(By.Id("status")).Click();
            _webDriver.FindElement(By.Id("status")).SendKeys("Waiting to hear back");
            _webDriver.FindElement(By.Id("notes")).Click();
            _webDriver.FindElement(By.Id("notes")).SendKeys("I want this job!");
        }

        public void FillOutFormForDeleteButoon()
        {
            _webDriver.Manage().Window.Maximize();
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            _webDriver.FindElement(By.Id("jobTitle")).Click();
            _webDriver.FindElement(By.Id("jobTitle")).SendKeys("Professor");
            _webDriver.FindElement(By.Id("companyName")).Click();
            _webDriver.FindElement(By.Id("companyName")).SendKeys("Western Oregon University");
            _webDriver.FindElement(By.Id("jobListingUrl")).Click();
            _webDriver.FindElement(By.Id("jobListingUrl")).SendKeys("wou.edu");
            _webDriver.FindElement(By.Id("appliedDate")).Click();
            _webDriver.FindElement(By.Id("appliedDate")).Click();
            _webDriver.FindElement(By.Id("appliedDate")).Click();
            _webDriver.FindElement(By.Id("appliedDate")).SendKeys("2024-04-02");
            _webDriver.FindElement(By.Id("applicationDeadline")).Click();
            _webDriver.FindElement(By.Id("applicationDeadline")).SendKeys("2024-05-15");
            _webDriver.FindElement(By.Id("status")).Click();
            _webDriver.FindElement(By.Id("status")).SendKeys("Waiting to hear back");
            _webDriver.FindElement(By.Id("notes")).Click();
            _webDriver.FindElement(By.Id("notes")).SendKeys("I want this job!");
        }


        public bool IsJobApplicationAddedToTable()
        {
            Thread.Sleep(1000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            string jobTitle = "Student";
            string companyName = "Western Oregon University";
            string jobListingUrl = "wou.edu";
            string appliedDate = "2020-04-02";
            string applicationDeadline = "2024-06-16";
            string status = "Waiting to hear back";
            string notes = "I want this job!";

            IWebElement table = _webDriver.FindElement(By.Id("app-table"));
            IReadOnlyCollection<IWebElement> rows = table.FindElements(By.TagName("tr"));

            foreach (IWebElement row in rows)
            {
                IReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                string urlCell = "";
                foreach (IWebElement cell in cells)
                {
                    if (cell.FindElements(By.TagName("a")).Count > 0)
                    {
                        urlCell = cell.FindElement(By.TagName("a")).GetAttribute("href");
                    }
                }
                List<string> cellTexts = cells.Select(cell => cell.Text).ToList();
                if (cellTexts.Count >= 7 &&
                    cellTexts[0] == jobTitle &&
                    cellTexts[1] == companyName &&
                    urlCell.Contains(jobListingUrl) &&
                    cellTexts[3] == appliedDate &&
                    cellTexts[4] == applicationDeadline &&
                    cellTexts[5] == status &&
                    cellTexts[6] == notes)
                {
                    return true;
                }
            }

            return false;
        }

        public void FilterByApplicationDeadlineAscending()
        {
            Thread.Sleep(1000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            IWebElement sortByDropdown = _webDriver.FindElement(By.Id("sort-option"));
            SelectElement sortBySelect = new SelectElement(sortByDropdown);
            sortBySelect.SelectByValue("application-deadline");
            IWebElement sortOrderDropdown = _webDriver.FindElement(By.Id("sort-order"));
            SelectElement sortOrderSelect = new SelectElement(sortOrderDropdown);
            sortOrderSelect.SelectByValue("ascending");
            IWebElement applyFiltersButton = _webDriver.FindElement(By.Id("apply-filters"));
            js.ExecuteScript("arguments[0].scrollIntoView();", applyFiltersButton);
            Thread.Sleep(500);
            applyFiltersButton.Click();
            Thread.Sleep(1000);
        }

        public bool AreJobsSorted()
        {
            Thread.Sleep(1000);
            IWebElement table = _webDriver.FindElement(By.Id("app-table"));
            IReadOnlyCollection<IWebElement> rows = table.FindElements(By.TagName("tr"));
            List<string> applicationDeadlines = new List<string>();

            foreach (IWebElement row in rows)
            {
                IReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                if (cells.Count >= 5)
                {
                    var cell = cells.ElementAt(4);
                    applicationDeadlines.Add(cell.Text);
                }
            }

            List<string> sortedApplicationDeadlines = new List<string>(applicationDeadlines);
            sortedApplicationDeadlines.Sort();

            return applicationDeadlines.SequenceEqual(sortedApplicationDeadlines);
        }


        public void DeleteJobApplication()
        {

            Thread.Sleep(1000);
            IWebElement table = _webDriver.FindElement(By.Id("app-table"));
            IReadOnlyCollection<IWebElement> rows;

            while (true)
            {
                rows = table.FindElements(By.TagName("tr"));
                if (rows.Count == 2)
                {
                    if (table.Text.Contains("No application trackers to display"))
                    {
                        break;
                    }
                }

                var deleteButton = rows.Skip(1).First().FindElement(By.Id("deleteButton2"));
                deleteButton.Click();
                Thread.Sleep(1000);
            }

        }

        public bool IsJobApplicationDeleted()
        {
            Thread.Sleep(1000);
            IWebElement table = _webDriver.FindElement(By.Id("app-table"));
            IReadOnlyCollection<IWebElement> rows = table.FindElements(By.TagName("tr"));

            if (rows.Count == 2)
            {
                return true;
            }
            return false;
        }

        public IWebElement[] GetInfo()
        {
            Thread.Sleep(1000);
            IWebElement JobTitle = _webDriver.FindElement(By.Id("jobTitle"));
            IWebElement Company = _webDriver.FindElement(By.Id("companyName"));
            IWebElement JobListingUrl = _webDriver.FindElement(By.Id("jobListingUrl"));
            IWebElement AppliedDate = _webDriver.FindElement(By.Id("appliedDate"));

            return new IWebElement[] { JobTitle, Company, JobListingUrl, AppliedDate };
        }
    }
}
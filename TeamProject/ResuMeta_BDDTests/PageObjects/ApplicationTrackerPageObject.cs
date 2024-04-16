using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using ResuMeta_BDDTests.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ResuMeta_BDDTests.PageObjects
{
    public class ApplicationTrackerPageObject : PageObject
    {
        private readonly ILogger<ApplicationTrackerPageObject> _logger;
        public ApplicationTrackerPageObject(IWebDriver webDriver, ILogger<ApplicationTrackerPageObject> logger) : base(webDriver)
        {
            _logger = logger;
            // using a named page (in Common.cs)
            _pageName = "ApplicationTracker";
        }


        public bool CheckIfTableHasOneEntry()
        { _logger.LogInformation("I am working");
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
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            var submitElement = _webDriver.FindElement(By.Id("add-application-button"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", submitElement);
            string currentUrl = _webDriver.Url;
            Thread.Sleep(500);
            submitElement.Click();
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
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


        public bool IsJobApplicationAddedToTable()
        {
            Thread.Sleep(4000);
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
            List<string> cellTexts = cells.Select(cell => cell.Text).ToList();

            if (cellTexts.Count >= 7 && 
                cellTexts[0] == jobTitle && 
                cellTexts[1] == companyName && 
                cellTexts[2] == jobListingUrl &&
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

    }
}
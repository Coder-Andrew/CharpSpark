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
    public class CreateResumePageObject : PageObject
    {
        public CreateResumePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "CreateResume";
        }

        public string GetViewResumeUrl()
        {
            string fullUrl = _webDriver.Url;
            int lastIndex = fullUrl.LastIndexOf("/") + 1;
            string urlId = fullUrl.Substring(lastIndex);
            return urlId;
        }

        public void SubmitForm()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            var submitElement = _webDriver.FindElement(By.Id("submitInfo"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", submitElement);
            string currentUrl = _webDriver.Url;
            Thread.Sleep(500);
            submitElement.Click();
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.Url != currentUrl);
        }

        public void FillOutForm()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            _webDriver.Manage().Window.Maximize();
            Thread.Sleep(500);
            _webDriver.FindElement(By.Id("education-add-btn")).Click();
            {
                var element = _webDriver.FindElement(By.Id("education-add-btn"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element).Perform();
            }
            {
                var element = _webDriver.FindElement(By.TagName("body"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element, 0, 0).Perform();
            }
            _webDriver.FindElement(By.Id("institutionName")).Click();
            _webDriver.FindElement(By.Id("institutionName")).SendKeys("Western Oregon University");
            _webDriver.FindElement(By.Id("educationSummary")).Click();
            _webDriver.FindElement(By.Id("educationSummary")).SendKeys("Senior at Western Oregon University, studying Computer Science as well as Information Systems.");
            _webDriver.FindElement(By.Id("startDate")).Click();
            _webDriver.FindElement(By.CssSelector(".col:nth-child(1)")).Click();
            _webDriver.FindElement(By.Id("startDate")).Click();
            _webDriver.FindElement(By.Id("startDate")).SendKeys("2020-09-02");
            _webDriver.FindElement(By.Id("startDate")).SendKeys("2020-09-25");
            _webDriver.FindElement(By.CssSelector(".col:nth-child(2)")).Click();
            var endDateElement = _webDriver.FindElement(By.Id("endDate"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", endDateElement);
            _webDriver.FindElement(By.Id("endDate")).Click();
            _webDriver.FindElement(By.Id("endDate")).SendKeys("2024-06-15");
            {
                var dropdown = _webDriver.FindElement(By.Id("completed"));
                dropdown.FindElement(By.XPath("//option[. = 'False']")).Click();
            }
            _webDriver.FindElement(By.CssSelector("#completed > option:nth-child(3)")).Click();
            {
                var dropdown = _webDriver.FindElement(By.Id("degreeType"));
                SelectElement selectElement = new SelectElement(dropdown);
                selectElement.SelectByText("Bachelor's Degree");
            }
            _webDriver.FindElement(By.CssSelector("#degreeType > option:nth-child(3)")).Click();
            _webDriver.FindElement(By.Id("major")).Click();
            _webDriver.FindElement(By.Id("major")).SendKeys("Computer Science");
            Thread.Sleep(500);
            _webDriver.FindElement(By.CssSelector(".col:nth-child(2)")).Click();
            _webDriver.FindElement(By.Id("minor")).Click();
            _webDriver.FindElement(By.Id("minor")).SendKeys("Information Systems");
            _webDriver.FindElement(By.CssSelector(".col:nth-child(2)")).Click();
            {
                var element = _webDriver.FindElement(By.Id("tabs"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element).ClickAndHold().Perform();
            }
            {
                var element = _webDriver.FindElement(By.Id("tabs"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element).Perform();
            }
            {
                var element = _webDriver.FindElement(By.Id("tabs"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element).Release().Perform();
            }
            ClickNext();
            _webDriver.FindElement(By.Id("employment-add-btn")).Click();
            _webDriver.FindElement(By.Id("company")).Click();
            _webDriver.FindElement(By.Id("company")).SendKeys("Dutch Bros Coffee");
            _webDriver.FindElement(By.Id("description")).Click();
            _webDriver.FindElement(By.Id("description")).SendKeys("Made coffee for the patrons of West Salem\'s Dutch Bros");
            _webDriver.FindElement(By.Id("location")).Click();
            _webDriver.FindElement(By.Id("location")).SendKeys("West Salem, OR");
            _webDriver.FindElement(By.Id("jobTitle")).Click();
            _webDriver.FindElement(By.Id("jobTitle")).SendKeys("Broista");
            _webDriver.FindElement(By.Id("firstName")).Click();
            _webDriver.FindElement(By.Id("firstName")).SendKeys("Travis");
            _webDriver.FindElement(By.Id("lastName")).Click();
            _webDriver.FindElement(By.Id("lastName")).SendKeys("B.");
            _webDriver.FindElement(By.Id("phoneNumber")).Click();
            _webDriver.FindElement(By.Id("phoneNumber")).SendKeys("123-456-7890");
            _webDriver.FindElement(By.CssSelector("#employment-box .col:nth-child(2)")).Click();
            {
                var element = _webDriver.FindElement(By.Id("tabs"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element).ClickAndHold().Perform();
            }
            {
                var element = _webDriver.FindElement(By.Id("tabs"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element).Perform();
            }
            {
                var element = _webDriver.FindElement(By.Id("tabs"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element).Release().Perform();
            }
            var startDateElement = _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(10) > #startDate"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", startDateElement);
            Thread.Sleep(500);
            startDateElement.Click();
            startDateElement.SendKeys("2019-08-20");
            var endDateElement1 = _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(12) > #endDate"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", endDateElement1);
            Thread.Sleep(500);
            endDateElement1.Click();
            endDateElement1.SendKeys("2022-01-29");
            _webDriver.FindElement(By.CssSelector("#employment-box .col:nth-child(2)")).Click();
            ClickNext();
            _webDriver.FindElement(By.Id("skills")).Click();
            _webDriver.FindElement(By.Id("skills")).SendKeys("Python");
            Thread.Sleep(1000);
            var python = _webDriver.FindElement(By.LinkText("Python"));
            python.Click();
            _webDriver.FindElement(By.Id("skills")).Click();
            _webDriver.FindElement(By.Id("skills")).SendKeys("Agile");
            Thread.Sleep(1000);
            _webDriver.FindElement(By.LinkText("Agile Software Development")).Click();
            _webDriver.FindElement(By.Id("achievement-add-btn")).Click();
            _webDriver.FindElement(By.Id("ach-title")).Click();
            _webDriver.FindElement(By.Id("ach-title")).SendKeys("Honor Roll");
            _webDriver.FindElement(By.Id("ach-summary")).Click();
            _webDriver.FindElement(By.Id("ach-summary")).SendKeys("Achieved ");
            _webDriver.FindElement(By.Id("ach-summary")).SendKeys("Achieved Academic Honor Roll for my time studying at Western Oregon University");
            ClickNext();
            _webDriver.FindElement(By.Id("project-add-btn")).Click();
            {
                var element = _webDriver.FindElement(By.Id("project-add-btn"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element).Perform();
            }
            {
                var element = _webDriver.FindElement(By.TagName("body"));
                Actions builder = new Actions(_webDriver);
                builder.MoveToElement(element, 0, 0).Perform();
            }
            _webDriver.FindElement(By.Id("project-name")).Click();
            _webDriver.FindElement(By.Id("project-name")).SendKeys("resuMeta");
            _webDriver.FindElement(By.Id("project-link")).Click();
            _webDriver.FindElement(By.Id("project-link")).SendKeys("https://resumeta.azurewebsites.net");
            _webDriver.FindElement(By.Id("project-summary")).Click();
            _webDriver.FindElement(By.Id("project-summary")).SendKeys("Senior Sequence Project created with team members of CharpSpark");
            _webDriver.FindElement(By.CssSelector(".form-btns")).Click();
            _webDriver.FindElement(By.CssSelector(".fa-arrow-right")).Click();
        }

        public void ClickNext()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            var nextButtonElement = _webDriver.FindElement(By.Id("next"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", nextButtonElement);
            Thread.Sleep(500);
            nextButtonElement.Click();
            Thread.Sleep(500);
        }
    }
}
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
            //_webDriver.Manage().Window.Size = new System.Drawing.Size(5120, 2880);

            _webDriver.Manage().Window.Maximize();
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            _webDriver.FindElement(By.Id("education-add-btn")).Click();
            _webDriver.FindElement(By.Id("institutionName")).Click();
            _webDriver.FindElement(By.Id("institutionName")).SendKeys("Western Oregon University");
            _webDriver.FindElement(By.Id("educationSummary")).Click();
            _webDriver.FindElement(By.Id("educationSummary")).SendKeys("Senior at Western Oregon University, expected to graduate June 2024");
            _webDriver.FindElement(By.Id("startDate")).Click();
            _webDriver.FindElement(By.Id("startDate")).Click();
            _webDriver.FindElement(By.Id("startDate")).Click();
            _webDriver.FindElement(By.Id("startDate")).SendKeys("2020-09-02");
            _webDriver.FindElement(By.Id("startDate")).SendKeys("2020-09-24");
            var endDateElement = _webDriver.FindElement(By.Id("endDate"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", endDateElement);
            _webDriver.FindElement(By.Id("endDate")).Click();
            _webDriver.FindElement(By.Id("endDate")).SendKeys("2024-06-16");
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
            _webDriver.FindElement(By.Id("minor")).Click();
            _webDriver.FindElement(By.Id("minor")).SendKeys("Information Systems");
            var nextElement = _webDriver.FindElement(By.CssSelector(".col:nth-child(2)"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", nextElement);
            _webDriver.FindElement(By.CssSelector(".col:nth-child(2)")).Click();
            var employmentElement = _webDriver.FindElement(By.Id("employment-add-btn"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", employmentElement);
            _webDriver.FindElement(By.Id("employment-add-btn")).Click();
            {
                var element = _webDriver.FindElement(By.Id("employment-add-btn"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                Actions builder = new Actions(_webDriver);
                //builder.MoveToElement(element).Perform();
            }
            {
                var element = _webDriver.FindElement(By.TagName("body"));
                Actions builder = new Actions(_webDriver);
                //builder.MoveToElement(element, 0, 0).Perform();
            }
            var companyElement = _webDriver.FindElement(By.Id("company"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", companyElement);
            _webDriver.FindElement(By.Id("company")).Click();
            _webDriver.FindElement(By.Id("company")).SendKeys("Dutch Bros Coffee");
            _webDriver.FindElement(By.Id("description")).Click();
            _webDriver.FindElement(By.Id("description")).SendKeys("Made coffee for the patrons of West Salem\'s Dutch Bros");
            var locationElement = _webDriver.FindElement(By.Id("location"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", locationElement);
            Thread.Sleep(500);
            locationElement.Click();
            _webDriver.FindElement(By.Id("location")).SendKeys("West Salem");
            _webDriver.FindElement(By.Id("jobTitle")).Click();
            _webDriver.FindElement(By.Id("jobTitle")).SendKeys("Broista");
            var employmentStartDateElement = _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(10) > #startDate"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", employmentStartDateElement);
            Thread.Sleep(500);
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(10) > #startDate")).Click();
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(10) > #startDate")).Click();
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(10) > #startDate")).SendKeys("2019-08-02");
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(10) > #startDate")).SendKeys("2019-08-20");
            _webDriver.FindElement(By.CssSelector("#employment-box .col:nth-child(2)")).Click();
            var employmentEndDateElement1 = _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(12) > #endDate"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", employmentEndDateElement1);
            Thread.Sleep(500);
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(12) > #endDate")).Click();
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(12) > #endDate")).Click();
            _webDriver.FindElement(By.CssSelector("#employment-box .col:nth-child(1)")).Click();
            var employmentEndDateElement2 = _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(12) > #endDate"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", employmentEndDateElement2);
            Thread.Sleep(500);
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(12) > #endDate")).Click();
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(12) > #endDate")).SendKeys("2022-01-02");
            _webDriver.FindElement(By.CssSelector(".d-flex:nth-child(12) > #endDate")).SendKeys("2022-01-29");
            _webDriver.FindElement(By.CssSelector("#employment-box .col:nth-child(2)")).Click();
            _webDriver.FindElement(By.Id("firstName")).Click();
            _webDriver.FindElement(By.Id("firstName")).SendKeys("Travis");
            _webDriver.FindElement(By.Id("lastName")).Click();
            _webDriver.FindElement(By.Id("lastName")).SendKeys("B.");
            var phoneNumberElement = _webDriver.FindElement(By.Id("phoneNumber"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", phoneNumberElement);
            Thread.Sleep(500);
            _webDriver.FindElement(By.Id("phoneNumber")).Click();
            _webDriver.FindElement(By.Id("phoneNumber")).SendKeys("123-456-7788");
            _webDriver.FindElement(By.CssSelector("#employment-box .col:nth-child(2)")).Click();
            var achievementElement = _webDriver.FindElement(By.Id("achievement-add-btn"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", achievementElement);
            Thread.Sleep(500);
            _webDriver.FindElement(By.Id("achievement-add-btn")).Click();
            _webDriver.FindElement(By.CssSelector(".achievement-title")).Click();
            _webDriver.FindElement(By.CssSelector(".achievement-title")).SendKeys(Keys.Down);
            _webDriver.FindElement(By.CssSelector(".achievement-title")).SendKeys(Keys.Down);
            _webDriver.FindElement(By.CssSelector(".achievement-title")).SendKeys("Academic Honor Roll");
            _webDriver.FindElement(By.CssSelector("textarea:nth-child(2)")).Click();
            _webDriver.FindElement(By.CssSelector("textarea:nth-child(2)")).Click();
            _webDriver.FindElement(By.CssSelector("textarea:nth-child(2)")).Click();
            _webDriver.FindElement(By.CssSelector("textarea:nth-child(2)")).SendKeys("Received Academic Honor Roll for my time studying at Western Oregon University");
            var skillElement = _webDriver.FindElement(By.Id("skills"));
            js.ExecuteScript(string.Format("window.scrollTo({0},{1})", skillElement.Location.X, skillElement.Location.Y));
            _webDriver.FindElement(By.Id("skills")).Click();
            _webDriver.FindElement(By.Id("skills")).SendKeys("Agile");
            js.ExecuteScript("arguments[0].scrollIntoView(true);", skillElement);
            Thread.Sleep(1000);
            var agile = _webDriver.FindElement(By.LinkText("Agile Software Development"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", agile);
            agile.Click();
            var projectElement = _webDriver.FindElement(By.Id("project-add-btn"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", projectElement);
            Thread.Sleep(500);
            _webDriver.FindElement(By.Id("project-add-btn")).Click();
            js.ExecuteScript("arguments[0].scrollIntoView(true);", projectElement);
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
            
            var projectNameElement = _webDriver.FindElement(By.Id("project-name"));
            js.ExecuteScript(string.Format("window.scrollTo({0},{1})", projectNameElement.Location.X, projectNameElement.Location.Y));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", projectNameElement);
            Thread.Sleep(500);
            projectNameElement.Click();
            projectNameElement.SendKeys("resuMeta");
            var projectLinkElement = _webDriver.FindElement(By.Id("project-link"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", projectLinkElement);
            Thread.Sleep(500);
            _webDriver.FindElement(By.Id("project-link")).Click();
            _webDriver.FindElement(By.Id("project-link")).SendKeys("https://resumeta.azurewebsites.net");
            var projectSummaryElement = _webDriver.FindElement(By.Id("project-summary"));
            _webDriver.FindElement(By.Id("project-summary")).Click();
            _webDriver.FindElement(By.Id("project-summary")).SendKeys("Senior Sequence Project created with team members of CharpSpark");
        }

    }
}
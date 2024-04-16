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
    public class CreateCoverLetterPageObject : PageObject
    {
        public CreateCoverLetterPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "CreateCoverLetter";
        }

        public string GetViewCoverLetterUrl()
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
            _webDriver.Manage().Window.Maximize();
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            _webDriver.FindElement(By.Id("cover-letter-dont-know")).Click();
            _webDriver.FindElement(By.Id("cover-letter-body")).SendKeys("Cover letter description detailing why I am fit for the position");
        }

    }
}
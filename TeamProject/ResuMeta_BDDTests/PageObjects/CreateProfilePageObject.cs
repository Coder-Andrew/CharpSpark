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

namespace ResuMeta_BDDTests.PageObjects
{
    public class CreateProfilePageObject : PageObject
    {
        public CreateProfilePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "CreateProfile";
        }

        public IWebElement DescriptionInput => _webDriver.FindElement(By.Id("description"));
        public IWebElement SubmitButton => _webDriver.FindElement(By.Id("submitInfo"));


        public string GetCurrentUrl()
        {
            Uri uri = new Uri(_webDriver.Url);
            string pathAndQuery = uri.PathAndQuery;
            return pathAndQuery;
        }
        public IWebElement GetResumeInput()
        {
            var resumes = _webDriver.FindElements(By.CssSelector("div[class=\"thumbnail\"]"));
            return resumes.FirstOrDefault();
        }

        public void FillOutProfileForm()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            _webDriver.FindElement(By.Id("description")).Click();
            _webDriver.FindElement(By.Id("description")).SendKeys("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure do");
            var resumes = _webDriver.FindElements(By.CssSelector("div[class=\"thumbnail\"]"));
            resumes.FirstOrDefault().Click();
            var submitButton = _webDriver.FindElement(By.Id("submitInfo"));
            js.ExecuteScript("arguments[0].scrollIntoView();", submitButton);
            Thread.Sleep(500);
            submitButton.Click();
        }

        public string GetCurrentEmail()
        {
            var emailElement = _webDriver.FindElement(By.CssSelector("span[class=\"red-text\"]"));
            return emailElement.Text;
        }
    }
}
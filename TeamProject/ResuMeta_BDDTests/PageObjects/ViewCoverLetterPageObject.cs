using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ResuMeta_BDDTests.Shared;
using System.Collections.ObjectModel;
using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace ResuMeta_BDDTests.PageObjects
{
    public class ViewCoverLetterPageObject : PageObject
    {
        public ViewCoverLetterPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "ViewCoverLetter";
        }

        public bool coverLetterCreated = false;

        public IWebElement SaveCoverLetterBtn => _webDriver.FindElement(By.Id("save-cover-letter"));
        public IWebElement CoverLetterTitle => _webDriver.FindElement(By.Id("cover-letter-title"));
        public void SaveCoverLetter()
        {
            CoverLetterTitle.Click();
            CoverLetterTitle.SendKeys("Test Cover Letter");
            SaveCoverLetterBtn.Click();
            coverLetterCreated = true;
        }
    }
}
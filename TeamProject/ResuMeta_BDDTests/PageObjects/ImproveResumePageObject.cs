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
    public class ImproveResumePageObject : PageObject
    {
        public ImproveResumePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "ImproveResume";
        }
        public IWebElement RegenerateBtn => _webDriver.FindElement(By.Id("regenerate-button"));
        public IWebElement JobDescription => _webDriver.FindElement(By.Id("job-description"));
        public IWebElement ImprovedResume => _webDriver.FindElement(By.Id("editor2"));
        public bool RegenerateButton()
        {
            Thread.Sleep(20000);
            if (RegenerateBtn.Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
  }
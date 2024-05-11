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
    public class YourDashboardPageObject : PageObject
    {
        public YourDashboardPageObject(IWebDriver driver) : base(driver)
        {
            _pageName = "YourDashboard";
        }
        
        public class ResumeVM
        {
            public string? Title { get; set; }
            public string? Url { get; set; }
        }

        public List<ResumeVM> SavedResumes
        {
            get
            {
                var resumeDivs = _webDriver.FindElements(By.CssSelector("#resume-section .thumbnail"));

                var resumes = resumeDivs.Select(div =>
                {
                    var title = div.FindElement(By.CssSelector(".thumbnail-title")).Text;
                    var url = div.GetAttribute("onclick").Split('\'')[1];
                    return new ResumeVM { Title = title, Url = url };
                }).ToList();

                return resumes;
            }
        }

        public class CoverLetterVM
        {
            public string? Title { get; set; }
            public string? Url { get; set; }
        }

        public List<CoverLetterVM> SavedCoverLetters
        {
            get
            {
                var coverLetterDivs = _webDriver.FindElements(By.CssSelector("#cover-letter-section .thumbnail"));

                var coverLetters = coverLetterDivs.Select(div =>
                {
                    var title = div.FindElement(By.CssSelector(".thumbnail-title")).Text;
                    var url = div.GetAttribute("onclick").Split('\'')[1];
                    return new CoverLetterVM { Title = title, Url = url };
                }).ToList();

                return coverLetters;
            }
        }

        public IWebElement CoverLetterSection => _webDriver.FindElement(By.Id("cover-letter-section"));
        public IWebElement ResumeSection => _webDriver.FindElement(By.Id("resume-section"));
        public IWebElement FirstResumeDiv => _webDriver.FindElements(By.CssSelector(".resume-content-ids")).FirstOrDefault();
        public IWebElement FirstCoverLetterDiv => _webDriver.FindElements(By.CssSelector(".cover-letter-content-ids")).FirstOrDefault();

        public List<string> GetResumeIds()
        {
            var resumeContentDivs = _webDriver.FindElements(By.CssSelector(".resume-content-ids"));

            var resumeIds = resumeContentDivs.Select(div =>
            {
                var id = div.GetAttribute("id");
                var resumeId = id.Substring("resume-content-".Length);
                return resumeId;
            }).ToList();

            return resumeIds;
        }
        
        public string GetYourResumeUrl(string resumeId)
        {
            string yourResumeUrl = "/YourResume/" + resumeId;

            return yourResumeUrl;
        }

        public void ClickResume()
        {
            FirstResumeDiv.Click();
        }

        public List<string> GetCoverLetterIds()
        {
            var coverLetterContentDivs = _webDriver.FindElements(By.CssSelector(".cover-letter-content-ids"));

            var coverLetterIds = coverLetterContentDivs.Select(div =>
            {
                var id = div.GetAttribute("id");
                var coverLetterId = id.Substring("cover-letter-content-".Length);
                return coverLetterId;
            }).ToList();

            return coverLetterIds;
        }

        public string GetYourCoverLetterUrl(string coverLetterId)
        {
            string yourCoverLetterUrl = "/YourCoverLetter/" + coverLetterId;

            return yourCoverLetterUrl;
        }


        public void ClickCoverLetter()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", FirstCoverLetterDiv);
            Thread.Sleep(500);
            FirstCoverLetterDiv.Click();
        }

        public bool ResumeExists()
        {
            return _webDriver.FindElements(By.ClassName("thumbnail")).Count > 0;
        }

    }
}
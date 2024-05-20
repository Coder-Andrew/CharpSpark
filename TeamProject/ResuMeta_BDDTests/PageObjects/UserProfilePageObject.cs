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
    public class UserProfilePageObject : PageObject
    {
        public UserProfilePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "UserProfile";
        }
        public IWebElement GetUpVoteCount()
        {
            ReadOnlyCollection<IWebElement> upVoteList = _webDriver.FindElements(By.Id("upvotes-count"));
            if (upVoteList.Count > 0)
            {
                return upVoteList[0];
            }
            else
            {
                return null;
            }
        }

        public IWebElement GetDownVoteCount()
        {
            ReadOnlyCollection<IWebElement> downVoteList = _webDriver.FindElements(By.Id("downvotes-count"));
            if (downVoteList.Count > 0)
            {
                return downVoteList[0];
            }
            else
            {
                return null;
            }
        }

        public void ClickUpVoteBtn()
        {
            var upVoteBtn = _webDriver.FindElement(By.Id("upvotes"));
            upVoteBtn.Click();
        }

        public void ClickDownVoteBtn()
        {
            var downVoteBtn = _webDriver.FindElement(By.Id("downvotes"));
            downVoteBtn.Click();
        }
        
        public IWebElement GetUpVoteBtn()
        {
            return _webDriver.FindElement(By.Id("upvotes"));
        }

        public IWebElement GetDownVoteBtn()
        {
            return _webDriver.FindElement(By.Id("downvotes"));
        }
    }
}
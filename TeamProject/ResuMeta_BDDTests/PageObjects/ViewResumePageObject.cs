using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ResuMeta_BDDTests.Shared;
using System.Collections.ObjectModel;

namespace ResuMeta_BDDTests.PageObjects
{
    public class ViewResumePageObject : PageObject
    {
        public ViewResumePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "ViewResume";
        }

        public IWebElement QuillEditor => _webDriver.FindElement(By.Id("ql-editor"));
        public IWebElement QuillToolBar => _webDriver.FindElement(By.Id("ql-toolbar"));
        public bool GetQuillToolBar()
        {
            if (QuillToolBar.Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Logout()
        {
            IWebElement navbarLogoutButton = _webDriver.FindElement(By.Id("logout"));
            navbarLogoutButton.Click();
        }

    }
}

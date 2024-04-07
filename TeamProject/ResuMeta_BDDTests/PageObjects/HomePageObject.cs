using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ResuMeta_BDDTests.Shared;
using System.Collections.ObjectModel;

namespace ResuMeta_BDDTests.PageObjects
{
    public class HomePageObject : PageObject
    {
        public HomePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Home";
        }
        public IWebElement RegisterButton => _webDriver.FindElement(By.Id("register"));
        public IWebElement LoginButton => _webDriver.FindElement(By.Id("login"));
        public IWebElement NavBarHelloLink => _webDriver.FindElement(By.CssSelector("a[title=\"Manage\"]"));
        public IWebElement ShowHideChatButton()
        {
            try
            {
                return _webDriver.FindElement(By.Id("show-hide-chat"));
            }
            catch
            {
                return null;
            }
        }
        public IWebElement ChatBox()
        {
            try
            {
                return _webDriver.FindElement(By.Id("chatbox"));
            }
            catch
            {
                return null;
            }
        }
        public string NavbarWelcomeText()
        {
            return NavBarHelloLink.Text;
        }
        public void Logout()
        {
            IWebElement navbarLogoutButton = _webDriver.FindElement(By.Id("logout"));
            navbarLogoutButton.Click();
        }

        public bool IsLoggedIn()
        {
            var logoutElements = _webDriver.FindElements(By.Id("logout"));
            return logoutElements.Count > 0;
        }
    }
}

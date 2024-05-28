using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ResuMeta_BDDTests.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

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
        public IWebElement ChatBox => _webDriver.FindElement(By.Id("chat-box"));
        public IWebElement ChatBoxInput => _webDriver.FindElement(By.Id("message-input"));
        public IWebElement ChatSendButton => _webDriver.FindElement(By.Id("send"));
        public ReadOnlyCollection<IWebElement> ChatGPTResponses => _webDriver.FindElements(By.CssSelector(".rounded.p-2.text-white.bg-primary"));
        public IWebElement ProfileButton => _webDriver.FindElement(By.CssSelector("a[href=\"/Profile\"]"));
        public IWebElement TrendingButton => _webDriver.FindElement(By.ClassName("fa-thumbs-up"));
        public void ClickAcceptCookiesButton()
        {
            IWebElement cookiesBtn = _webDriver.FindElement(By.Id("consent-yes"));
            cookiesBtn.Click();
        }


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
        public void ClickShowHideChatButton()
        {         
            ShowHideChatButton().Click();
        }
        public void RevealChatBox()
        {
            IWebElement showChatBtn = _webDriver.FindElement(By.Id("show-hide-chat"));
            showChatBtn.Click();
        }
        public void TypeIntoChatBox(string message)
        {
            //ScrollIntoView(ChatBoxInput);
            ChatBoxInput.SendKeys(message);        
        }
        public string ChatBoxInputText()
        {
            return ChatBoxInput.GetAttribute("value");
        }
        public void HitEnterInChatBox()
        {
            ChatBoxInput.SendKeys("\n");
        }
        public void ClickSendChat()
        {
            ChatSendButton.Click();
        }
        public string NavbarWelcomeText()
        {
            return NavBarHelloLink.Text;
        }
        public void Logout()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            IWebElement navbarLogoutButton = _webDriver.FindElement(By.Id("logout"));
            js.ExecuteScript("arguments[0].scrollIntoView();", navbarLogoutButton);
            Thread.Sleep(1000);
            navbarLogoutButton.Click();
        }

        public bool IsLoggedIn()
        {
            var logoutElements = _webDriver.FindElements(By.Id("logout"));
            return logoutElements.Count > 0;
        }

        public void SearchProfile(string email)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            IWebElement searchInput = _webDriver.FindElement(By.Id("search-profiles"));
            js.ExecuteScript("arguments[0].scrollIntoView();", searchInput);
            Thread.Sleep(1000);
            searchInput.Click();
            searchInput.SendKeys(email);
            Thread.Sleep(3000);
            IWebElement profileCard = _webDriver.FindElements(By.Id("profile-username")).FirstOrDefault();
            profileCard.Click();
        }

        public string GetUserProfileURL()
        {
            string fullUrl = _webDriver.Url;
            int lastIndex = fullUrl.LastIndexOf("/") + 1;
            string urlId = fullUrl.Substring(lastIndex);
            return urlId;
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ResuMeta_BDDTests.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

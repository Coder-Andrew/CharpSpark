using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace ResuMeta_BDDTests.PageObjects
{
    public class RegisterPageObject : PageObject
    {
        public RegisterPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "Register";
        }

        public void Register(string email)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Id("Input_Email")).Click();
            _webDriver.FindElement(By.Id("Input_Email")).SendKeys(email);
            _webDriver.FindElement(By.Id("Input_FirstName")).Click();
            _webDriver.FindElement(By.Id("Input_FirstName")).SendKeys("example");
            _webDriver.FindElement(By.Id("Input_LastName")).Click();
            _webDriver.FindElement(By.Id("Input_LastName")).SendKeys("example");
            _webDriver.FindElement(By.Id("Input_PhoneNumber")).Click();
            _webDriver.FindElement(By.Id("Input_PhoneNumber")).SendKeys("555-555-5555");
            _webDriver.FindElement(By.Id("Input_Password")).Click();
            _webDriver.FindElement(By.Id("Input_Password")).SendKeys("Password123!");
            _webDriver.FindElement(By.Id("Input_ConfirmPassword")).Click();
            _webDriver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("Password123!");
            {
            var dropdown = _webDriver.FindElement(By.Id("Input_SecurityQuestion1"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", dropdown);
            Thread.Sleep(500);
            SelectElement selectElement = new SelectElement(dropdown);
            selectElement.SelectByText("What is your mother's maiden name?");
            }
            _webDriver.FindElement(By.CssSelector("#Input_SecurityQuestion1 > option:nth-child(2)")).Click();
            _webDriver.FindElement(By.Id("Input_SecurityAnswer1")).Click();
            _webDriver.FindElement(By.Id("Input_SecurityAnswer1")).SendKeys("mom");
            _webDriver.FindElement(By.CssSelector(".text-center")).Click();
            {
            var dropdown = _webDriver.FindElement(By.Id("Input_SecurityQuestion2"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", dropdown);
            Thread.Sleep(500);
            SelectElement selectElement = new SelectElement(dropdown);
            selectElement.SelectByText("What is your favorite book?");
            }
            _webDriver.FindElement(By.CssSelector("#Input_SecurityQuestion2 > option:nth-child(2)")).Click();
            _webDriver.FindElement(By.Id("Input_SecurityAnswer2")).Click();
            _webDriver.FindElement(By.Id("Input_SecurityAnswer2")).SendKeys("book");
            {
            var dropdown = _webDriver.FindElement(By.Id("Input_SecurityQuestion3"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", dropdown);
            Thread.Sleep(500);
            SelectElement selectElement = new SelectElement(dropdown);
            selectElement.SelectByText("What is the name of your favorite pet?");
            }
            _webDriver.FindElement(By.CssSelector("#Input_SecurityQuestion3 > option:nth-child(3)")).Click();
            _webDriver.FindElement(By.Id("Input_SecurityAnswer3")).Click();
            _webDriver.FindElement(By.Id("Input_SecurityAnswer3")).SendKeys("pet");
            _webDriver.FindElement(By.CssSelector(".flex-container")).Click();
            var submitBtn = _webDriver.FindElement(By.Id("registerSubmit"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", submitBtn);
            Thread.Sleep(500);
            submitBtn.Click();
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ResuMeta_BDDTests.Shared;
using System.Collections.ObjectModel;
using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace ResuMeta_BDDTests.PageObjects
{
    public class ViewResumePageObject : PageObject
    {
        public ViewResumePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "ViewResume";
        }

        public IWebElement QuillEditor => _webDriver.FindElement(By.CssSelector("div[class=\"ql-editor\"]"));
        public IWebElement QuillToolBar => _webDriver.FindElement(By.CssSelector("div[class=\"ql-toolbar ql-snow\"]"));
        public IWebElement SaveResumeBtn => _webDriver.FindElement(By.Id("save-resume"));
        public IWebElement ResumeTitle => _webDriver.FindElement(By.Id("resume-title"));
        public IWebElement ExportPdfBtn => _webDriver.FindElement(By.Id("export-pdf"));
        public bool GetEditor()
        {
            if (QuillToolBar.Displayed && QuillEditor.Displayed) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TypeIntoEditor()
        {
            QuillEditor.Click();
            //if (CheckOperatingSystem() == "MacOS")
            //{
            //    QuillEditor.SendKeys(Keys.Command + "a");
            //    QuillEditor.SendKeys(Keys.Backspace);
            //}
            //else
            //{
            //    QuillEditor.SendKeys(Keys.Control + "a");
            //    QuillEditor.SendKeys(Keys.Delete);
            //}
            QuillEditor.SendKeys("Hello, World!");
            Thread.Sleep(1000);
        }   


        public string CheckOperatingSystem()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "MacOS";
            }
            else
            {
                return "Windows or Linux";
            }
        }

        public void SaveResume()
        {
            ResumeTitle.Click();
            ResumeTitle.SendKeys("Test Resume");
            SaveResumeBtn.Click();
        }

        public string GetSaveMessage()
        {
            Thread.Sleep(1000);
            var saveStatus = _webDriver.FindElement(By.Id("validation-success"));
            return saveStatus.Text;
        }

        public void ExportPdf()
        {
            ExportPdfBtn.Click();
            Thread.Sleep(3000);
            if (_webDriver.WindowHandles.Count > 1)
            {
                _webDriver.SwitchTo().Window(_webDriver.WindowHandles[1]);
                _webDriver.Close();
                _webDriver.SwitchTo().Window(_webDriver.WindowHandles[0]);
            }
        }
    }
}

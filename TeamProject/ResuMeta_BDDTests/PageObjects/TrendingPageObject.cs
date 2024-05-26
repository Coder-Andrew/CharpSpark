using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResuMeta_BDDTests.PageObjects
{
    public class TrendingPageObject : PageObject
    {
        public TrendingPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "Trending";
        }

        public List<IWebElement> TrendingProfiles()
        {
            Thread.Sleep(3000);
            return _webDriver.FindElements(By.CssSelector(".card")).ToList();
        }
    }
}

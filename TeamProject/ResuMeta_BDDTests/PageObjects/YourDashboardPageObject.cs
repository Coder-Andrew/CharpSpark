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
    public class YourDashboardPageObject : PageObject
    {
        public YourDashboardPageObject(IWebDriver driver) : base(driver)
        {
            _pageName = "YourDashboard";
        }
    }
}
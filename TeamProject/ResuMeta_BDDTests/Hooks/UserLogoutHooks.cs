using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResuMeta_BDDTests.Hooks
{
    [Binding]
    public class UserLogoutHooks
    {
        [AfterScenario("LoggedIn")]
        public static void AfterScenario(BrowserDriver browserDriver)
        {
            // if every page has the navbar then we could put this in the base PageObject class and make it not abstract
            HomePageObject homePage = new HomePageObject(browserDriver.Current);
            homePage.Logout();
        }

    }
}

using NUnit.Framework;
using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;
using System.Threading;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private readonly HomePageObject _homePage;
        private readonly ScenarioContext _scenarioContext;

        public HomeStepDefinitions(ScenarioContext context, BrowserDriver browserDriver) 
        {
            _scenarioContext = context;
            _homePage = new HomePageObject(browserDriver.Current);
        } 

        [Given(@"I am a visitor")]
        public void GivenIAmAVisitor()
        {
            if (_homePage.IsLoggedIn())
            {
                _homePage.Logout();
            }
        }

        [Given(@"I am on the ""([^""]*)"" page"),When(@"I am on the ""([^""]*)"" page")]
        public void WhenIAmOnThePage(string pageName)
        {
            _homePage.GoTo(pageName);
            if (pageName == "Home")
            {
                Thread.Sleep(3000);
            }
        }

        [Then(@"The page title contains ""([^""]*)""")]
        public void ThenThePageTitleContains(string p0)
        {
            _homePage.GetTitle().Should().ContainEquivalentOf(p0, AtLeast.Once());
        }

        [Then(@"The page presents a Register button")]
        public void ThenThePagePresentsARegisterButton()
        {
            Thread.Sleep(3000);
            _homePage.RegisterButton.Should().NotBeNull();
            _homePage.RegisterButton.Displayed.Should().BeTrue();

            // And must it be a button?  Makes it more fragile and increases "friction" in developing the UI, but...
            // Here using normal NUnit constraints
            Assert.That(_homePage.RegisterButton.GetAttribute("class"), Does.Contain("nav-link"));
        }

        [Given(@"I logout"), When(@"I logout")]
        public void GivenILogout()
        {
            _homePage.GoTo();
            Thread.Sleep(3000);
            _homePage.Logout();
        }

    }
}

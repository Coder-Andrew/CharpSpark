using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;
using ResuMeta_BDDTests.Shared;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP310StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CreateProfilePageObject _createProfilePage;
        private readonly YourProfilePageObject _yourProfilePage;
        private readonly UserProfilePageObject _userProfilePage;
        private readonly RegisterPageObject _registerPage;
        private readonly CreateResumePageObject _createResumePage;
        private readonly HomePageObject _homePage;
        private readonly ViewResumePageObject _viewResumePage;
        public string _userProfilePageUrl;
        public string _viewResumeUrl;
        private static Random random = new Random();

        public CHARP310StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _createProfilePage = new CreateProfilePageObject(browserDriver.Current);
            _yourProfilePage = new YourProfilePageObject(browserDriver.Current);
            _userProfilePage = new UserProfilePageObject(browserDriver.Current);
            _registerPage = new RegisterPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _createResumePage = new CreateResumePageObject(browserDriver.Current);
            _viewResumePage = new ViewResumePageObject(browserDriver.Current);
        }

        public string GenerateEmail(int length)
        {
            if (length == 0)
            {
                length = 5;
            }
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray()) + "@mail.com";
        }

        [Given("I am a random user")]
        public void GivenIAmARandomUser()
        {
            var email = GenerateEmail(random.Next(10));
            _registerPage.GoTo();
            _registerPage.Register(email);
        }

        [Then("I should see a tab on the navbar called \"Profile\"")]
        public void ThenIShouldSeeATabOnTheNavbarCalledProfile()
        {
            IWebElement profileTab = _homePage.ProfileButton;
            profileTab.Should().NotBeNull();
            profileTab.Displayed.Should().BeTrue();
        }

        [When ("I click on the \"Profile\" tab of the navbar"), Given("I click on the \"Profile\" tab of the navbar")]
        public void WhenIClickOnTheProfileTabOfTheNavbar()
        {
            _homePage.ProfileButton.Click();
        }

        [Then ("I should see a page where I can create a profile")]
        public void ThenIShouldSeeAPageWhereICanCreateAProfile()
        {
            Thread.Sleep(1000);
            IWebElement descriptionInput = _createProfilePage.DescriptionInput;
            IWebElement submitButton = _createProfilePage.SubmitButton;
            IWebElement resumeInput = _createProfilePage.GetResumeInput();
            descriptionInput.Should().NotBeNull();
            submitButton.Should().NotBeNull();
            resumeInput.Should().NotBeNull();
            descriptionInput.Displayed.Should().BeTrue();
            submitButton.Displayed.Should().BeTrue();
            resumeInput.Displayed.Should().BeTrue();
        }

        [Given ("I have created a resume")]
        public void GivenIHaveCreatedAResume()
        {
            Common.ResetPaths();
            _createResumePage.GoTo();
            _createResumePage.FillOutEducationForm();
            _createResumePage.SubmitForm();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            Common.Paths["ViewResume"] = Common.Paths["ViewResume"] + _viewResumeUrl;
            _viewResumePage.GoTo();
            _viewResumePage.SaveResume();
        }

        [When ("I fill out the form for \"CreateProfile\"")]
        public void WhenIFillOutTheCreateProfileForm()
        {
            _createProfilePage.FillOutProfileForm();
        }

        [Then ("I should be redirected to the \"YourProfile\" page")]
        public void ThenIShouldBeRedirectedToTheYourProfilePage()
        {
            string url = _createProfilePage.GetCurrentUrl();
            Common.PathFor("YourProfile").Should().Contain(url);
        }

        [Given("I have created a public profile")]
        public void GivenIHaveCreatedAPublicProfile()
        {
            _createProfilePage.GoTo();
            _createProfilePage.FillOutProfileForm();
        }

        [Then("I should see the information I used to create my profile displayed on the page")]
        public void ThenIShouldSeeTheInfoIUsedToCreateMyProfile()
        {
            IWebElement description = _yourProfilePage.Description;
            description.Should().NotBeNull();
            description.Text.Should().Contain("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure do");
        }

        [When ("I go to the \"YourProfile\" page")]
        public void WhenIGoToYourProfilePage()
        {
            _yourProfilePage.GoTo();
        }
    }
}
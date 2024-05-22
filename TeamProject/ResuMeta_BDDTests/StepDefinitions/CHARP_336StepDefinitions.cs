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
    public class CHARP336StepDefinitions
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

        public CHARP336StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
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
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray()) + "@mail.com";
        }

        [Then ("I can see the vote count tied to my resume")]
        public void ThenICanSeeTheVoteCountTiedToMyResume()
        {
            IWebElement upVoteCount = _yourProfilePage.GetUpVoteCount();
            IWebElement downVoteCount = _yourProfilePage.GetDownVoteCount();
            upVoteCount.Should().NotBeNull();
            downVoteCount.Should().NotBeNull();
            upVoteCount.Text.Should().Contain("0");
            downVoteCount.Text.Should().Contain("0");
        }

        [When ("I navigate to someone elses profile"), Given ("I navigate to someone elses profile")]
        public void WhenINavigateToSomeoneElsesProfile()
        {
            // create a new resume
            Common.ResetPaths();
            _createResumePage.GoTo();
            _createResumePage.FillOutEducationForm();
            _createResumePage.SubmitForm();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            Common.Paths["ViewResume"] = Common.Paths["ViewResume"] + _viewResumeUrl;
            _viewResumePage.GoTo();
            _viewResumePage.SaveResume();

            // create a new profile
            _createProfilePage.GoTo();
            _createProfilePage.FillOutProfileForm();

            // grab the email
            var email = _createProfilePage.GetCurrentEmail();
            email = email.Substring(0, email.Length - 1);

            var newEmail = GenerateEmail(random.Next(7, 21));
            _registerPage.GoTo();
            _registerPage.Register(newEmail);

            Common.ResetPaths();
            _createResumePage.GoTo();
            _createResumePage.FillOutEducationForm();
            _createResumePage.SubmitForm();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            Common.Paths["ViewResume"] = Common.Paths["ViewResume"] + _viewResumeUrl;
            _viewResumePage.GoTo();
            _viewResumePage.SaveResume();
            
            _createProfilePage.GoTo();
            _createProfilePage.FillOutProfileForm();

            // go to the user profile
            _homePage.GoTo();
            _homePage.SearchProfile(email);
            _userProfilePageUrl = _homePage.GetUserProfileURL();
            Common.Paths["UserProfile"] = Common.Paths["UserProfile"] + _userProfilePageUrl;
            _userProfilePage.GoTo();

        }

        [Then ("I can see I have an option to upvote or downvote their resume")]
        public void ThenICanSeeIHaveAnOptionToUpvoteOrDownvoteTheirResume()
        {
            IWebElement upVoteBtn = _userProfilePage.GetUpVoteBtn();
            IWebElement downVoteBtn = _userProfilePage.GetDownVoteBtn();
            upVoteBtn.Should().NotBeNull();
            downVoteBtn.Should().NotBeNull();
            upVoteBtn.Displayed.Should().BeTrue();
            downVoteBtn.Displayed.Should().BeTrue();
        }

        [When ("I click on \"UpVote\"")]
        public void WhenIClickOnUpVote()
        {
            _userProfilePage.ClickUpVoteBtn();
        }

        [Then ("I can see they have one more upvote")]
        public void ThenIShouldSeeTheUpVoteCountIncrease()
        {
            IWebElement upVoteCount = _userProfilePage.GetUpVoteCount();
            upVoteCount.Text.Should().Contain("1");
        }

        [When ("I click on \"DownVote\"")]
        public void WhenIClickOnDownVote()
        {
            _userProfilePage.ClickDownVoteBtn();
        }

        [Then ("I can see they have one more downvote")]
        public void ThenIShouldSeeTheDownVoteCountIncrease()
        {
            IWebElement downVoteCount = _userProfilePage.GetDownVoteCount();
            downVoteCount.Text.Should().Contain("1");
        }
    }
}
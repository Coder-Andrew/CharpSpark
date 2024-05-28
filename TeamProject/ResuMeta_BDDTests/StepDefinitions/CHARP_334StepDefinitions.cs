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
    public class CHARP334StepDefinitions
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

        public CHARP334StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
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


        [When ("I click on \"Follow\"")]
        public void WhenIClickOnFollow()
        {
            _userProfilePage.ClickFollowBtn();
        }

        [Then ("I can see they have one more follower")]
        public void ThenIShouldSeeTheFollowerCountIncrease()
        {
            IWebElement followerCount = _userProfilePage.GetFollowerCount();
            followerCount.Text.Should().Contain("1");
        }

        [Then("I can see I have an option to follow them")]
        public void ThenICanSeeIHaveAnOptionToFollowThem()
        {
            IWebElement followBtn = _userProfilePage.GetFollowBtn();
            followBtn.Should().NotBeNull();
            followBtn.Displayed.Should().BeTrue();
        }

        [Then("I can see my follower and following count")]
        public void ThenICanSeeMyFollowerAndFollowingCount()
        {
            IWebElement followerCount = _yourProfilePage.GetFollowerCount();
            IWebElement followingCount = _yourProfilePage.GetFollowingCount();
            followerCount.Should().NotBeNull();
            followingCount.Should().NotBeNull();
            followerCount.Displayed.Should().BeTrue();
            followingCount.Displayed.Should().BeTrue();
        }

        [When("I click on \"Followers\"")]
        public void WhenIClickOnFollowers()
        {
            _yourProfilePage.ClickOnFollowers();
        }

        [Then("I can see all the users who follow me")]
        public void ThenICanSeeAllTheUsersWhoFollowMe()
        {
            var followers = _yourProfilePage.GetFollowers();
            followers.Should().NotBeNull();
            followers.Displayed.Should().BeTrue();
            followers.Text.Should().Contain("No followers found"); // no one follows this user, so it displays this message
        }

    }
}
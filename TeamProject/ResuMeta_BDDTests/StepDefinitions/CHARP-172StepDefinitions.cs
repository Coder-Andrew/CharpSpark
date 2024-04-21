using ResuMeta_BDDTests.Drivers;
using ResuMeta_BDDTests.PageObjects;
using ResuMeta_BDDTests.Shared;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP172StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CreateResumePageObject _createResumePage;
        private readonly ViewResumePageObject _viewResumePage;
        public string _viewResumeUrl;

        public CHARP172StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _createResumePage = new CreateResumePageObject(browserDriver.Current);
            _viewResumePage = new ViewResumePageObject(browserDriver.Current);
        }
        [Then("I should be able to see the 5 tabs named \"Education\", \"Employment\", \"Skills & Achievements\", \"Projects\" and \"Personal Summary\"")]
        public void ThenIShouldBeAbleToSeeThe5TabsNamedEducationEmploymentSkillsAchievementsProjectsAndPersonalSummary()
        {
            IWebElement[] tabs = _createResumePage.GetTabs();
            tabs.Should().NotBeNull();
            tabs.Length.Should().Be(5);
            tabs[0].Text.Should().Contain("Education");
            tabs[1].Text.Should().Contain("Employment");
            tabs[2].Text.Should().Contain("Skills & Achievements");
            tabs[3].Text.Should().Contain("Projects");
            tabs[4].Text.Should().Contain("Personal Summary");
        }

        [Then("I should see that the first tab opened should be \"Education\"")]
        public void ThenIShouldSeeThatTheFirstTabOpenedShouldBeEducation()
        {
            _createResumePage.GetActiveTab().Text.Should().Contain("Education");
            var panel = _createResumePage.GetActiveTabPanel();
            panel.Should().NotBeNull();
            panel.FindElement(By.TagName("h4")).Text.Should().Contain("Education");
            panel.FindElement(By.Id("education-add-btn")).Should().NotBeNull();
            panel.FindElement(By.Id("education-clear-btn")).Should().NotBeNull();
            panel.FindElement(By.Id("open-modal")).Should().NotBeNull();

        }

        [When(@"I click on the ""([^""]*)"" tab")]
        public void WhenIClickOnTheTab(string tabName)
        {
            _createResumePage.ClickTab(tabName);
        }

        [Then(@"I should see the ""([^""]*)"" form section displayed")]
        public void ThenIShouldSeeTheFormSectionDisplayed(string formSection)
        {
            _createResumePage.GetActiveTab().Text.Should().Contain(formSection);
            var panel = _createResumePage.GetActiveTabPanel();
            panel.Should().NotBeNull();
            panel.FindElement(By.TagName("h4")).Text.Should().Contain(formSection);
            if (formSection == "Skills & Achievements")
            {
                panel.FindElement(By.Id("skills")).Should().NotBeNull();
                panel.FindElement(By.Id("achievement-add-btn")).Should().NotBeNull();
                panel.FindElement(By.Id("achievement-clear-btn")).Should().NotBeNull();
                panel.FindElement(By.Id("open-modal")).Should().NotBeNull();
                return;
            }
            else if (formSection == "Projects")
            {
                panel.FindElement(By.Id("project-add-btn")).Should().NotBeNull();
                panel.FindElement(By.Id("project-clear-btn")).Should().NotBeNull();
                panel.FindElement(By.Id("open-modal")).Should().NotBeNull();
                return;
            }
            else if (formSection == "Personal Summary")
            {
                panel.FindElement(By.Id("personal-summary-add-btn")).Should().NotBeNull();
                return;
            }
            else
            {
                panel.FindElement(By.Id($"{formSection.ToLower()}-add-btn")).Should().NotBeNull();
                panel.FindElement(By.Id($"{formSection.ToLower()}-clear-btn")).Should().NotBeNull();
                panel.FindElement(By.Id("open-modal")).Should().NotBeNull();
            }
        }
       
        [When(@"I click on the ""([^""]*)"" button")]
        public void WhenIClickOnTheButton(string buttonName)
        {
            switch (buttonName)
            {
                case "Next":
                    _createResumePage.ClickNextButton();
                    break;
                case "Previous":
                    _createResumePage.ClickPreviousButton();
                    break;
                default:
                    break;
            }
        }

        [Then(@"I should not see the ""([^""]*)"" button")]
        public void ThenIShouldNotSeeTheButton(string buttonName)
        {
            switch (buttonName)
            {
                case "Next":
                    var nextResult = _createResumePage.NextButtonExists();
                    nextResult.Should().BeFalse();
                    break;
                case "Previous":
                    var prevResult = _createResumePage.PreviousButtonExists();
                    prevResult.Should().BeFalse();
                    break;
                default:
                    break;
            }
        }

        [Given ("I have filled out the form sections for the tabs \"Education\", \"Employment\" and \"Projects\"")]
        public void GivenIHaveFilledOutTheFormSectionsForTheTabsEducationEmploymentAndProjects()
        {
            Common.ResetPaths();
            _createResumePage.FillOutEducationForm();
            _createResumePage.ClickNext();
            _createResumePage.FillOutEmploymentForm();
            _createResumePage.ClickNext();
            _createResumePage.ClickNext();
            _createResumePage.FillOutProjectsForm();
        }

        [When ("I click on the \"Submit\" button on the form")]
        public void WhenIClickOnTheSubmitButtonOnTheForm()
        {
            _createResumePage.SubmitForm();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            Common.Paths["ViewResume"] = Common.Paths["ViewResume"] + _viewResumeUrl;
        }

        [Then("I should be redirected to the {string} page with only the information I inputted into each tab section of the form displayed in a text editor")]
        public void ThenIShouldBeRedirectedToThePageWithOnlyTheInformationIInputtedIntoEachTabSectionOfTheFormDisplayedInATextEditor(string viewResume)
        {
            _viewResumePage.GoTo("ViewResume");
            _viewResumePage.QuillEditor.Text.Should().Contain("Education");
            _viewResumePage.QuillEditor.Text.Should().Contain("Employment");
            _viewResumePage.QuillEditor.Text.Should().Contain("Projects");
            _viewResumePage.QuillEditor.Text.Should().NotContain("Skills");
            _viewResumePage.QuillEditor.Text.Should().NotContain("Achievements");
        }
    }
}
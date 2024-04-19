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
    public class CHARP96StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CreateResumePageObject _createResumePage;
        private readonly ViewResumePageObject _viewResumePage;
        public string _viewResumeUrl;

        public CHARP96StepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _createResumePage = new CreateResumePageObject(browserDriver.Current);
            _viewResumePage = new ViewResumePageObject(browserDriver.Current);
        }

        [When("I submit my information in the CreateResume page form")]
        public void WhenISubmitMyInformationInTheCreateResumePageForm()
        {
            Common.ResetPaths();
            _createResumePage.FillOutForm();
            _createResumePage.SubmitForm();
            _viewResumeUrl = _createResumePage.GetViewResumeUrl();
            Common.Paths["ViewResume"] = Common.Paths["ViewResume"] + _viewResumeUrl;
        }

        [Then("I will be redirected to the {string} page with a WYSIWYG editor")]
        public void ThenIWillBeRedirectedToThePageWithAWYSIWYGEditor(string viewResume)
        {
            Thread.Sleep(1000);
            _viewResumePage.GetEditor().Should().BeTrue();
        }

        [Given("have been redirected to the \"ViewResume\" page")]
        public void GivenHaveBeenRedirectedToThePage()
        {
            _viewResumePage.GoTo("ViewResume");
        }

        [When("I type into the WYSIWYG editor")]
        public void WhenITypeIntoTheWYSIWYGEditor()
        {
            _viewResumePage.TypeIntoEditor();
        }

        [Then("I will be able to see the editor modify the content")]
        public void ThenIWillBeAbleToSeeTheEditorModifyTheContent()
        {
            _viewResumePage.QuillEditor.Text.Should().Contain("Hello, World!");
        }

        [When("I click on the \"Save Resume\" button")]
        public void WhenIClickOnTheSaveResumeButton()
        {
            _viewResumePage.SaveResume();
        }

        [Then("I will be able to see a confirmation message letting me know the resume saved successfully")]
        public void ThenIWillBeAbleToSeeAConfirmationMessageLettingMeKnowTheResumeSavedSuccessfully()
        {
            _viewResumePage.GetSaveMessage().Should().Contain("Resume saved successfully.");
        }

        [Given("I have saved my resume")]
        public void GivenIHaveSavedMyResume()
        {
            _viewResumePage.SaveResume();
        }

        [When("I click on the \"Export Resume\" button")]
        public void WhenIClickOnTheExportResumeButton()
        {
            _viewResumePage.ExportPdf();
        }

        [Then("I will be able to see a confirmation message letting me know the resume exported successfully")]
        public void ThenIWillBeAbleToSeeAConfirmationMessageLettingMeKnowTheResumeExportedSuccessfully()
        {
            _viewResumePage.GetSaveMessage().Should().Contain("Resume exported successfully.");
        }
    }
}
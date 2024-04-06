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
            _createResumePage.FillOutForm();
            _createResumePage.SubmitForm();
            string viewResumeUrlId = _createResumePage.GetViewResumeUrl();
            Common.Paths["ViewResume"] = Common.Paths["ViewResume"] + viewResumeUrlId;

        }

        [Then("I will be redirected to the {string} page with a WYSIWYG editor")]
        public void ThenIWillBeRedirectedToThePageWithAWYSIWYGEditor(string viewResume)
        {
            Thread.Sleep(1000);
            _viewResumePage.GetEditor().Should().BeTrue();
        }
    }
}
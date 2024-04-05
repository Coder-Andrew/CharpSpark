global using FluentAssertions;
global using NUnit;
using Reqnroll;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CHARP96StepDefinitions
    {
        // private Calculator calculator = new Calculator();
        private int _result;

        [Given("I am a logged in user")]
        public void GivenIAmALoggedInUser()
        {
            _scenarioContext.Pending();
        }

        [Given(@"the second number is (.*)")]
        public void GivenTheSecondNumberIs(int p0)
        {
            calculator.SecondNumber= p0;
        }

        [When(@"the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _result = calculator.Add();
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            p0.Should().Be(_result);
        }
    }
}
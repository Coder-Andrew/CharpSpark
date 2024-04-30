global using FluentAssertions;
global using NUnit;
using ResuMeta_BDDTests.Utilities;
using Reqnroll;

namespace ResuMeta_BDDTests.StepDefinitions
{
    [Binding]
    public class CalculatorStepDefinitions
    {
        private Calculator calculator = new Calculator();
        private int _result;

        [Given(@"the first number is (.*)")]
        public void GivenTheFirstNumberIs(int p0)
        {
            calculator.FirstNumber= p0;
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
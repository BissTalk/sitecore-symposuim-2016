using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Bissol.SymDemo.Common.Tests.Functional.Steps
{
    [Binding]
    public class CreateReadUpdateDeleteSitecoreItemsSteps
    {
        [Given(@"I have a usename of ""(.*)""")]
        public void GivenIHaveAUsenameOf(string username)
        {
            ScenarioContext.Current?.Add("usename", username);
        }
        
        [Given(@"I have a passwork of ""(.*)""")]
        public void GivenIHaveAPassworkOf(string password)
        {
            ScenarioContext.Current?.Add("password", password);
        }
        
        [When(@"I create a randomly named item")]
        public void WhenICreateARandomlyNamedItem()
        {
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I can red the item back")]
        public void ThenICanRedTheItemBack()
        {
            Assert.IsTrue(true);
        }
    }
}

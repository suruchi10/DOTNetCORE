using Automation.UI.Accelerators;
using Automation.UI.Accelerators.Pages;
using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Automation.Ui.Tests.Steps
{
    [Binding]
    class HeaderSteps
    {
        private DriverHelper _driverHelper;
        private HomePage homePage;
        private LoginPage loginPage;
        private HeaderTab headerPage;

        public HeaderSteps(DriverHelper driverHelper)
        {
            _driverHelper = driverHelper;
            homePage = new HomePage(_driverHelper.Driver);
            //loginPage = new LoginPage(_driverHelper.Driver);
            headerPage = new HeaderTab(_driverHelper.Driver);
        }

        [When(@"I click on '(.*)' tab")]
        public void WhenIClickOnTab(string header)
        {
            var result = headerPage.ClickOnHeader(header);
            Assert.IsTrue(result, "Header value not clicked");
        }

        [Then(@"I should see '(.*)' in the list")]
        public void ThenIShouldSeeInTheList(string value)
        {
            var result = headerPage.CheckEmployeePresent(value);
            Assert.IsTrue(result, "Employee not present");
        }


    }
}

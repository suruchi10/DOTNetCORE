using Automation.UI.Accelerators;
using Automation.UI.Accelerators.BaseClasses;
using Automation.UI.Accelerators.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Automation.Ui.Tests.Steps
{

    [Binding]
    public class LoginSteps : CustomControl
    {
        private DriverHelper _driverHelper;

        public LoginSteps(DriverHelper driverHelper) => _driverHelper = driverHelper;


        [Given(@"I navigate to application")]
        [Obsolete]
        public void GivenINavigateToApplication()
        {
            var loginPage = LoginPage.GetInstance(_driverHelper.Driver).NavigateToLoginPage(_driverHelper.Driver);
            Save("loginPage", loginPage);
        }

        [When(@"I click the Login link")]
        [Obsolete]
        public void GivenIClickTheLoginLink()
        {
            var homePage = HomePage.GetInstance(_driverHelper.Driver).ClickLogin;
            Save("homePage", homePage);
        }

        [When(@"I enter username and password")]
        [Obsolete]
        public void GivenIEnterUsernameAndPassword(Table table)
        {
            var loginPage = Retrieve<LoginPage>("loginPage");
            dynamic data = table.CreateDynamicInstance();
            loginPage.EnterUserNameAndPassword(data.UserName, data.Password);
        }

        [When(@"I click login")]
        public void GivenIClickLogin()
        {
            LoginPage.GetInstance(_driverHelper.Driver).ClickLogin();
        }

        [Then(@"I should see user logged in to the application")]

        public void ThenIShouldSeeUserLoggedInToTheApplication()
        {

            Assert.That(HomePage.GetInstance(_driverHelper.Driver).IsLogOffExist(), Is.True, "Log off button did not displayed");
        }



    }
}

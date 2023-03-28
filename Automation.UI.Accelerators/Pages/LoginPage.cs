
using Automation.Ui.Accelerators.Constants;
using Automation.UI.Accelerators.BaseClasses;
using OpenQA.Selenium;

namespace Automation.UI.Accelerators.Pages
{
    /// <summary>
    ///  Represents PageFunction. Inherates from BasePage.
    /// </summary>
    public partial class LoginPage : CustomControl
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public LoginPage()
        {
        }

        private new readonly IWebDriver Driver;
        private static LoginPage _instance;

        public LoginPage(IWebDriver driver)
        {
           this.Driver = driver;
        }

        public static LoginPage GetInstance(IWebDriver driver)
        {
            if (_instance == null)
            {
                _instance = new LoginPage(driver);
            }
            return _instance;
        }

        IWebElement txtUserName => GetLocator(Driver, By.Name("UserName"));
        IWebElement txtPassword => GetLocator(Driver,By.Name("Password"));
        IWebElement btnLogin => GetLocator(Driver,By.CssSelector(".btn-default"));
        IWebElement lnkLogOff => GetLocator(Driver,By.LinkText("Log off"));


        public void EnterUserNameAndPassword(string userName, string password)
        {
            txtUserName.SendKeys(userName);
            txtPassword.SendKeys(password);
        }

        public void ClickLogin()
        {
            btnLogin.Click();
            WaitForElement(Driver,lnkLogOff,true, 20);

        }

        //Method hiding example
        public new LoginPage NavigateToLoginPage(IWebDriver Driver)
        {
             var applicationUrl = Constants.URL;
             Driver.Navigate().GoToUrl(applicationUrl);           

            return new LoginPage(Driver);
        }

        
    }


    
}

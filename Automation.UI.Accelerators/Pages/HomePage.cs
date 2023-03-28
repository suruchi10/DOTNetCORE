
using Automation.UI.Accelerators.BaseClasses;
using OpenQA.Selenium;

namespace Automation.UI.Accelerators.Pages
{
    public class HomePage : CustomControl
    {
       
        private  IWebDriver Driver;
        private static HomePage _instance;

        public HomePage(IWebDriver driver) => this.Driver = driver;

        public static HomePage GetInstance(IWebDriver driver)
        {
            if (_instance == null)
            {
                _instance = new HomePage(driver);
            }
            return _instance;
        }

        private IWebElement lnkLogin => GetLocator(Driver, By.LinkText("Login"));

        private IWebElement lnkLogOff => GetLocator(Driver,By.LinkText("Log off"));

        public HomePage ClickLogin
        {
            get
            {
                lnkLogin.Click();
                return new HomePage(Driver);
            }
        }

        public bool IsLogOffExist() => lnkLogOff.Displayed;
    }
}

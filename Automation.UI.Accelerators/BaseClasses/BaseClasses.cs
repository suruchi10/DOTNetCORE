using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Xml;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Data;
using System.Text;
using Automation.Ui.Accelerators.Constants;
using Automation.UI.Accelerators.UtilityClasses;

namespace Automation.UI.Accelerators.BaseClasses
{
    /// <summary>
    /// This is the Super class for all pages
    /// </summary>
    /// 
    public abstract class BasePage
    {
        /// <summary>
        /// Get the browser configuration details
        /// </summary>
        public Dictionary<string, string> BrowserConfig = Utility.BrowserConfig;

        /// <summary>
        /// Gets or Sets Driver
        /// </summary>
        public RemoteWebDriver Driver { get; set; }

        /// <summary>
        /// Gets or Sets Test Data as XMLNode
        /// </summary>
        public XmlNode TestDataNode { get; set; }

        /// <summary>
        /// Gets or Sets Reporter
        /// </summary>


        public Dictionary<string, Dictionary<string, Dictionary<string, List<BrowserFinder>>>> PageObjects { get; set; }

        public const string SCROLLINTOVIEW = @"(arguments[0].scrollIntoView(true));";

        public static readonly string HighliteBorderScript = $@"arguments[0].style.cssText = 'border-width: 4px; border-style: solid; border-color: {"orange"}';";

        public BasePage()
        {

        }

        public BasePage(RemoteWebDriver driver)
        {
            Driver = driver;
        }

        public BasePage(XmlNode testNode)
        {
            TestDataNode = testNode;
        }

        public BasePage(RemoteWebDriver driver, XmlNode testNode)
        {
            Driver = driver;
            TestDataNode = testNode;
        }


        internal IWebElement WaitForElementVisible(IWebDriver Driver, IWebElement WebElement, int maxWaitTime = 60)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(maxWaitTime));
            wait.Until(webEle => WebElement);
            if (WebElement == null) return WebElement;
            var jsExecutor = (IJavaScriptExecutor)Driver;
            jsExecutor.ExecuteScript(HighliteBorderScript, new object[] { WebElement });
            jsExecutor.ExecuteScript(@"$(arguments[0].scrollIntoView(true));", new object[] { WebElement });

            return WebElement;
        }

        public void NavigateToLoginPage(IWebDriver Driver)
        {
            var applicationUrl = Constants.URL;
            Driver.Navigate().GoToUrl(applicationUrl);

            
        }

        public static void tearDown(IWebDriver driver)
        {
            driver.Quit();
        }

    }
}


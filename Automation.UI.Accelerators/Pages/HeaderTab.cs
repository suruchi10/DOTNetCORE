using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace Automation.UI.Accelerators.Pages
{
    public partial class HeaderTab 
    {
        private IWebDriver Driver;
        private static HeaderTab _instance;

        public HeaderTab(IWebDriver driver) => Driver = driver;

        public static HeaderTab GetInstance(IWebDriver driver)
        {
            if (_instance == null)
            {
                _instance = new HeaderTab(driver);
            }
            return _instance;
        }


        IList<IWebElement> EmployeeName => Driver.FindElements(By.CssSelector(" [class='table'] td:nth-child(1)"));

        IList<IWebElement> NavigationHeader => Driver.FindElements(By.CssSelector("li a[href]"));

        IWebElement PageHeader => Driver.FindElement(By.CssSelector("[class='table'] tbody tr:nth-child(1)"));

        public bool ClickOnHeader(string element)
        {
            foreach (var item in NavigationHeader.Where(item => item.Text.Equals(element)))
            {
                item.Click();
                return true;
            }

            return false;
        }

        public bool CheckEmployeePresent(string element)
        {
            foreach (var _ in EmployeeName.Where(item => item.Text.Equals(element)).Select(item => new { }))
            {
                return true;
            }

            return false;
        }

        
    }
}

using Automation.Ui.Accelerators.Constants;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace Automation.UI.Accelerators.BaseClasses
{
    public class CustomControl : DriverHelper
    {

        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static void EnterText(IWebElement webElement, string value) => webElement.SendKeys(value);

        public static void Click(IWebElement webElement) => webElement.Click();

        public static void SelectByValue(IWebElement webElement, string value)
        {
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByValue(value);
        }

        public static void SelectByText(IWebElement webElement, string text)
        {
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByText(text);
        }

        public bool WaitForElement(IWebDriver driver, IWebElement element, bool visible, int timeInSecs)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeInSecs));
            return wait.Until(d => visible && IsElementVisible(element) || visible && !IsElementVisible(element));
        }

        protected virtual bool IsElementVisible(IWebElement element)
        {
            try
            {
                bool isVisible = element != null && element.Displayed;
                return isVisible;
            }

            catch (Exception ex)
            {
                Logger.Debug("Failed to check if element is visible: {0}", ex.Message);
                return false;
            }
        }

        public void NavigateToLoginPage(IWebDriver Driver)
        {
            var applicationUrl = Constants.URL;
            Driver.Navigate().GoToUrl(applicationUrl);

        }

        [Obsolete]
        protected T Save<T>(string key, T data)
        {
            try
            {
                Store<T>(key, data);
            }

            catch
            {
                Replace<T>(key, data);
            }

            return data;
        }

        [Obsolete]
        protected T Replace<T>(string key, T data)
        {
            T result;

            if (ScenarioContext.Current.TryGetValue(key, out result))
            {
                ScenarioContext.Current.Set(data, key);
                return data;
            }

            throw new Exception(string.Format("Item not found: {0{", key));
        }

        [Obsolete]
        protected T Retrieve<T>(string key)
        {
            T result;

            if (ScenarioContext.Current.TryGetValue(key, out result))
            {
                return result;
            }

            throw new Exception(string.Format("Item not found: {0{", key));
        }

        [Obsolete]
        protected void Store<T>(string key, T data)
        {

            if (ScenarioContext.Current.ContainsKey(key))
            {
                throw new Exception(string.Format("Item already stored: {0{", key));
            }

            ScenarioContext.Current.Set(data, key);
        }
        public static IWebElement GetLocator(IWebDriver driver, By elementName)
        {
            IWebElement element = null;
            try
            {
                element = driver.FindElement(elementName);
                return element;
            }

            catch (Exception ex)
            {
                throw new NoSuchElementException(string.Format("Failed at GetLocator() {0}\n{1}", ex.StackTrace, ex.Message));
            }
        }


    }
}

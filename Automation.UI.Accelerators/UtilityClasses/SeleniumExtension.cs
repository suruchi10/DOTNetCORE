using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automation.UI.Accelerators.UtilityClasses
{
    public static class SeleniumExtension
    {
        /// <summary>
        /// HighLights elements in web page.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="driver"></param>
        public static IWebElement HighLightElement(this IWebElement element, RemoteWebDriver driver, string color="orange")
        {

            if (element == null)
                return null;

            var highlightJs =
                $@"arguments[0].style.cssText = ""border-width: 4px; border-style: solid; border-color: {color}"";";
            var elementToHighlight = new object[] { element };
            var scrollToViewScript = "$(arguments[0].scrollIntoView(true));";

            try
            {
                var jsExecutor = (IJavaScriptExecutor)driver;
                jsExecutor.ExecuteScript(highlightJs, elementToHighlight);
                jsExecutor.ExecuteScript(scrollToViewScript, elementToHighlight);
            }
            catch
            {
                // ignored
            }

            return element;
        }

        public static IWebElement GetLocator(IWebDriver driver , string elementType, string elementName)
        {
            IWebElement element = null;
            try
            {
                switch (elementType.ToLower())
                {
                    case "xpath":
                        {
                            element = driver.FindElement(By.XPath(elementName));
                        }
                        break;
                    case "name":
                        {
                            element = driver.FindElement(By.Name(elementName));
                        }
                        break;
                    case "id":
                        {
                            element = driver.FindElement(By.Id(elementName));
                        }
                        break;
                    case "classname":
                        {
                            element = driver.FindElement(By.ClassName(elementName));
                        }
                        break;
                    case "CssSelector":
                        {
                            element = driver.FindElement(By.CssSelector(elementName));
                        }
                        break;
                    case "tagname":
                        {
                            element = driver.FindElement(By.TagName(elementName));
                        }
                        break;
                    case "linktext":
                        {
                            element = driver.FindElement(By.LinkText(elementName));
                        }
                        break;
                    case "partiallinktext":
                        {
                            element = driver.FindElement(By.PartialLinkText(elementName));
                        }
                        break;
                }
                return element;
            }
            catch (Exception ex)
            {
                throw new NoSuchElementException(string.Format("Failed at GetLocator() {0}\n{1}", ex.StackTrace, ex.Message));
            }
        }
    }
}
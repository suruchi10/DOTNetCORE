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

namespace Automation.API.Accelerators.BaseClasses
{
    /// <summary>
    /// This is the Super class for all pages
    /// </summary>
    /// 
    public  class BasePage
    {
        public IWebDriver Driver { get; set; }

    }
}


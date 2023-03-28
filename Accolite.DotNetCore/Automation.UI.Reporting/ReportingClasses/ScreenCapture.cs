using AventStack.ExtentReports;
using NLog;
using NLog.Targets;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Automation.Reporting.ReportingClasses
{
    public sealed class ScreenCapture
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void SaveBrowserScreen(IWebDriver driver, string title)
        {
            try
            {
                //Capturing the test failure screenshot 
                var fileName = GetScreenshotFileName(title);
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fileName, ScreenshotImageFormat.Png);
                logger.Debug("Successfully captured screenshot to {0} {1}", Environment.NewLine, fileName);
            }

            catch (Exception ex)
            {
                logger.Error("Failed to take screenshot {0}", ex.Message);
            }
        }

        public static MediaEntityModelProvider CaptureScreenAndReturnFileName(IWebDriver driver, string title)
        {
            try
            {
                //Capturing the test failure screenshot 

                var screenShot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;

                return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, title).Build();

            }

            catch (Exception ex)
            {
                logger.Error("Failed to take screenshot {0}", ex.Message);
                return null;
            }
        }
        public static string GetScreenshotFileName(string title)
        {
            var file = LogManager.Configuration.FindTargetByName("fileTarget") as FileTarget;
            var dir = Path.GetDirectoryName(file.FileName.Render(new LogEventInfo { TimeStamp = DateTime.Now }));
            title = string.Join(string.Empty, title.Split(Path.GetInvalidFileNameChars()));
            var date = DateTime.Now.ToLongTimeString().Replace(":", "");
            return Path.Combine(dir, title + "_" + date + ".png");
        }
    }
}

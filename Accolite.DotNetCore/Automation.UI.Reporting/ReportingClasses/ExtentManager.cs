using Automation.Reporting.Helper;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;


namespace Automation.Reporting.ReportingClasses
{
    public class ExtentManager
    {
        public static readonly object synchroniser = new object();
        public static ExtentReports extent = new ExtentReports();
        private static string newPath = Constants.NewPath + DateTime.Now.ToString("yyyy_MM_dd_HHmm tt") + "\\";
        private static string reportpath = newPath + DateTime.Now.ToString("yyyy_MM_dd_HHmm tt");
        private static string reportFileName = " Execution Report" + DateTime.Now.ToString("yyyy_MM_dd_HHmm tt");


        public static ExtentReports createInstance()
        {

            var htmlReporter = new ExtentHtmlReporter(reportpath);
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = reportFileName;
            htmlReporter.Config.EnableTimeline = true;
            htmlReporter.Config.ReportName = reportFileName;
            extent.AddSystemInfo("Operating System : ", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Machine Name : ", Environment.MachineName);

            extent.AnalysisStrategy = AnalysisStrategy.BDD;

            extent.AttachReporter(htmlReporter);

            return extent;
        }
    }
}

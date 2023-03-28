using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Text;
using AventStack.ExtentReports.Gherkin.Model;

namespace Automation.Reporting.ReportingClasses
{
    public class ExtentReport
    {
        public static readonly object synchroniser = new object();

        public static ExtentReports extentReports = ExtentManager.createInstance();
        private static ExtentTest extentTest = null;
        private static Dictionary<string, ExtentTest> extentFeatureMap = new Dictionary<string, ExtentTest>();
        private static Dictionary<string, ExtentTest> extentScenarioMap = new Dictionary<string, ExtentTest>();

        public static ExtentTest getFeature(string featureName)
        {
            lock (synchroniser)
            {
                return extentFeatureMap[featureName];
            }
        }

        public static ExtentTest getScenario(string scenarioName)
        {
            lock (synchroniser)
            {
                try
                {
                    return extentScenarioMap[scenarioName];
                }
                catch (KeyNotFoundException e)
                {
                    return null;
                }
            }
        }

        public static ExtentTest startFeature(string featureName)
        {
            lock (synchroniser)
            {
                ExtentTest test;
                if (!extentFeatureMap.TryGetValue(featureName, out test))
                {
                    test = extentReports.CreateTest<Feature>(featureName);
                    extentFeatureMap.Add(featureName, test);
                }
                return test;
            }
        }

        public static ExtentTest startScenario(string scenarioName, string featureName)
        {
            lock (synchroniser)
            {
                if (getScenario(scenarioName) == null)
                {
                    extentTest = getFeature(featureName);
                    extentTest = extentTest.CreateNode<Scenario>(scenarioName);
                    extentScenarioMap.Add(scenarioName, extentTest);
                    return extentTest;
                }

                else
                {
                    return getScenario(scenarioName);
                }
            }
        }

        public static void flushReport()
        {
            extentReports.Flush();
        }


    }
}

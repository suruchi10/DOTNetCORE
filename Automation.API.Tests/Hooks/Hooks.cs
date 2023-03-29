
using Automation.API.Accelerators.BaseClasses;
using AventStack.ExtentReports.Gherkin.Model;
using NLog;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Text;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Automation.Reporting.ReportingClasses;

namespace Automation.API.Tests.Hooks
{
    [Binding]
    public sealed class Hooks : BasePage
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public static readonly object synchroniser = new object();
        public static readonly string TestId = DateTime.Now.TimeOfDay.TotalSeconds.ToString();
        private static BasePage _driverHelper;

        public Hooks(BasePage driverHelper) => _driverHelper = driverHelper;



        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            logger.Info("*********Cleaning Chrome Instances*******");
            Process.Start("cmd.exe", "/C taskkill /F /IM chromedriver.exe /T");
            logger.Info("*********Starting New Tests*******");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext FeatureContext)
        {
            logger.Debug("Start Feature");
            ExtentReport.startFeature(FeatureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext ScenarioContext, FeatureContext FeatureContext)
        {
            lock (synchroniser)
            {
                logger.Debug("Test: {0}", ScenarioContext.ScenarioInfo.Title);
                ExtentReport.startScenario(ScenarioContext.ScenarioInfo.Title, FeatureContext.FeatureInfo.Title);

            }
        }

        [BeforeStep]
        public void BeforeStep(ScenarioContext ScenarioContext)
        {
            lock (synchroniser)
            {
                logger.Debug("Step: {0}", ScenarioContext.StepContext.StepInfo.Text);
            }
        }


        [AfterStep]
        public void AfterStep(ScenarioContext ScenarioContext)
        {
            lock (synchroniser)
            {
                logger.Debug("Step Complete: {0}", ScenarioContext.StepContext.StepInfo.Text);
                var stepInfo = ScenarioContext.StepContext.StepInfo;
                var stepType = ScenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

                if (ScenarioContext.TestError == null)
                {
                    if (stepType == "Given")
                        ExtentReport.getScenario(ScenarioContext.ScenarioInfo.Title).CreateNode<Given>(ScenarioContext.StepContext.StepInfo.Text);
                    else if (stepType == "When")
                        ExtentReport.getScenario(ScenarioContext.ScenarioInfo.Title).CreateNode<When>(ScenarioContext.StepContext.StepInfo.Text);
                    else if (stepType == "Then")
                        ExtentReport.getScenario(ScenarioContext.ScenarioInfo.Title).CreateNode<Then>(ScenarioContext.StepContext.StepInfo.Text);
                    else if (stepType == "And")
                        ExtentReport.getScenario(ScenarioContext.ScenarioInfo.Title).CreateNode<And>(ScenarioContext.StepContext.StepInfo.Text);

                }

                else if (ScenarioContext.TestError != null)
                {

                    logger.Error(ScenarioContext.TestError, "Failed test {0}", ScenarioContext.TestError.Message);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(ScenarioContext.ScenarioInfo.Title)
                        .Append(TestId);


                }

                else if (ScenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
                {
                    if (stepType == "Given")
                        ExtentReport.getScenario(ScenarioContext.ScenarioInfo.Title).CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else if (stepType == "When")
                        ExtentReport.getScenario(ScenarioContext.ScenarioInfo.Title).CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else if (stepType == "Then")
                        ExtentReport.getScenario(ScenarioContext.ScenarioInfo.Title).CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }


            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext ScenarioContext)
        {
            lock (synchroniser)
            {
                if (ScenarioContext.TestError != null)
                {
                    logger.Error(ScenarioContext.TestError, "Failed test {0}", ScenarioContext.TestError.Message);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(ScenarioContext.ScenarioInfo.Title)
                        .Append(TestId);


                }
                else
                {
                    logger.Debug("Test completed successfully: {0}", ScenarioContext.ScenarioInfo.Title);
                }
   

            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            logger.Debug("Feature Complete");
            _driverHelper.Driver.Close();
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReport.flushReport();

        }
    }
}

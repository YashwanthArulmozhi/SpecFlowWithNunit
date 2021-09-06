
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SeleniumCSharpSpecflowProject
{
    [Binding]
    class ReporterClass
    {

        private static ExtentHtmlReporter htmlReporter;
        private static ExtentReports extentReports;
        static string reportCompleteFilePath;
        static string reporterNameTimeStamp;
       private static ExtentTest featureName;
       private static ExtentTest scenarioName;
        static ExtentTest stepName;
        public static ThreadLocal<ExtentTest> stepThreadLocal = new ThreadLocal<ExtentTest>();
        public static ThreadLocal<ExtentTest> featureThreadLocal = new ThreadLocal<ExtentTest>();

        public static void SetStepThradLocal(ExtentTest stepThread)
        {
            stepThreadLocal.Value = stepThread;
        }

        public static ThreadLocal<ExtentTest> GetStepThreadLocal()
        {
            return stepThreadLocal;
        }

        [BeforeTestRun]
        public static void CreateExtentHtmlReporter()
        {
            
                reporterNameTimeStamp = DateTime.Now.ToString("dd_MMM_yyyy_HH_mm_ss");
                reportCompleteFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent +@"\Reports\TestReport\Report_"+reporterNameTimeStamp+@"\";
                Console.WriteLine("Path of the Report File - > " + reportCompleteFilePath);
             htmlReporter = new ExtentHtmlReporter(reportCompleteFilePath);
             htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            htmlReporter.Config.ReportName = "Automation Report";
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void CreateFeature(FeatureContext featureContext)
        {
            featureName = extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
        }

        [BeforeScenario]
        public static void CreateScenario(ScenarioContext scenarioContext)
        {
            scenarioName = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
           
        }

        [BeforeStep]
        public static void createStep(ScenarioContext scenarioContext)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            switch (stepType)
            {
                case "Given":
                    stepName = scenarioName.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                    SetStepThradLocal(stepName);
                    break;
                case "When":
                    stepName = scenarioName.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                    SetStepThradLocal(stepName);
                    break;
                case "Then":
                    stepName = scenarioName.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                    SetStepThradLocal(stepName);
                    break;
                case "And":
                    stepName = scenarioName.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                    SetStepThradLocal(stepName);
                    break;
            }
        }

        

        [AfterStep]
        public static void CreateStepsWithScenario(ScenarioContext scenarioContext)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {
                switch (stepType)
                {
                    case "Given":
                        stepName.Pass("Step Passed Successfully");
                        break;
                    case "When":
                        stepName.Pass("Step Passed Successfully");
                        break;
                    case "Then":
                        stepName.Pass("Step Passed Successfully");
                        break;
                    case "And":
                        stepName.Pass("Step Passed Successfully");
                        break;
                }
            }
            else if(scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                string filePathToSaveScreenshots = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent + @"\Reports\Screenshots\ScreenshotImage"+DateTime.Now.ToString("ddMMHHmmss")+".png";
                switch (stepType)
                {
                    case "Given":
                        stepName.Fail(scenarioContext.TestError.Message).AddScreenCaptureFromPath(CommonActionClass.TakeScreenshotImage(filePathToSaveScreenshots),"Failed Image");
                        break;
                    case "When":
                        stepName.Fail(scenarioContext.TestError.Message).AddScreenCaptureFromPath(CommonActionClass.TakeScreenshotImage(filePathToSaveScreenshots),"Failed Image");
                        break;
                    case "Then":
                        stepName.Fail(scenarioContext.ScenarioExecutionStatus.ToString()).AddScreenCaptureFromPath(CommonActionClass.TakeScreenshotImage(filePathToSaveScreenshots),"Failed Image");
                        break;
                    case "And":
                        stepName.Fail(scenarioContext.TestError.Message).AddScreenCaptureFromPath(CommonActionClass.TakeScreenshotImage(filePathToSaveScreenshots),"Failed Image");
                        break;
                }
            }
        }

       public static void AddStepLog(string passedDescription)
        {
            GetStepThreadLocal().Value.Pass(passedDescription);
        }

         public static void AddFailedStepLog(string failedDescription)
         {
            GetStepThreadLocal().Value.Fail(failedDescription);
         }

        [AfterTestRun]
        public static void AfterTestReporterFlush()
        {
            try
            {
                extentReports.Flush();
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = reportCompleteFilePath + "index.html";
                process.Start();
            }
            catch (Exception e)
            {
              Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}

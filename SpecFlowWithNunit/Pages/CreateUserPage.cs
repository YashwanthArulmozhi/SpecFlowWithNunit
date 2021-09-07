using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCSharpSpecflowProject.Steps;
using SpecFlowWithNunit.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SeleniumCSharpSpecflowProject
{
    class CreateUserPage : CommonActionsUtils
    {

        private By link_AddUser = By.XPath("//a[contains(text(),'Add a User')]");
        private By link_RunProject = By.XPath("//span[text()='Run this project']");
        private By link_Register = By.XPath("//a[text()='Register']");
        private By input_FirstName = By.XPath("//input[@formcontrolname='firstName']");
        private By input_LastName = By.XPath("//input[@formcontrolname='lastName']");
        private By input_UserName = By.XPath("//input[@formcontrolname='username']");
        private By input_PWd = By.XPath("//input[@formcontrolname='password']");
        private By button_Register = By.XPath("//button[contains(text(),'Register')]");
        private By label_SuccessMsg = By.XPath("//div[@class='alert alert-success']");
        private By input_EmailId = By.Name("username");
        private By input_Password = By.Name("password");
        private By button_Save = By.XPath("//input[@value='save']");
        private By label_Details = By.XPath("//b[text()='The username:']/..");
        private By link_Login = By.XPath("(//a[@href='login.php'])[1]");

        public string applicationUrl;
        public static string userName;
        public static string password;

       /* public void IntializeData(string scenarioName)
        {
            applicationUrl = ReadDataFromExcel(scenarioName, "URL");
            userName = ReadDataFromExcel(scenarioName, "Username");
            password = ReadDataFromExcel(scenarioName, "Password");
        }*/

        public void LaunchTheApplication()
        {
            // string v = "http://thedemosite.co.uk/login.php";
            applicationUrl = ExcelUtils.ReadDataFromExcel(CreateUserSteps.scenarioTitle, "URL");
            LaunchApplication(applicationUrl);
            ReporterClass.AddStepLog("Application URL - > " + applicationUrl);
        }

        public void ClickRubProjectIfDisplayed()
        {
            if(WaitForDynamicObjectToAppear(link_RunProject)==true)
            {
                ClickElement(link_RunProject);
            }
        }

        public void RegisterUser()
        {
            userName = ExcelUtils.ReadDataFromExcel(CreateUserSteps.scenarioTitle, "Username");
            password = ExcelUtils.ReadDataFromExcel(CreateUserSteps.scenarioTitle, "Password");
            ClickRubProjectIfDisplayed();
            if (WaitForElement(link_Register) != null)
            {
                ClickElement(link_Register);
                if (WaitForElement(input_FirstName) != null)
                {
                    SendValue(input_FirstName, userName);
                    SendValue(input_LastName, userName);
                    SendValue(input_UserName, userName);
                    SendValue(input_PWd, password);
                    ClickElement(button_Register); 
                }
              
            }
        }

        public void ValidateSuccessFullRegistration()
        {
            WaitForElement(label_SuccessMsg);
            if (GetTextValue(label_SuccessMsg).Equals("Registration successful", StringComparison.OrdinalIgnoreCase))
            {
                ReporterClass.AddStepLog("Success Message Validated. Success Message in UI -> "+ GetTextValue(label_SuccessMsg));
            }
            else
            {
               Assert.Fail("Success message is not displayed correctly. Message in UI -> "+ GetTextValue(label_SuccessMsg));
            }
        }

        public void CreateNewUser()
        {
            if (WaitForElement(link_AddUser) != null)
            {
                ClickElement(link_AddUser);
                if (WaitForElement(input_EmailId) != null)
                {
                    //Learning purpose code
                    /*StreamReader s = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).FullName + @"\SeleniumCSharpSpecflowProject\Config.txt");
                    string line=null;
                    while(!String.IsNullOrEmpty(line = s.ReadLine()))
                    {
                        String value = line.Split("=")[1];
                        Console.WriteLine($"value is ->{value}");
                    }
                    s.Close();*/
                    
                    SendValue(input_EmailId, userName);
                    SendValue(input_Password, password);
                    ClickElement(button_Save);
                    ReporterClass.AddStepLog("Successfully Clicked on Save button");
                }
            }
        }

        public void ValidateNewUser()
        {
            if (WaitForElement(label_Details) != null)
            {
                string userCreateInUI = GetTextValue(label_Details).Substring(14, 9);
               string value = userCreateInUI.Trim().Should().Contain(userName).ToString();
               // CommonActionClass.TakeScreenshotImage(Directory.GetParent(Environment.CurrentDirectory).FullName + @"Reports\Screenshots\ScreenshotImage" + DateTime.Now.ToString("ddMMHHmmss") + ".png");
              Console.WriteLine("User Created in UI - >" + userCreateInUI);
                Console.WriteLine("Validated Data - >" + value);
            }
        }


        public void CloseDriver()
        {
            CloseBrowser();
        }
    }
}

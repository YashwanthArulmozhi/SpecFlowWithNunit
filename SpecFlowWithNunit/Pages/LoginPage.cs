using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SeleniumCSharpSpecflowProject
{
    class LoginPage : CommonActionClass
    {

        private By input_EmailId = By.Name("username");
        private By input_Password = By.Name("password");
        private By input_UserName = By.XPath("//input[@formcontrolname='username']");
        private By input_PWd = By.XPath("//input[@formcontrolname='password']");
        private By button_Lgn = By.XPath("//button[contains(text(),'Login')]");
        private By label_Welcome = By.XPath("//p[contains(text(),'Angular')]/../h1");
        private By label_SuccessMsg = By.XPath("//b[contains(text(),'Successful Login')]");
        private By button_Login = By.XPath("//input[@value='Test Login']");
        private By link_Logout = By.XPath("//a[text()='Logout']");
       String DeleteLink = "//a[contains(text(),'PARAMETER')]/a";


        public void LogIn()
        {
            if (WaitForElement(input_UserName) != null)
            {
                SendValue(input_UserName, CreateUserPage.userName);
                SendValue(input_PWd, CreateUserPage.password);
                ClickElement(button_Lgn);
            }
        }

        public void ValidateWelcomeMessage()
        {
            WaitForElement(label_Welcome);
            string UiUserWelcomeMsg = GetTextValue(label_Welcome).Split(" ")[1].Replace("!", "");
            if (UiUserWelcomeMsg.Equals(CreateUserPage.userName, StringComparison.OrdinalIgnoreCase))
            {
                ReporterClass.AddStepLog("UI Message - > " + GetTextValue(label_Welcome));
            }
            else
            {
                ReporterClass.AddFailedStepLog("Failed to Validate Welcome message");
            }
        }

        public void Logout()
        {
            WaitForElement(link_Logout);
            ClickElement(link_Logout);
            if(GetSizeOfElements(input_UserName)==1)
            {
                ReporterClass.AddStepLog("Successfully Logged out");
            }
            else
            {
                ReporterClass.AddFailedStepLog("Not Logged out");
            }
        }

        public void DeleteUserAndValidate()
        {
            try
            {
                DeleteLink = DeleteLink.Replace("PARAMETER", CreateUserPage.userName);
                By link_Delete = By.XPath(DeleteLink);
                WaitForElement(link_Delete);
                ClickElement(link_Delete);
                WaitForDynamicObjectToAppear(link_Delete);
                //Thread.Sleep(7000);
                if (GetSizeOfElements(link_Delete) == 0)
                {
                    ReporterClass.AddStepLog("User is Deleted");
                }
                else
                {
                    Assert.Fail("User is not Deleted");
                }
            }
            catch(Exception e)
            {
                ReporterClass.AddFailedStepLog("Element not available. " +e.InnerException);
                Logout();
                Assert.Fail();
            }
        }

        public void ProvideLoginDetails()
        {
            if (WaitForElement(input_EmailId) != null)
            {
                SendValue(input_EmailId, CreateUserPage.userName);
                SendValue(input_Password, CreateUserPage.password);
                ClickElement(button_Login);
                Console.WriteLine("Successfully Clicked on Login button");
            }
        }

        public void ValidateSuccessFulLogin()
        {
            if (WaitForElement(label_SuccessMsg) != null)
            {
                if (GetTextValue(label_SuccessMsg).Contains("Successful Login"))
                {
                    Console.WriteLine("Passed - > " + GetTextValue(label_SuccessMsg));
                }
                else
                {
                    Console.WriteLine("Failed - > " + GetTextValue(label_SuccessMsg));
                }
            }
        }

    }
}

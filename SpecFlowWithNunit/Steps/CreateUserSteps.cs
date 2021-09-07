using System;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace SeleniumCSharpSpecflowProject.Steps
{
    [Binding]
    public class CreateUserSteps
    {
       public static string scenarioTitle;

        CreateUserPage createUserPage = new CreateUserPage();
        LoginPage loginPage = new LoginPage();

        [Before]
        public void GetTestData(ScenarioContext scenarioContext)
        {
            scenarioTitle = scenarioContext.ScenarioInfo.Title;
            scenarioTitle = scenarioTitle.Split("_")[1];
            Console.WriteLine(scenarioTitle);
        }

        [Given(@"Launch the application")]
        public void GivenLaunchTheApplication()
        {
            createUserPage.LaunchTheApplication();
        }
        
        [When(@"Create a new user")]
        public void WhenCreateANewUser()
        {
            // CreateUser.CreateNewUser();
            createUserPage.RegisterUser();
        }
        
        [Then(@"Verify new user is displayed correctly")]
        public void ThenVerifyNewUserIsDisplayedCorrectly()
        {
            // CreateUser.ValidateNewUser();
            createUserPage.ValidateSuccessFullRegistration();
        }

        [When(@"Provide login details and click Login")]
        public void WhenProvideLoginDetailsAndClickLogin()
        {
            //login.ProvideLoginDetails();
            loginPage.LogIn();
        }

        [Then(@"Validate the login successful message displayed in UI")]
        public void ThenValidateTheLoginSuccessfulMessageDisplayedInUI()
        {
            // login.ValidateSuccessFulLogin();
            loginPage.ValidateWelcomeMessage();
        }

        [Then(@"Delete the newly Created user and validate")]
        public void ThenDeleteTheNewlyCreatedUserAndValidate()
        {
            loginPage.DeleteUserAndValidate();
        }

        [Then(@"Logout of the application")]
        public void ThenLogoutOfTheApplication()
        {
            Console.WriteLine("Url is displayed : " + createUserPage.applicationUrl);
            loginPage.Logout();
        }


    }
}

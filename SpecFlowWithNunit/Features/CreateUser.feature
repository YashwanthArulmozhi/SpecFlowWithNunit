Feature: DemoQa
	Application to create a new user and Login

@Scenario1
 Scenario:1_Launch application and Create user and validate the user is create successfully
	Given Launch the application
	When Create a new user
	Then Verify new user is displayed correctly


	@Scenario2
 Scenario:2_Login to the application with the newly created user and validate login successful
	Given Launch the application
	When Provide login details and click Login
	Then Validate the login successful message displayed in UI
	Then Logout of the application


	@Scenario3
 Scenario:3_Login to the application with the newly created user and Delete the newly created user
	Given Launch the application
	When Provide login details and click Login
	Then Validate the login successful message displayed in UI
	Then Delete the newly Created user and validate
Feature: HomePage
	Simple calculator for adding two numbers

@smoke
Scenario: Login user as Administrator
	Given I navigate to application
	When I click the Login link
	And I enter username and password
		| UserName | Password |
		| admin    | password |
	And I click login
	Then I should see user logged in to the application
Feature: ManageUser Validate Tests on Admin

Background: 
	Given I navigate to application
	When I click the Login link
	And I enter username and password
		| UserName | Password |
		| admin    | password |
	And I click login
	Then I should see user logged in to the application

@smoke @sanity
Scenario Outline: Test to verify if Employee exist
   When I click on '<Tab>' tab
   Then I should see '<EmployeeName>' in the list

   Examples: 
       |Tab				 |EmployeeName|
	   |Employee List	 |Ramesh      |
	   |Employee Details |John0		  |
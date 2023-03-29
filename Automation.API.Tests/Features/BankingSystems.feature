Feature: BankingSystems

This feature is to Test all banking operations with Restsharp.net


Background: : Given I have a SQL Injection
	And I hit banking operation systems
	Then I should get the response code as '200'

Scenario: Verify an user should be able to create accounts

	Given I enter all the mandatory details of user
	When I perform operation for post "2"
	Then I should get the response code as '201'
	And I should see the user is successfully created

Scenario: Verify an existing user should be able to create accounts

    Given I have an existing user
	And I enter all the mandatory details of user
	When I perform operation for post "2"
	Then I should get the response code as '201'
	And I should see the user is successfully created

Scenario Outline: Verify an user should not be able to deposit more than 10000 in single transaction

	Given I search for an existing user
	And I perform deposit operation with Amount '<amount>'
	When '<amount>' is within the speified limit of less than 10K
	Then I should get the response code as 201

	Examples:
	| amount |
	| 10000  |
	| 10001  |
	| 9999   |

Scenario: Verify an user should not be able to withdraw amount if balance is less than $100

	Given I search for an existing user
	And I check for balance
	When user place a request to withdraw certain amounts
	And the current balance would go lesser than threshhold limit
	Then User should not be able to withdraw

Scenario: Verify an user should be able to withdraw amount

	Given I search for an existing user
	And I check for balance
	When user place a request to withdraw certain amounts
	And the current balance would not go lesser than threshhold limit
	Then User should be able to withdraw




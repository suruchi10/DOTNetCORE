Feature: GetPosts
	Test GET posts operation with Restsharp.net

Scenario Outline: Verify author of the posts with Id 2 
	Given I perform GET operation for "posts/2"
	When I perform operation for post "2"
	Then I should see the '<Author/Title>' name as '<Name>'

	Examples: 
	| Author/Title | Name        |
	| author       | Karna       |
	


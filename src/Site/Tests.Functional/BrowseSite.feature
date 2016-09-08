@WebDriver
Feature: BrowseSite
	In order to see the site
	As a browser of the web
	I want to see a cool home page

@smoke
Scenario: View the scroll
	When I go to the home page
	Then I can see the scroll

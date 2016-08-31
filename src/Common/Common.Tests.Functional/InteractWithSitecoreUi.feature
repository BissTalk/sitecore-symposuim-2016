@WebDriver
Feature: InteractWithTheSitecoreUi
	In order to manage content
	As a content editor
	I need to be able to use the sitecore UI

@smoke
Scenario: Sitecore Login
	When I login to Sitecore as an admin
	Then I should be on to the LaunchPad page
	


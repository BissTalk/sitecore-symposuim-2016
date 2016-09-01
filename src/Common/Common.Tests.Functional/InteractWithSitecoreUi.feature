@WebDriver
Feature: InteractWithTheSitecoreUi
	In order to manage content
	As a content editor
	I need to be able to use the sitecore UI

@smoke
Scenario: Sitecore login
	When I login to Sitecore as an admin
	Then I should be on the LaunchPad page
	Given I am on the LaunchPad Page


@smoke
Scenario: Content Editor pages come up without error
	When I login to Sitecore as an admin
	And I navigate to the LaunchPad
	And I click the Experience Editor link on the LaunchPad
	Then I should be on the Experience Editor page
	When I navigate to the LaunchPad
	And I click the Content Editor link on the LaunchPad
	Then I should be on the Content Editor page


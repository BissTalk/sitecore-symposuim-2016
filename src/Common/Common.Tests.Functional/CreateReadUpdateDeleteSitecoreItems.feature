Feature: CreateReadUpdateDeleteSitecoreItems
	In order to manage content on our web sites
	As a content author
	I need to have the ability to create, read, update, and delete Sitecore items

@smoke
Scenario: Create a new item in Sitecore CMS
	Given I have a usename of "jbissol"
	And I have a passwork of "jbissol"
	When I create a randomly named item
	Then I can red the item back

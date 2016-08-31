﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using Bissol.SymDemo.Common.Tests.Functional.Factories;
using Bissol.SymDemo.Common.Tests.Functional.Pages;
using Bissol.SymDemo.Common.Tests.Functional.Settings;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Bissol.SymDemo.Common.Tests.Functional.Steps
{
    /// <summary>
    ///     Step definision methods for supporting test cases for interacting with the Sitecore web UI.
    /// </summary>
    [Binding]
    public class InteractWithTheSitecoreUiSteps
    {
        /// <summary>
        ///     Gets the current scenario context.
        /// </summary>
        /// <value>
        ///     The current scenario context.
        /// </value>
        [NotNull]
        // ReSharper disable once AssignNullToNotNullAttribute
        private ScenarioContext Context => ScenarioContext.Current;

        /// <summary>
        ///     Gets the application settings.
        /// </summary>
        /// <value>
        ///     The application settings.
        /// </value>
        [NotNull]
        private static ApplicationSettings Settings => ApplicationSettings.Default ?? new ApplicationSettings();

        /// <summary>
        ///     Initializes an instance of a web driver.
        /// </summary>
        [BeforeScenario("WebDriver"), UsedImplicitly]
        public void Initialize()
        {
            var webDriver = WebDriverFactory.Create(Settings.WebDriver ?? "chrome");
            // ReSharper disable PossibleNullReferenceException
            webDriver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 60));
            webDriver.Manage().Window.Size = new Size(1920, 1080);
            // ReSharper restore PossibleNullReferenceException
            Context.SetWebDriver(webDriver);
        }

        /// <summary>
        ///     Cleans up the web driver when the scenario is complete.
        /// </summary>
        [AfterScenario("WebDriver"), UsedImplicitly]
        public void CleanUp()
        {
            var webDriver = Context.WebDriver();
            webDriver.Close();
            webDriver.Quit();
            webDriver.Dispose();
            Context.SetWebDriver(null);
        }

        /// <summary>
        ///     Executes a login to the Sitecore login screen.
        /// </summary>
        [When(@"I login to Sitecore as an admin"), UsedImplicitly]
        public void WhenLoginToSitecore()
        {
            Context.GoTo("/sitecore")
                .VerifyPage<SitecoreLogin>()
                .TakeScreenshot("sitecore-login.jpg", ImageFormat.Jpeg)
                .SetUserNameField(Settings.SitecoreAdminUser)
                .SetPasswordField(Settings.SitecoreAdminPassword)
                .ClickLogInButton();
        }

        /// <summary>
        ///     Verifies the launch pad page was loaded.
        /// </summary>
        [Then(@"I should be on to the LaunchPad page"), UsedImplicitly]
        public void ThenIShouldGetRediretedToTheLaunchPadPage()
        {
            Context.VerifyPage<Launchpad>()
                .TakeScreenshot("sitecore-launchpad.jpg", ImageFormat.Jpeg);
        }
    }
}
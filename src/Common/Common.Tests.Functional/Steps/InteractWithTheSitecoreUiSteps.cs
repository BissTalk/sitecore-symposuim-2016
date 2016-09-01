using System;
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
        ///     Verifies the LaunchPad page was loaded.
        /// </summary>
        [Then(@"I should be on the LaunchPad page"), UsedImplicitly]
        public void VerifyLaunchPadPage()
        {
            Context.VerifyPage<Launchpad>()
                .TakeScreenshot("sitecore-launchpad.jpg", ImageFormat.Jpeg);
        }

        /// <summary>
        ///     Given the I am on the LaunchPad page.
        /// </summary>
        [Given(@"I am on the LaunchPad Page"), UsedImplicitly]
        public void GivenIAmOnTheLaunchPadPage()
        {
            Context.VerifyPage<Launchpad>();
        }

        /// <summary>
        ///     When I click the LaunchPad Experience Edior link.
        /// </summary>
        [When(@"I click the Experience Editor link on the LaunchPad"), UsedImplicitly]
        public void ClickLaunchPadExperienceEditorLink()
        {
            Context.LoadPage<Launchpad>()
                .ClickExperienceEditorLink();
        }

        /// <summary>
        ///     When I click the LaunchPad Experience Edior link.
        /// </summary>
        [When(@"I click the Content Editor link on the LaunchPad"), UsedImplicitly]
        public void ClickLaunchPadContentEditorLink()
        {
            Context.LoadPage<Launchpad>()
                .ClickContentEditorLink();
        }

        /// <summary>
        ///     Verifies the Experience Editor page was loaded.
        /// </summary>
        [Then(@"I should be on the Experience Editor page"), UsedImplicitly]
        public void VerifyExperienceEditorPage()
        {
            Context.VerifyPage<ExperienceEditor>()
                .TakeScreenshot("sitecore-experience-editor.jpg", ImageFormat.Jpeg);
        }

        /// <summary>
        ///     When I click the LaunchPad Experience Edior link.
        /// </summary>
        [When(@"I navigate to the LaunchPad"), UsedImplicitly]
        public void NavigateToLaunchPad()
        {
            Context.GoToPage<Launchpad>();
        }

        /// <summary>
        ///     Verifies the Experience Editor page was loaded.
        /// </summary>
        [Then(@"I should be on the Content Editor page"), UsedImplicitly]
        public void VerifyContentEditorPage()
        {
            Context.VerifyPage<ContentEditor>()
                .TakeScreenshot("sitecore-content-editor.jpg", ImageFormat.Jpeg);
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Imaging;
using JetBrains.Annotations;
using SymDemo.Site.Tests.Functional.Factories;
using SymDemo.Site.Tests.Functional.Pages;
using SymDemo.Site.Tests.Functional.Settings;
using TechTalk.SpecFlow;

namespace SymDemo.Site.Tests.Functional.Steps
{
    [Binding]
    public class BrowseSiteSteps
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
            webDriver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 2, 30));
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
        
        [When(@"I go to the home page"), UsedImplicitly]
        public void WhenIGoToTheHomePage()
        {
            Context.GoToPage<LandingPage>();
        }
        
        [Then(@"I can see the scroll"), UsedImplicitly]
        public void ThenTheResultShouldBeOnTheScreen()
        {
            Context.VerifyPage<LandingPage>()
                .WaitFor(new TimeSpan(0, 0, 3))
                .TakeScreenshot("Into.jpg", ImageFormat.Jpeg)
                .WaitFor(new TimeSpan(0, 0, 6))
                .TakeScreenshot("Logo.jpg", ImageFormat.Jpeg)
                .WaitFor(new TimeSpan(0, 0, 20))
                .TakeScreenshot("Scroll.jpg", ImageFormat.Jpeg);
        }
    }
}

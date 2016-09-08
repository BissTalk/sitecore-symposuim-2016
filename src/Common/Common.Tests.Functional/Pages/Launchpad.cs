using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SymDemo.Common.Tests.Functional.Pages
{
    /// <summary>
    ///     The Sitecore LaunchPad Page.
    /// </summary>
    [UsedImplicitly]
    public class Launchpad : BasePage<Launchpad>
    {
        /// <summary>
        ///     The content editor link
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "a[title='Content Editor']")]
        [CacheLookup]
        private IWebElement contentEditorLink;

        /// <summary>
        ///     The control panel link
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "a[title='Control Panel']")]
        [CacheLookup]
        private IWebElement controlPanelLink;

        /// <summary>
        ///     The desktop link
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "a[title='Desktop']")]
        [CacheLookup]
        private IWebElement desktopLink;

        /// <summary>
        ///     The experience analytics link
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "a[title='Experience Analytics']")]
        [CacheLookup]
        private IWebElement experienceAnalyticsLink;

        /// <summary>
        ///     The experience editor link
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "a[title='Experience Editor']")]
        [CacheLookup]
        private IWebElement experienceEditorLink;

        /// <summary>
        ///     The logout link
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "a.logout.data-sc-registered")]
        [CacheLookup]
        private IWebElement logoutLink;

        /// <summary>
        ///     The workbox
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "a[title='Workbox']")]
        [CacheLookup]
        private IWebElement workbox;

        [UsedImplicitly]
        public Launchpad([NotNull] IWebDriver driver)
            : base(driver, new Dictionary<string, string>(), 15)
        {
        }

        /// <summary>
        ///     Gets the relative URL of the page.
        /// </summary>
        /// <value>
        ///     The relative URL path.
        /// </value>
        public override string RelativePath => "/sitecore/client/Applications/Launchpad";

        /// <summary>
        ///     Gets the page verification text.
        /// </summary>
        /// <value>
        ///     The page verification text.
        /// </value>
        protected override string PageVerificationText => "class=\"sc-launchpad\"";

        /// <summary>
        ///     Click on Content Editor Link.
        /// </summary>
        /// <returns>The Launchpad class instance.</returns>
        [NotNull]
        public Launchpad ClickContentEditorLink()
        {
            contentEditorLink.Click(true);
            return this;
        }

        /// <summary>
        ///     Click on Control Panel Link.
        /// </summary>
        /// <returns>The Launchpad class instance.</returns>
        [NotNull]
        public Launchpad ClickControlPanelLink()
        {
            controlPanelLink.Click(true);
            return this;
        }

        /// <summary>
        ///     Click on Desktop Link.
        /// </summary>
        /// <returns>The Launchpad class instance.</returns>
        [NotNull]
        public Launchpad ClickDesktopLink()
        {
            desktopLink.Click(true);
            return this;
        }

        /// <summary>
        ///     Click on Experience Analytics Link.
        /// </summary>
        /// <returns>The Launchpad class instance.</returns>
        [NotNull]
        public Launchpad ClickExperienceAnalyticsLink()
        {
            experienceAnalyticsLink.Click(true);
            return this;
        }

        /// <summary>
        ///     Click on Experience Editor Link.
        /// </summary>
        /// <returns>The Launchpad class instance.</returns>
        [NotNull]
        public Launchpad ClickExperienceEditorLink()
        {
            experienceEditorLink.Click(true);
            return this;
        }

        /// <summary>
        ///     Click on Logout Link.
        /// </summary>
        /// <returns>The Launchpad class instance.</returns>
        [NotNull]
        public Launchpad ClickLogoutLink()
        {
            logoutLink.Click(true);
            return this;
        }

        /// <summary>
        ///     Click on Workbox Link.
        /// </summary>
        /// <returns>The Launchpad class instance.</returns>
        [NotNull]
        public Launchpad ClickWorkboxLink()
        {
            workbox.Click(true);
            return this;
        }
    }
}
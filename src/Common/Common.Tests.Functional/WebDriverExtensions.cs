using System;
using Bissol.SymDemo.Common.Tests.Functional.Pages;
using Bissol.SymDemo.Common.Tests.Functional.Settings;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Bissol.SymDemo.Common.Tests.Functional
{
    /// <summary>
    ///     Extension methods for Selenium's <see cref="IWebDriver" />.
    /// </summary>
    internal static class WebDriverExtensions
    {
        /// <summary>
        ///     Gets the application settings.
        /// </summary>
        /// <value>
        ///     The settings.
        /// </value>
        [NotNull]
        internal static ApplicationSettings Settings => ApplicationSettings.Default ?? new ApplicationSettings();

        /// <summary>
        ///     Navagates the web driver to the given URL.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="url">The URL.</param>
        /// <returns>The current web driver.</returns>
        /// <exception cref="ArgumentNullException">Neither parameter 'driver' nor 'url' can be null.</exception>
        /// <exception cref="ArgumentException">The 'url' parameter must start with a /</exception>
        [NotNull]
        public static IWebDriver GoTo([NotNull] this IWebDriver driver, [NotNull] string url)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (url == null) throw new ArgumentNullException(nameof(url));
            if (!url.StartsWith("/"))
                throw new ArgumentException("The 'url' prameter must start with a /");
            var baseUri = new Uri(Settings.SitecoreUrl);
            var uri = new Uri(baseUri, url);
            driver.Navigate()?.GoToUrl(uri);
            return driver;
        }

        /// <summary>
        ///     Goes to page.
        /// </summary>
        /// <typeparam name="T">The Type of page.</typeparam>
        /// <param name="driver">The driver.</param>
        /// <returns>The current web driver.</returns>
        /// <exception cref="ArgumentNullException">The parameter 'driver' cannot be null.</exception>
        [NotNull]
        public static T GoToPage<T>([NotNull] this IWebDriver driver) where T : BasePage<T>
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            var page = PageFactory.InitElements<T>(driver);
            // ReSharper disable once PossibleNullReferenceException
            return page.GoTo();
        }


        /// <summary>
        ///     Loads the page assuming it was already loaded.
        /// </summary>
        /// <typeparam name="T">The Type of page.</typeparam>
        /// <param name="driver">The driver.</param>
        /// <returns>The current web driver.</returns>
        /// <exception cref="ArgumentNullException">The parameter 'driver' cannot be null.</exception>
        [NotNull]
        public static T LoadPage<T>([NotNull] this IWebDriver driver) where T : BasePage<T>
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            // ReSharper disable once AssignNullToNotNullAttribute
            return PageFactory.InitElements<T>(driver);
        }

        /// <summary>
        ///     Verifies the page.
        /// </summary>
        /// <typeparam name="T">The Type of page.</typeparam>
        /// <param name="driver">The driver.</param>
        /// <returns>The current web driver.</returns>
        /// <exception cref="ArgumentNullException">The parameter 'driver' cannot be null.</exception>
        [NotNull]
        public static T VerifyPage<T>([NotNull] this IWebDriver driver) where T : BasePage<T>
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            return driver.LoadPage<T>()
                .VerifyPageUrl()
                .VerifyPageLoaded();
        }
    }
}
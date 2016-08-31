using System;
using Bissol.SymDemo.Common.Tests.Functional.Pages;
using JetBrains.Annotations;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Bissol.SymDemo.Common.Tests.Functional
{
    /// <summary>
    ///     Extension methods for <see cref="ScenarioContext" />.
    /// </summary>
    internal static class ScenarioContextExtensions
    {
        /// <summary>
        ///     The webdriver key
        /// </summary>
        private const string WEBDRIVER_KEY = "WEBDRIVER";

        /// <summary>
        ///     Retrieves the Selenuim web driver from the scenario context.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        /// <returns>the Selenuim web driver.</returns>
        /// <exception cref="ArgumentNullException"> </exception>
        [NotNull]
        public static IWebDriver WebDriver([NotNull] this ScenarioContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            IWebDriver result;
            if (!context.ContainsKey(WEBDRIVER_KEY)
                || (result = context[WEBDRIVER_KEY] as IWebDriver) == null)
                throw new NotFoundException("Web driver was never set or was set to null in this context.");
            return result;
        }

        /// <summary>
        ///     Sets the Selenuim web driver in the scenario context.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        /// <param name="driver">The web driver.</param>
        /// <exception cref="ArgumentNullException"> </exception>
        public static void SetWebDriver([NotNull] this ScenarioContext context, [CanBeNull] IWebDriver driver)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.ContainsKey(WEBDRIVER_KEY)) context.Remove(WEBDRIVER_KEY);
            if (driver != null) context.Add(WEBDRIVER_KEY, driver);
        }

        /// <summary>
        ///     Navagates the web driver to the given URL.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        /// <param name="url">The URL.</param>
        /// <returns>The current web driver.</returns>
        /// <exception cref="ArgumentNullException">The parameter 'context' cannot be null.</exception>
        [NotNull]
        public static IWebDriver GoTo([NotNull] this ScenarioContext context, [NotNull] string url)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            return context.WebDriver().GoTo(url);
        }

        /// <summary>
        ///     Goes to page.
        /// </summary>
        /// <typeparam name="T">The Type of page.</typeparam>
        /// <param name="context">The scenario context.</param>
        /// <returns>The current web driver.</returns>
        /// <exception cref="ArgumentNullException">The parameter 'context' cannot be null.</exception>
        [NotNull]
        public static T GoToPage<T>([NotNull] this ScenarioContext context) where T : BasePage<T>
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            return context.WebDriver().GoToPage<T>();
        }


        /// <summary>
        ///     Loads the page assuming it was already loaded.
        /// </summary>
        /// <typeparam name="T">The Type of page.</typeparam>
        /// <param name="context">The scenario context.</param>
        /// <returns>The current web driver.</returns>
        /// <exception cref="ArgumentNullException">The parameter 'context' cannot be null.</exception>
        [NotNull]
        public static T LoadPage<T>([NotNull] this ScenarioContext context) where T : BasePage<T>
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            return context.WebDriver().LoadPage<T>();
        }

        /// <summary>
        ///     Verifies the page.
        /// </summary>
        /// <typeparam name="T">The Type of page.</typeparam>
        /// <param name="context">The scenario context.</param>
        /// <returns>The current web driver.</returns>
        /// <exception cref="ArgumentNullException">The parameter 'context' cannot be null.</exception>
        [NotNull]
        public static T VerifyPage<T>([NotNull] this ScenarioContext context) where T : BasePage<T>
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            return context.WebDriver().VerifyPage<T>();
        }
    }
}
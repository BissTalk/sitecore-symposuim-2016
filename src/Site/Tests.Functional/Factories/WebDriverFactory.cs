using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using JetBrains.Annotations;

namespace SymDemo.Site.Tests.Functional.Factories
{
    /// <summary>
    ///     Static factory for creating instances of Selenium web drivers.
    /// </summary>
    public static class WebDriverFactory
    {
        /// <summary>
        ///     The Lookup the correct factory method by driver name.
        /// </summary>
        [NotNull]
        private static readonly IDictionary<string, Func<IWebDriver>> _funcs =
            new Dictionary<string, Func<IWebDriver>>
            {
                {"chrome", () => new ChromeDriver(new ChromeOptions {LeaveBrowserRunning = false})},
                {"phantomjs", () => new PhantomJSDriver()},
                {"phantom", () => new PhantomJSDriver()}
            };

        /// <summary>
        ///     Creates an instance of the Selenium web driver.
        /// </summary>
        /// <returns>The Selenium web driver.</returns>
        [NotNull]
        public static IWebDriver Create([NotNull] string driverName)
        {
            if (driverName == null) throw new ArgumentNullException(nameof(driverName));
            driverName = driverName.ToLower();
            if (!_funcs.ContainsKey(driverName))
                throw new NotSupportedException($"The driver '{driverName}' is not implemented.");
            // ReSharper disable once PossibleNullReferenceException
            // ReSharper disable once AssignNullToNotNullAttribute
            return _funcs[driverName]();
        }
    }
}
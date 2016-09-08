using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using JetBrains.Annotations;

namespace SymDemo.Site.Tests.Functional.Pages
{
    public class LandingPage : BasePage<LandingPage>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePage{T}" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public LandingPage([NotNull] IWebDriver driver)
            : base(driver)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePage{T}" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="data">The data.</param>
        /// <param name="timeout">The timeout.</param>
        public LandingPage([NotNull] IWebDriver driver, [NotNull] Dictionary<string, string> data,
            int timeout = DefaultTimeout)
            : base(driver, data, timeout)
        {
        }

        /// <summary>
        ///     Gets the relative URL of the page.
        /// </summary>
        /// <value>
        ///     The relative URL path.
        /// </value>
        public override string RelativePath => "/";

        /// <summary>
        /// Waits for the given amount of time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>The Landing Page</returns>
        [NotNull]
        public LandingPage WaitFor(TimeSpan time)
        {
            Thread.Sleep(time);
            return this;
        }
    }
}
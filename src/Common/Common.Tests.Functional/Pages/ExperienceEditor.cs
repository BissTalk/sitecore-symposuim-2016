using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SymDemo.Common.Tests.Functional.Pages
{
    /// <summary>
    ///     Sitecore Experience Editor Page (Formally know as Page Editor)
    /// </summary>
    /// <seealso cref="BasePage{T}" />
    [UsedImplicitly]
    public class ExperienceEditor : BasePage<ExperienceEditor>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePage{T}" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public ExperienceEditor([NotNull] IWebDriver driver)
            : base(driver)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePage{T}" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="data">The data.</param>
        /// <param name="timeout">The timeout.</param>
        public ExperienceEditor([NotNull] IWebDriver driver, [NotNull] Dictionary<string, string> data, int timeout = DefaultTimeout)
            : base(driver, data, timeout)
        {
        }

        /// <summary>
        ///     Gets the relative URL of the page.
        /// </summary>
        /// <value>
        ///     The relative URL path.
        /// </value>
        public override string RelativePath => "/?sc_mode=edit";

        /// <summary>
        ///     Verify that current page URL matches the expected URL.
        /// </summary>
        /// <returns>This instance.</returns>
        [NotNull]
        public override ExperienceEditor VerifyPageUrl()
        {
            new WebDriverWait(WebDriver, TimeSpan.FromMinutes(Timeout)).Until(d => d.Url.Contains("?sc_mode=edit"));
            return this;
        }

        /// <summary>
        ///     Verify that the page loaded completely.
        /// </summary>
        /// <returns>This instance.</returns>
        public override ExperienceEditor VerifyPageLoaded()
        {
            new WebDriverWait(WebDriver, TimeSpan.FromMinutes(Timeout)).Until(
                d =>
                {
                    IWebElement el = null;
                    var t = d?.PageSource != null;
                    t = t && d.PageSource.Contains(PageVerificationText);
                    t = t && (el = d.FindElement(By.Id("ribbonPreLoadingIndicator"))) != null;
                    t = t && el.GetCssValue("display") == "none";
                    return t;
                });
            return this;
        }
    }
}
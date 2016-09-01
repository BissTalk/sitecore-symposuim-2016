using System.Collections.Generic;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Bissol.SymDemo.Common.Tests.Functional.Pages
{
    /// <summary>
    ///     The Sitecore Content Editor Page.
    /// </summary>
    /// <seealso
    ///     cref="Bissol.SymDemo.Common.Tests.Functional.Pages.BasePage{Bissol.SymDemo.Common.Tests.Functional.Pages.ContentEditor}" />
    [UsedImplicitly]
    public class ContentEditor : BasePage<ContentEditor>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePage{T}" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public ContentEditor([NotNull] IWebDriver driver)
            : base(driver)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePage{T}" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="data">The data.</param>
        /// <param name="timeout">The timeout.</param>
        public ContentEditor([NotNull] IWebDriver driver, [NotNull] Dictionary<string, string> data, int timeout = 15)
            : base(driver, data, timeout)
        {
        }

        /// <summary>
        ///     Gets the relative URL of the page.
        /// </summary>
        /// <value>
        ///     The relative URL path.
        /// </value>
        public override string RelativePath => "/sitecore/shell/Applications/Content%20Editor.aspx";

        /// <summary>
        ///     Gets the page verification text.
        /// </summary>
        /// <value>
        ///     The page verification text.
        /// </value>
        protected override string PageVerificationText => "id=\"ContentTreeInnerPanel\"";
    }
}
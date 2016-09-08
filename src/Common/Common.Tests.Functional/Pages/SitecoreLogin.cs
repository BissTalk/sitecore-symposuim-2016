using System.Collections.Generic;

using JetBrains.Annotations;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

#pragma warning disable 649

namespace SymDemo.Common.Tests.Functional.Pages
{
    /// <summary>
    ///     Object representation of the Sitecore login page.
    /// </summary>
    /// <seealso
    ///     cref="BasePage{T}" />
    public class SitecoreLogin : BasePage<SitecoreLogin>
    {
        /// <summary>
        ///     The log in button.
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "input.btn-primary")]
        [CacheLookup]
        private IWebElement _logIn;

        /// <summary>
        ///     The password input box
        /// </summary>
        [FindsBy(How = How.Id, Using = "Password")]
        [CacheLookup]
        private IWebElement _password;

        /// <summary>
        ///     The user name input box.
        /// </summary>
        [FindsBy(How = How.Id, Using = "UserName")]
        [CacheLookup]
        private IWebElement _userName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SitecoreLogin" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        [UsedImplicitly]
        public SitecoreLogin([NotNull] IWebDriver driver)
            : base(driver, new Dictionary<string, string>(), 3)
        {
        }

        /// <summary>
        ///     Gets the relative URL of the page.
        /// </summary>
        /// <value>
        ///     The relative URL path.
        /// </value>
        public override string RelativePath => "/sitecore/login";

        /// <summary>
        ///     Click on Log In Button.
        /// </summary>
        /// <returns>The SitecoreLogin class instance.</returns>
        [NotNull]
        public SitecoreLogin ClickLogInButton()
        {

            _logIn.Click(true);
            return this;
        }

        /// <summary>
        ///     Set value to Remember Me Password field.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>
        ///     The SitecoreLogin class instance.
        /// </returns>
        [NotNull]
        public SitecoreLogin SetPasswordField(string password)
        {
            _password.SendKeys(password);
            return this;
        }

        /// <summary>
        ///     Set value to Remember Me Password field.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        ///     The SitecoreLogin class instance.
        /// </returns>
        [NotNull]
        public SitecoreLogin SetUserNameField(string userName)
        {
            _userName?.SendKeys(userName);
            return this;
        }

    }
}
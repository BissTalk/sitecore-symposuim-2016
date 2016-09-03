using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace SymDemo.Common.Tests.Functional.Pages
{
    /// <summary>
    ///     The base page.
    /// </summary>
    /// <typeparam name="T">The type of the page</typeparam>
    public abstract class BasePage<T>
        where T : BasePage<T>
    {
        /// <summary>
        ///     The default test results folder
        /// </summary>
        private const string DEFAULT_TEST_RESULTS_FOLDER = "TestResults";

        /// <summary>
        ///     The default timeout.
        /// </summary>
        private const int DEFAULT_TIMEOUT = 30;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePage{T}" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        protected BasePage([NotNull] IWebDriver driver)
            : this(driver, new Dictionary<string, string>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePage{T}" /> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="data">The data.</param>
        /// <param name="timeout">The timeout.</param>
        protected BasePage([NotNull] IWebDriver driver, [NotNull] Dictionary<string, string> data,
            int timeout = DEFAULT_TIMEOUT)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (data == null) throw new ArgumentNullException(nameof(data));
            WebDriver = driver;
            PageData = data;
            Timeout = timeout;
        }

        /// <summary>
        ///     The page data.
        /// </summary>
        [UsedImplicitly, NotNull]
        public Dictionary<string, string> PageData { get; private set; }

        /// <summary>
        ///     Gets the relative URL of the page.
        /// </summary>
        /// <value>
        ///     The relative URL path.
        /// </value>
        [UsedImplicitly, NotNull]
        public abstract string RelativePath { get; }

        /// <summary>
        ///     The page timeout
        /// </summary>
        [UsedImplicitly]
        public int Timeout { get; private set; }

        /// <summary>
        ///     The selenium web driver
        /// </summary>
        [UsedImplicitly, NotNull]
        public IWebDriver WebDriver { get; private set; }

        /// <summary>
        ///     Gets the page verification text.
        /// </summary>
        /// <value>
        ///     The page verification text.
        /// </value>
        [NotNull]
        protected virtual string PageVerificationText { get; } = string.Empty;

        /// <summary>
        ///     Takes the screenshot.
        /// </summary>
        /// <returns>The screenshot,</returns>
        [NotNull]
        public virtual Screenshot TakeScreenshot()
        {
            return WebDriver.TakeScreenshot();
        }

        /// <summary>
        ///     Takes the screenshot.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        [NotNull]
        public T TakeScreenshot([NotNull] string fileName, ImageFormat format)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            try
            {
                var folderName = WebDriverExtensions.Settings.TestResultsFolder ?? DEFAULT_TEST_RESULTS_FOLDER;
                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);
                TakeScreenshot().SaveAsFile($"{folderName}\\{fileName}", format);
            }
            catch (ExternalException)
            {
                Console.WriteLine("WARNING: Failed to capture screenshot.");
            }
            return (T) this;

        }

        /// <summary>
        ///     Verify that the page loaded completely.
        /// </summary>
        /// <returns>This instance.</returns>
        [NotNull]
        public virtual T VerifyPageLoaded()
        {
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(Timeout)).Until(
                d => d?.PageSource != null && d.PageSource.Contains(PageVerificationText));
            PageFactory.InitElements(WebDriver, this);
            return (T) this;
        }

        /// <summary>
        ///     Verify that current page URL matches the expected URL.
        /// </summary>
        /// <returns>This instance.</returns>
        [NotNull]
        public virtual T VerifyPageUrl()
        {
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(Timeout)).Until(d => d.Url.Contains(RelativePath));
            return (T) this;
        }

        /// <summary>
        ///     Goes to the page URL.
        /// </summary>
        /// <returns>This instance.</returns>
        [NotNull]
        public virtual T GoTo()
        {
            WebDriver.GoTo(RelativePath)
                .VerifyPage<T>();
            return (T) this;
        }

        /// <summary>
        ///     Goes to the page.
        /// </summary>
        /// <typeparam name="TPage">The type of the Page.</typeparam>
        /// <returns>
        ///     This the new page.
        /// </returns>
        [NotNull]
        public virtual TPage GoToPage<TPage>() where TPage : BasePage<TPage>
        {
            return WebDriver.GoToPage<TPage>();
        }

        /// <summary>
        ///     Verifies the page was loaded.
        /// </summary>
        /// <typeparam name="TPage">The type of the Page.</typeparam>
        /// <returns>
        ///     This the new page.
        /// </returns>
        [NotNull]
        public virtual TPage VerifyPage<TPage>() where TPage : BasePage<TPage>
        {
            return WebDriver.VerifyPage<TPage>();
        }
    }
}
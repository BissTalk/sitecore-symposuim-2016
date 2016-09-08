using System;
using System.Reflection;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace SymDemo.Common.Tests.Functional
{
    /// <summary>
    ///     Extension methods for <see cref="IWebElement" />s.
    /// </summary>
    public static class WebElementExtensions
    {
        /// <summary>
        ///     Clicks the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="safe">
        ///     if set to <c>true</c> eat the <see cref="TargetInvocationException" /> that is thrown after 60
        ///     seconds.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Click([NotNull] this IWebElement element, bool safe)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            try
            {
                element.Click();
            }
            catch (TargetInvocationException)
            {
                if (!safe)
                    throw;
            }
        }
    }
}
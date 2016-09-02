using System;

namespace SymDemo.UnityContainerManager
{
    /// <summary>
    ///     Internal Extention methods.
    /// </summary>
    internal static class ExtensionMethods
    {
        /// <summary>
        ///     Throws the exception if null.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="argName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowExceptionIfNull(this object arg, string argName)
        {
            if (arg == null) throw new ArgumentNullException(argName);
        }
    }
}
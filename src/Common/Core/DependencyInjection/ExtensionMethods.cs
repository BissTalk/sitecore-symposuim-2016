using System;
using SymDemo.UnityContainerManager;
using Microsoft.Practices.Unity;
using Sitecore;

namespace SymDemo.DependencyInjection
{
    /// <summary>
    ///     Parameter input check;
    /// </summary>
    internal static class ExtensionMethods
    {
        /// <summary>
        ///     Checks argument for null.
        /// </summary>
        /// <param name="obj">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NullCheck([NotNull] this object obj, [NotNull] string argumentName)
        {
            if (obj == null) throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        ///     Gets the area container.
        /// </summary>
        /// <param name="containerManager">The container manager.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The correct unity container for the Area, otherwise default container.</returns>
        [NotNull]
        public static IUnityContainer GetAreaContainer(this IUnityContainerManager containerManager, Type serviceType)
        {
            serviceType.NullCheck("serviceType");
            containerManager.NullCheck("containerManager");
            string areaName;
            IUnityContainer container;
            if (AreaResolver.TryResolveArea(serviceType, out areaName) &&
                (container = containerManager.Get(areaName)) != null)
            {
                return container;
            }
            return containerManager.Get();
        }

        /// <summary>
        ///     Gets the area container.
        /// </summary>
        /// <param name="containerManager">The container manager.</param>
        /// <param name="areaName">Name of the area.</param>
        /// <returns>The correct unity container for the Area, otherwise default container.</returns>
        [NotNull]
        public static IUnityContainer GetAreaContainer(this IUnityContainerManager containerManager, string areaName)
        {
            areaName.NullCheck("areaName");
            containerManager.NullCheck("containerManager");
            return containerManager.Get(areaName) ?? containerManager.Get();
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Linq;
using Sitecore;

namespace SymDemo.DependencyInjection
{
    /// <summary>
    ///     Figures out the MVC area name given an MVC controller.
    /// </summary>
    public static class AreaResolver
    {
        /// <summary>
        ///     The cache
        /// </summary>
        [NotNull]
        private static readonly ConcurrentDictionary<Type, string> _cache = new ConcurrentDictionary<Type, string>();

        /// <summary>
        ///     Resolves the area.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>Name of the MVC Area if it can be determined, otherwise <c>System.String.Empty</c></returns>
        [CanBeNull]
        public static string ResolveArea([NotNull] Type controllerType)
        {
            controllerType.NullCheck("controllerType");
            return _cache.GetOrAdd(
                controllerType,
                type =>
                    {
                        if (string.IsNullOrEmpty(controllerType.Namespace)) return string.Empty;
                        var parts = controllerType.Namespace.Split('.').ToList();
                        var areaIndex = parts.IndexOf("Areas");

                        return areaIndex > -1 && areaIndex < parts.Count - 1 ? parts[areaIndex + 1] : string.Empty;
                    });
        }

        /// <summary>
        ///     Tries the resolve area.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if the area was resolved, otherwise <c>false</c>.</returns>
        public static bool TryResolveArea([NotNull] Type controllerType, out string result)
        {
            controllerType.NullCheck("controllerType");
            result = ResolveArea(controllerType);
            return result.Length > 0;
        }
    }
}
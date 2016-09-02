using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using SymDemo.UnityContainerManager;
using Microsoft.Practices.Unity;
using Sitecore;
using Sitecore.Diagnostics;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace SymDemo.DependencyInjection
{
    /// <summary>
    ///     Multi-tenant enabled Unity dependency resolver.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.IDependencyResolver" />
    /// <seealso cref="System.Web.Http.Dependencies.IDependencyResolver" />
    public class DependencyResolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        /// <summary>
        ///     Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>
        ///     The requested service or object.
        /// </returns>
        public object GetController([NotNull] Type serviceType)
        {
            serviceType.NullCheck(nameof(serviceType));
            var containerManager = UnityContainerManager.UnityContainerManager.Instance;
            var container = containerManager.GetAreaContainer(serviceType);
            return SafeExecuteFunction(containerManager, () => container.Resolve(serviceType));
        }
        /// <summary>
        ///     Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>
        ///     The requested service or object.
        /// </returns>
        public object GetService([NotNull] Type serviceType)
        {
            serviceType.NullCheck(nameof(serviceType));
            var containerManager = UnityContainerManager.UnityContainerManager.Instance;
            var container = containerManager.GetAreaContainer(serviceType);
            if ((serviceType.IsInterface || serviceType.IsAbstract) && !container.IsRegistered(serviceType)) return null;
            return SafeExecuteFunction(containerManager, () => container.Resolve(serviceType));
        }

        /// <summary>
        ///     Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>
        ///     The requested services.
        /// </returns>
        public IEnumerable<object> GetServices([NotNull] Type serviceType)
        {
            serviceType.NullCheck(nameof(serviceType));
            var containerManager = UnityContainerManager.UnityContainerManager.Instance;
            var container = containerManager.GetAreaContainer(serviceType);
            return SafeExecuteFunction(containerManager, () => container.ResolveAll(serviceType));
        }

        /// <summary>
        ///     Starts a resolution scope.
        /// </summary>
        /// <returns>
        ///     The dependency scope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            return this;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // There is no cleanup needed here.
        }

        /// <summary>
        ///     Safes the execute function.
        /// </summary>
        /// <typeparam name="T">Type of object to be returned.</typeparam>
        /// <param name="containerManager">The container manager.</param>
        /// <param name="func">The function.</param>
        /// <returns>The result of the function, otherwise null (or default).</returns>
        private static T SafeExecuteFunction<T>([NotNull] IUnityContainerManager containerManager,
            [NotNull] Func<T> func)
        {
            containerManager.NullCheck(nameof(containerManager));
            func.NullCheck(nameof(func));

            try
            {
                return func();
            }
            catch (Exception e)
            {
                Log.Error((e.InnerException ?? e).Message, e, typeof (DependencyResolver));
            }
            return default(T);
        }
    }
}
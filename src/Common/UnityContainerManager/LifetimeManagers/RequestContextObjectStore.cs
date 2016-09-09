using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using Microsoft.Practices.ObjectBuilder2;

namespace SymDemo.UnityContainerManager.LifetimeManagers
{ 
    /// <summary>
    ///     Request specific object registry/store.
    /// </summary>
    /// <seealso cref="System.Collections.Concurrent.ConcurrentDictionary{Guid,Object}" />
    /// <seealso cref="System.IDisposable" />
    internal class RequestContextObjectStore : ConcurrentDictionary<Guid, object>, IDisposable
    {
        /// <summary>
        ///     The key for the item collection of the <see cref="HttpContext" />.
        /// </summary>
        private const string KEY = "DependencyInjection.Unity.LifetimeManagers.RequestContextObjectStore";

        /// <summary>
        ///     Prevents a default instance of the <see cref="RequestContextObjectStore" /> class from being created.
        /// </summary>
        private RequestContextObjectStore()
        {
        }

        /// <summary>
        ///     Gets the current context object store.
        /// </summary>
        /// <value>
        ///     The current context object store.
        /// </value>
        [NotNull]
        public static RequestContextObjectStore Current
        {
            [NotNull]
            get
            {
                var context = HttpContext.Current;
                if (context == null) return new RequestContextObjectStore();
                context.Items[KEY] = context.Items[KEY] as RequestContextObjectStore ??
                                     new RequestContextObjectStore();
                return (RequestContextObjectStore)context.Items[KEY];
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Values.Select(i => i as IDisposable).Where(i => i != null).ForEach(d => d?.Dispose());
        }
    }
}
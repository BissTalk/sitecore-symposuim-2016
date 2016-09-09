using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using Microsoft.Practices.ObjectBuilder2;

namespace SymDemo.UnityContainerManager.LifetimeManagers
{
    /// <summary>
    ///     Session specific object registry/store.
    /// </summary>
    /// <seealso cref="System.Collections.Concurrent.ConcurrentDictionary{Guid,Object}" />
    /// <seealso cref="System.IDisposable" />
    internal class SessionObjectStore : ConcurrentDictionary<Guid, object>, IDisposable
    {
        /// <summary>
        ///     The key for the item collection of the <see cref="HttpContext" />.
        /// </summary>
        private const string KEY = "DependencyInjection.Unity.LifetimeManagers.RequestContextObjectStore";

        /// <summary>
        ///     Prevents a default instance of the <see cref="SessionObjectStore" /> class from being created.
        /// </summary>
        private SessionObjectStore()
        {
        }

        /// <summary>
        ///     Gets the current session object store.
        /// </summary>
        /// <value>
        ///     The current session object store.
        /// </value>
        [NotNull]
        public static SessionObjectStore Current
        {
            [NotNull]
            get
            {
                var context = HttpContext.Current;
                if (context?.Session == null) return new SessionObjectStore();
                context.Session[KEY] = context.Session[KEY] as SessionObjectStore ??
                                       new SessionObjectStore();
                return (SessionObjectStore)context.Session[KEY];
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
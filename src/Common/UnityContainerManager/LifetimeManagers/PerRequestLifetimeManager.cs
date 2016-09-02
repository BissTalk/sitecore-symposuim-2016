using System;
using System.Data;
using Microsoft.Practices.Unity;

namespace SymDemo.UnityContainerManager.LifetimeManagers
{
    /// <summary>
    ///     Defines a <see cref="LifetimeManager" /> which returns the same object for a web request.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Unity.LifetimeManager" />
    public class PerRequestLifetimeManager : LifetimeManager
    {
        /// <summary>
        ///     The unique key for the instance.
        /// </summary>
        private readonly Guid _guid = Guid.NewGuid();

        /// <summary>
        ///     Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>
        ///     the object desired, or null if no such object is currently stored.
        /// </returns>
        public override object GetValue()
        {
            object result;
            if (RequestContextObjectStore.Current == null) throw new NoNullAllowedException("RequestContextObjectStore.Current cannot be null");
            return RequestContextObjectStore.Current.TryGetValue(_guid, out result)
                ? result
                : null;
        }

        /// <summary>
        ///     Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            if (RequestContextObjectStore.Current == null) throw new NoNullAllowedException("RequestContextObjectStore.Current cannot be null");
            RequestContextObjectStore.Current.AddOrUpdate(_guid, newValue, (k, v) => newValue);
        }

        /// <summary>
        ///     Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            object val;
            if (RequestContextObjectStore.Current == null) throw new NoNullAllowedException("RequestContextObjectStore.Current cannot be null");
            RequestContextObjectStore.Current.TryRemove(_guid, out val);
        }
    }
}
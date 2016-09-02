using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using SymDemo.UnityContainerManager.UnityExtensions;

namespace SymDemo.UnityContainerManager
{
    /// <summary>
    ///     A registry for 1 or more Unity containers.
    /// </summary>
    /// <seealso cref="IUnityContainerManager" />
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class UnityContainerManager : IUnityContainerManager
    {
        /// <summary>
        ///     The registry of containers.
        /// </summary>
        [NotNull]
        private static readonly Dictionary<string, IUnityContainer> _lookup = new Dictionary<string, IUnityContainer>();

        /// <summary>
        ///     The sole instance (Singleton) of a <see cref="UnityContainerManager" />.
        /// </summary>
        [NotNull]
        private static readonly Lazy<IUnityContainerManager> _soleInstance =
            new Lazy<IUnityContainerManager>(() => new UnityContainerManager());

        /// <summary>
        ///     Prevents a default instance of the <see cref="UnityContainerManager" /> class from being created.
        /// </summary>
        private UnityContainerManager()
        {
        }

        /// <summary>
        ///     Lazy Initialize Instance of UnityContainerManager
        /// </summary>
        [NotNull]
        public static IUnityContainerManager Instance => _soleInstance.Value;

        /// <summary>
        ///     Add to container by Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="container"></param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null.</exception>
        public void Add(string key, IUnityContainer container)
        {
            key.ThrowExceptionIfNull(nameof(key));
            container.ThrowExceptionIfNull(nameof(container));

            if (!_lookup.ContainsKey(key)) _lookup.Add(key, container);
            else _lookup[key] = container;
        }

        /// <summary>
        ///     Add to container by Key
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null.</exception>
        [NotNull]
        public IUnityContainer Add([NotNull] string key)
        {
            key.ThrowExceptionIfNull(nameof(key));

            if (_lookup.ContainsKey(key)) _lookup.Remove(key);
            _lookup.Add(key, Get().CreateChildContainer());
            return _lookup[key];
        }

        /// <summary>
        ///     Adds a new default container: Add("default", container).
        /// </summary>
        /// <param name="container">The default container.</param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null.</exception>
        public void Add([NotNull] IUnityContainer container)
        {
            container.ThrowExceptionIfNull(nameof(container));
            Add("default", container);
        }

        /// <summary>
        ///     Get Container by key
        /// </summary>
        /// <param name="key">The registry key for the container.</param>
        /// <returns>The named instance of a container.  (will create new if doesn't exist). c</returns>
        /// <exception cref="ArgumentNullException">Thrown if the key argument is null.</exception>
        public IUnityContainer Get(string key)
        {
            key.ThrowExceptionIfNull(nameof(key));
            if (!_lookup.ContainsKey(key)) _lookup.Add(key, CreateUnityContainer());
            return _lookup[key];
        }

        /// <summary>
        ///     Gets the default unity container: Get("default").
        /// </summary>
        /// <returns>The default container.</returns>
        [NotNull]
        public IUnityContainer Get()
        {
            return Get("default");
        }

        /// <summary>
        ///     Remove by registry key.
        /// </summary>
        /// <param name="key">The registry key for the container.</param>
        /// <exception cref="ArgumentNullException">Thrown if the key argument is null.</exception>
        public void Remove([NotNull]string key)
        {
            key.ThrowExceptionIfNull(nameof(key));
            if (!_lookup.ContainsKey(key)) return;
            _lookup[key].Dispose();
            _lookup.Remove(key);
        }

        /// <summary>
        ///     Removes All containers
        /// </summary>
        public void RemoveAll()
        {
            var keys = _lookup.Keys.ToArray();
            keys.ForEach(Remove);
        }

        /// <summary>
        ///     Creates a new unity container.
        /// </summary>
        /// <returns>A new unity container configured to allow for interception.</returns>
        [NotNull]
        private static IUnityContainer CreateUnityContainer()
        {
            return
                new UnityContainer().AddNewExtension<Interception>()
                    .AddNewExtension<InterceptionLifetimeManagerExtension>();
        }
    }
}
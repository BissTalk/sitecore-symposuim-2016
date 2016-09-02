using Microsoft.Practices.Unity;

namespace SymDemo.UnityContainerManager
{
    /// <summary>
    ///     A registry for 1 or more unity containers.
    /// </summary>
    public interface IUnityContainerManager
    {
        /// <summary>
        ///     Add Key, create a container 
        /// </summary>
        /// <param name="key"></param>
        [NotNull]
        IUnityContainer Add([NotNull] string key);

        /// <summary>
        ///     Add Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="container"></param>
        void Add([NotNull] string key, [NotNull] IUnityContainer container);

        /// <summary>
        ///     Add a container
        /// </summary>
        /// <param name="container"></param>
        void Add([NotNull] IUnityContainer container);

        /// <summary>
        ///     Get One Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IUnityContainer Get([NotNull] string key);

        /// <summary>
        ///     Get all
        /// </summary>
        /// <returns></returns>
        [NotNull]
        IUnityContainer Get();

        /// <summary>
        ///     Remove Key
        /// </summary>
        /// <param name="key"></param>
        void Remove([NotNull] string key);

        /// <summary>
        ///     Remove All
        /// </summary>
        void RemoveAll();
    }
}
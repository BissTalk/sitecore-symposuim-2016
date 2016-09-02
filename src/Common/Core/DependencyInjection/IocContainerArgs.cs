using Microsoft.Practices.Unity;
using Sitecore.Pipelines;

namespace SymDemo.DependencyInjection
{
    /// <summary>
    ///     Pipeline arguments containing the unity container that is getting bootstraped.
    /// </summary>
    /// <seealso cref="Sitecore.Pipelines.PipelineArgs" />
    public class IocContainerArgs : PipelineArgs
    {
        /// <summary>
        ///     Unity Container for the Pipeline to resolve
        /// </summary>
        /// <param name="container">The container getting configured.</param>
        public IocContainerArgs(IUnityContainer container)
        {
            Container = container;
        }

        /// <summary>
        ///     Property for IUnityContainer
        /// </summary>
        public IUnityContainer Container { get; private set; }
    }
}
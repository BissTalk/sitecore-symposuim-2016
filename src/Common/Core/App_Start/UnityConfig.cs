using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace SymDemo
{
    /// <summary>
    ///     Configures the Unity IoC for Dependency Injection.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Registers the components.
        /// </summary>
        /// <example>
        /// 
        /// container.RegisterType&lt;ITestService, TestService&gt;();
        /// 
        /// </example>
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
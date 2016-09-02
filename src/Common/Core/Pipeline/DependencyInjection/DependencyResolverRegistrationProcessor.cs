using System.Web.Http;
using Sitecore;
using Sitecore.Mvc.Controllers;
using Sitecore.Pipelines;
using SymDemo.DependencyInjection;
using ControllerBuilder = System.Web.Mvc.ControllerBuilder;

namespace SymDemo.Pipeline.DependencyInjection
{
    using MvcDependencyResolver = System.Web.Mvc.DependencyResolver;
    /// <summary>
    ///     Registers the MVC IDependecyResolver to enable multi-tenancy.
    /// </summary>
    public class DependencyResolverRegistrationProcessor
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process([CanBeNull] PipelineArgs args)
        {
            var dependencyResolver = new DependencyResolver();
            MvcDependencyResolver.SetResolver(dependencyResolver);
            if (ControllerBuilder.Current == null) return;
            ControllerBuilder.Current.SetControllerFactory(new SitecoreControllerFactory(new UnityControllerFactory(dependencyResolver)));
            if (GlobalConfiguration.Configuration == null) return;
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
        }
    }
}
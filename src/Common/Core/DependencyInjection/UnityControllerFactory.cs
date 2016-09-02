using System;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore;

namespace SymDemo.DependencyInjection
{
    /// <summary>
    /// Unity Controller factory.
    /// </summary>
    /// <seealso cref="DefaultControllerFactory" />
    public class UnityControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        ///     The dependency resolver.
        /// </summary>
        [NotNull] private readonly DependencyResolver _dependencyResolver;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnityControllerFactory" /> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public UnityControllerFactory([NotNull] DependencyResolver dependencyResolver)
        {
            dependencyResolver.NullCheck("dependencyResolver");
            _dependencyResolver = dependencyResolver;
        }

        /// <summary>
        ///     Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>
        ///     The controller instance.
        /// </returns>
        [CanBeNull]
        protected override IController GetControllerInstance([NotNull] RequestContext requestContext,
            [NotNull] Type controllerType)
        {
            requestContext.NullCheck("requestContext");
            controllerType.NullCheck("controllerType");
            return _dependencyResolver.GetController(controllerType) as IController
                   ?? base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
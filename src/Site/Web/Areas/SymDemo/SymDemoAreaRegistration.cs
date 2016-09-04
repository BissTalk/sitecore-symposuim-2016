using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Sitecore;

namespace SymDemo.Site.Web.Areas.SymDemo
{
    /// <summary>
    ///     Registers the Area in ASP.Net MVC and WebAPI and triggers the IoC bootstrapper.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AreaRegistration" />
    public class SymDemoAreaRegistration : AreaRegistration
    {
        /// <summary>
        ///     The area name
        /// </summary>
        [NotNull]
        private static string _areaName = "SymDemo";

        /// <summary>
        ///     <c>true</c> if area name his initialized, otherwise <c>false</c>.
        /// </summary>
        private static bool _isInitialized;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SymDemoAreaRegistration" /> class.
        /// </summary>
        public SymDemoAreaRegistration()
        {
            InitiallizeAreaName();
        }

        /// <summary>
        ///     Gets the name of the area to register.
        /// </summary>
        [NotNull]
        public override string AreaName => _areaName;

        /// <summary>
        ///     Gets the name of the area to register.
        /// </summary>
        [NotNull]
        public static string Area => _areaName;

        /// <summary>
        ///     Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea([NotNull] AreaRegistrationContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            RouteConfig.RegisterRoutes(context.Routes);
            UnitySetup.Initialize(_areaName);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        /// <summary>
        ///     Gets the name of the area.
        /// </summary>
        /// <returns>Area Name</returns>
        private static void InitiallizeAreaName()
        {
            if (_isInitialized) return;
            var ns = typeof (SymDemoAreaRegistration).Namespace;
            _areaName = ns?.Split('.').Last() ?? _areaName;
            _isInitialized = true;
        }
    }
}
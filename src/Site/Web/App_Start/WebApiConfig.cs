using System.Web.Http;
using SymDemo.Site.Web.Areas.SymDemo;

namespace SymDemo.Site.Web
{
    /// <summary>
    ///     Registraion bootstrrapper for WebAPI
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        ///     Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            var area = SymDemoAreaRegistration.Area;
        }
    }
}
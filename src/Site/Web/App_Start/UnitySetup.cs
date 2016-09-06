using System;
using System.Web;
using Glass.Mapper.Sc;
using Microsoft.Practices.Unity;
using Sitecore;
using SymDemo.UnityContainerManager.LifetimeManagers;
using unityManager = SymDemo.UnityContainerManager.UnityContainerManager;

namespace SymDemo.Site.Web
{
    /// <summary>
    ///     Unity IoC bootstrapper.
    /// </summary>
    public static class UnitySetup
    {
        /// <summary>
        ///     Initializes the specified container.
        /// </summary>
        /// <param name="containerKey">The container key.</param>
        public static void Initialize([NotNull]string containerKey)
        {
            if(containerKey == null) throw new ArgumentNullException(nameof(containerKey));
            

            // ReSharper disable PossibleNullReferenceException
            var container = unityManager.Instance.Get(containerKey);
            
            container.RegisterType<HttpContextBase>(new PerRequestLifetimeManager(),
                // ReSharper disable once AssignNullToNotNullAttribute
                new InjectionFactory(c => new HttpContextWrapper(HttpContext.Current)));
            container.RegisterType<ISitecoreContext>(new PerRequestLifetimeManager(),
                new InjectionFactory(c => new SitecoreContext(GlassMapperSc.GlassContextName)));
            container.RegisterType<ISitecoreService>(new PerRequestLifetimeManager(),
                new InjectionFactory(c => c.Resolve<ISitecoreContext>()));
            container.RegisterType<IGlassHtml, GlassHtml>();
            // ReSharper restore PossibleNullReferenceException

        }
    }
}
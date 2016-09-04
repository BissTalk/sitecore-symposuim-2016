using Glass.Mapper.Maps;
using Glass.Mapper.Sc.Configuration.Fluent;
using Glass.Mapper.Sc.IoC;
using Sitecore;
using Sitecore.Pipelines;
using Context = Glass.Mapper.Context;

namespace SymDemo.Common.Web
{
    /// <summary>
    ///     Glass Mapper for Sitecore setup.
    /// </summary>
    [UsedImplicitly]
    public class GlassMapperSc
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        [UsedImplicitly]
        public void Process([CanBeNull] PipelineArgs args)
        {
            Start();
        }

        /// <summary>
        ///     Starts this boottrapping process.
        /// </summary>
        [UsedImplicitly]
        public static void Start()
        {
            //install the custom services
            var resolver = GlassMapperScCustom.CreateResolver();

            //create a context
            var context = Context.Create(resolver);

            LoadConfigurationMaps(resolver, context);

            context.Load(
                GlassMapperScCustom.GlassLoaders()
                );

            GlassMapperScCustom.PostLoad();
        }

        /// <summary>
        ///     Loads the configuration maps.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <param name="context">The context.</param>
        [UsedImplicitly]
        public static void LoadConfigurationMaps(IDependencyResolver resolver, Context context)
        {
            var dependencyResolver = resolver as DependencyResolver;
            if (dependencyResolver == null)
                return;

            if (dependencyResolver.ConfigurationMapFactory is ConfigurationMapConfigFactory)
                GlassMapperScCustom.AddMaps(dependencyResolver.ConfigurationMapFactory);

            IConfigurationMap configurationMap = new ConfigurationMap(dependencyResolver);
            var configurationLoader = configurationMap.GetConfigurationLoader<SitecoreFluentConfigurationLoader>();
            context.Load(configurationLoader);
        }
    }
}
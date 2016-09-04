using System;
using Glass.Mapper.Configuration;
using Glass.Mapper.IoC;
using Glass.Mapper.Maps;
using Glass.Mapper.Sc.IoC;
using Sitecore;
using IDependencyResolver = Glass.Mapper.Sc.IoC.IDependencyResolver;

namespace SymDemo.Common.Web
{
    /// <summary>
    ///     Glass Mapper customizations.
    /// </summary>
    public static  class GlassMapperScCustom
    {
        /// <summary>
        ///     Creates the resolver.
        /// </summary>
        /// <returns>The dependency resolver.</returns>
        [NotNull]
        public static IDependencyResolver CreateResolver(){
			var config = new Glass.Mapper.Sc.Config();

			var dependencyResolver = new DependencyResolver(config);
			// add any changes to the standard resolver here
			return dependencyResolver;
		}

        /// <summary>
        ///     Get the Glass Mapper loaders.
        /// </summary>
        /// <returns>The Glass Mapper loaders.</returns>
        [NotNull]
        public static IConfigurationLoader[] GlassLoaders(){			
			
			/* USE THIS AREA TO ADD FLUENT CONFIGURATION LOADERS
             * 
             * If you are using Attribute Configuration or automapping/on-demand mapping you don't need to do anything!
             * 
             */

			return new IConfigurationLoader[]{};
		}

        /// <summary>
        ///     Post load setup for things like code first initialization.
        /// </summary>
        public static void PostLoad(){
			//Remove the comments to activate CodeFist
			/* CODE FIRST START
            var dbs = Sitecore.Configuration.Factory.GetDatabases();
            foreach (var db in dbs)
            {
                var provider = db.GetDataProviders().FirstOrDefault(x => x is GlassDataProvider) as GlassDataProvider;
                if (provider != null)
                {
                    using (new SecurityDisabler())
                    {
                        provider.Initialise(db);
                    }
                }
            }
             * CODE FIRST END
             */
		}

        /// <summary>
        ///     Adds the maps.
        /// </summary>
        /// <param name="mapsConfigFactory">The maps configuration factory.</param>
        public static void AddMaps([NotNull] IConfigFactory<IGlassMap> mapsConfigFactory)
        {
            if (mapsConfigFactory == null) throw new ArgumentNullException(nameof(mapsConfigFactory));
			// Add maps here
            // mapsConfigFactory.Add(() => new SeoMap());
        }
    }
}
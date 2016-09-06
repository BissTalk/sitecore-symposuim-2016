using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.Practices.ObjectBuilder2;
using Sitecore;

namespace SymDemo
{
    /// <summary>
    ///     Infrastructure HTML Helper Extensions for use in Razor View markup files.
    /// </summary>
    [UsedImplicitly]
    public static class HtmlHelperExtensions
    {
        /// <summary>
        ///     The key for HTTP Context item to hold resources.
        /// </summary>
        private const string KEY = "PAGE.RESOURCES";

        /// <summary>
        ///     Renders 'disabled' attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static HtmlString AttributeDisabled<T>([NotNull] this HtmlHelper<T> html, bool isDisabled)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));

            return new HtmlString(isDisabled ? "disabled" : null);
        }

        /// <summary>
        ///     Adds the script.
        /// </summary>
        /// <typeparam name="T">The Model Type.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="path">The js path.</param>
        [UsedImplicitly]
        public static HtmlString AddScript<T>([NotNull] this HtmlHelper<T> html, [NotNull] string path)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));
            if (path == null) throw new ArgumentNullException(nameof(path));

            html.GetResourceRegistry().AddScript(path);
            return new HtmlString(string.Empty);
        }

        /// <summary>
        ///     Adds the style to the rendering page.
        /// </summary>
        /// <typeparam name="T">The Model Type.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="path">The CSS path.</param>
        [UsedImplicitly]
        public static HtmlString AddStyle<T>([NotNull] this HtmlHelper<T> html, string path)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));

            html.GetResourceRegistry().AddStyle(path);
            return new HtmlString(string.Empty);
        }

        /// <summary>
        ///     Adds import link to the rendering page.
        /// </summary>
        /// <typeparam name="T">The Model Type.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="path">The import path.</param>
        [UsedImplicitly]
        public static HtmlString AddImport<T>([NotNull] this HtmlHelper<T> html, string path)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));

            html.GetResourceRegistry().AddImport(path);
            return new HtmlString(null);
        }

        /// <summary>
        ///     Writes the scripts.
        /// </summary>
        /// <typeparam name="T">The Model Type.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <returns>HTML Tags for scripts.</returns>
        [UsedImplicitly]
        public static HtmlString WriteScripts<T>([NotNull] this HtmlHelper<T> html)
        {
            if(html == null) throw new ArgumentNullException(nameof(html));

            return html.WriteResources(r => r?.Scripts, CreateJsTag);
        }

        /// <summary>
        ///     Writes the imported resources.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static HtmlString WriteImports<T>([NotNull] this HtmlHelper<T> html)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));

            return html.WriteResources(r => r?.Imports, CreateImportTag);
        }

        /// <summary>
        ///     Writes the styles.
        /// </summary>
        /// <typeparam name="T">The Model Type.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <returns>HTML Tags for styles.</returns>
        [UsedImplicitly]
        public static HtmlString WriteStyles<T>([NotNull] this HtmlHelper<T> html)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));

            return html.WriteResources(r => r?.Styles, CreateCssTag);
        }

        /// <summary>
        ///     Writes the resources.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="getPaths">The get paths.</param>
        /// <param name="buildTagForPath">The build tag from path.</param>
        /// <returns></returns>
        [UsedImplicitly]
        private static HtmlString WriteResources<T>([NotNull] this HtmlHelper<T> html,
            [NotNull] Func<ResourceRegistry, IEnumerable<string>> getPaths, [NotNull] Func<string, string> buildTagForPath)
        {
            if(html == null) throw new ArgumentNullException(nameof(html));
            if(getPaths == null) throw new ArgumentNullException(nameof(getPaths));
            if(buildTagForPath == null) throw new ArgumentNullException(nameof(buildTagForPath));
            if(html.ViewContext?.HttpContext?.Items == null) throw new InvalidOperationException("html.ViewContext.HttpContext.Items is not set");

            var registry = html.ViewContext.HttpContext.Items[KEY] as ResourceRegistry;
            if (registry == null) return new HtmlString(string.Empty);
            var sb = new StringBuilder();
            getPaths(registry).ForEach(path => sb.AppendLine(buildTagForPath(path)));
            return new HtmlString(sb.ToString());
        }

        private static string AddCacheBuster([NotNull]string rootRelativePath)
        {
            if (rootRelativePath == null) throw new ArgumentNullException(nameof(rootRelativePath));

            if (!rootRelativePath.StartsWith("/")
                || !(rootRelativePath.EndsWith(".css") || rootRelativePath.EndsWith(".js"))) return rootRelativePath;
            if (HttpRuntime.Cache != null && HttpRuntime.Cache[rootRelativePath] != null) return HttpRuntime.Cache[rootRelativePath] as string;
            var absolutePath = HostingEnvironment.MapPath(string.Concat("~", rootRelativePath));
            if (string.IsNullOrEmpty(absolutePath) || !File.Exists(absolutePath))
            {
                if (HttpRuntime.Cache != null)
                    HttpRuntime.Cache.Insert(rootRelativePath, rootRelativePath, null, DateTime.MaxValue, TimeSpan.Zero);
                return rootRelativePath;
            }
            var date = File.GetLastWriteTime(absolutePath);
            var result = string.Concat(rootRelativePath, "?cb=", date.Ticks);
            if (HttpRuntime.Cache == null) return result;
            HttpRuntime.Cache.Insert(rootRelativePath, result, new CacheDependency(absolutePath));
            return HttpRuntime.Cache[rootRelativePath] as string;
        }

        /// <summary>
        ///     Creates the CSS tag.
        /// </summary>
        /// <param name="path">The CSS path.</param>
        /// <returns>String containing the HTML Tag result.</returns>
        private static string CreateCssTag([NotNull] string path)
        {
            if(path == null) throw new ArgumentNullException(nameof(path));

            var builder = new TagBuilder("link");
            builder.Attributes?.Add("href", AddCacheBuster(path));
            builder.Attributes?.Add("rel", "stylesheet");
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        /// <summary>
        ///     Creates the script tag for referencing JavaScript.
        /// </summary>
        /// <param name="path">The js path.</param>
        /// <returns>String containing the HTML Tag result.</returns>
        private static string CreateJsTag([NotNull] string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            var builder = new TagBuilder("script");
            builder.Attributes?.Add("type", "text/javascript");
            builder.Attributes?.Add("src", AddCacheBuster(path));
            return builder.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        ///     Creates the import tag.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private static string CreateImportTag([NotNull] string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            var builder = new TagBuilder("link");
            builder.Attributes?.Add("href", path);
            builder.Attributes?.Add("rel", "import");
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        /// <summary>
        ///     Gets the resource registry.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        [NotNull]
        private static ResourceRegistry GetResourceRegistry([NotNull] this HtmlHelper html)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));
            if (html.ViewContext?.HttpContext?.Items == null) throw new InvalidOperationException("html.ViewContext.HttpContext.Items is not set");

            if (!html.ViewContext.HttpContext.Items.Contains(KEY))
                html.ViewContext.HttpContext.Items.Add(KEY, new ResourceRegistry());
            // ReSharper disable once AssignNullToNotNullAttribute
            return (ResourceRegistry) html.ViewContext.HttpContext.Items[KEY];
        }
    }
}
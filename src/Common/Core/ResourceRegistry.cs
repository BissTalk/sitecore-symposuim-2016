using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;

namespace SymDemo
{
    /// <summary>
    ///     Registry for we assets for a page such as JavaScript and CSS.
    /// </summary>
    public class ResourceRegistry
    {
        /// <summary>
        ///     The collection of import file paths for a page.
        /// </summary>
        [NotNull]
        private readonly HashSet<string> _imports = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        ///     The collection of JavaScript file paths for a page.
        /// </summary>
        [NotNull]
        private readonly HashSet<string> _scripts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        ///     The collection of CSS file paths for a page.
        /// </summary>
        [NotNull]
        private readonly HashSet<string> _styles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        ///     The path to the CDN.
        /// </summary>
        [NotNull]
        private string _cdnPath = string.Empty;

        /// <summary>
        ///     Gets the JavaScript file path collection.
        /// </summary>
        /// <value>
        ///     The JavaScript file path collection.
        /// </value>
        [NotNull]
        public IEnumerable<string> Scripts => _scripts.Select(CreateCdnUrl);

        /// <summary>
        ///     Gets the CSS file path collection.
        /// </summary>
        /// <value>
        ///     The CSS file path collection..
        /// </value>
        [NotNull]
        public IEnumerable<string> Styles => _styles.Select(CreateCdnUrl);

        /// <summary>
        ///     Gets the paths for imported resources.
        /// </summary>
        /// <value>
        ///     The imports.
        /// </value>
        [NotNull]
        public IEnumerable<string> Imports => _imports;

        /// <summary>
        ///     Creates the CDN URL.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns>CDN Url</returns>
        [NotNull]
        private string CreateCdnUrl([NotNull] string relativeUrl)
        {
            if(relativeUrl == null) throw new ArgumentNullException(nameof(relativeUrl));

            return string.Format(relativeUrl, _cdnPath);
        }

        /// <summary>
        ///     Adds a CSS path to the collection of styles.
        /// </summary>
        /// <param name="cssPath">The CSS path.</param>
        public void AddStyle(string cssPath)
        {
            _styles.Add(ToRegistryFormat(cssPath));
        }

        /// <summary>
        ///     Adds a JavaScript path to the collection of styles.
        /// </summary>
        /// <param name="path">The JavaScript path.</param>
        public void AddScript(string path)
        {
            _scripts.Add(ToRegistryFormat(path));
        }

        /// <summary>
        ///     Adds a resource path to the collection of imported resources.
        /// </summary>
        /// <param name="path">The imported resource path.</param>
        public void AddImport(string path)
        {
            _imports.Add(path);
        }

        /// <summary>
        ///     Sets the CDN path.
        /// </summary>
        /// <param name="path">The cd path.</param>
        public void SetCdnPath([NotNull]string path)
        {
            if(path == null) throw new ArgumentNullException(nameof(path));

            _cdnPath = path.Trim().TrimEnd('/');
        }

        /// <summary>
        ///     To the internal string format for the registry.
        /// </summary>
        /// <param name="path">The CSS path.</param>
        /// <returns>The internal registry format.</returns>
        [NotNull]
        private static string ToRegistryFormat(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            if (!path.StartsWith("//") && !Uri.IsWellFormedUriString(path, UriKind.Absolute))
                path = string.Concat("{0}", StringUtil.EnsurePrefix('/', path));
            return path;
        }
    }
}
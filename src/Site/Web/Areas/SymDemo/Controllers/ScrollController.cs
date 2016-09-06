using System;
using System.Web;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore;
using SymDemo.Site.Models.Items;

namespace SymDemo.Site.Web.Areas.SymDemo.Controllers
{
    /// <summary>
    ///     Controller for the sroll feature(s).
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ScrollController : GlassController
    {
        private readonly HttpContextBase _httpContext;
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScrollController" /> class.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="sitecoreContext">The Sitecore context.</param>
        public ScrollController([NotNull] HttpContextBase httpContext, [NotNull] ISitecoreContext sitecoreContext):
            base(sitecoreContext)
        {
            if(httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if(sitecoreContext == null) throw new ArgumentNullException(nameof(sitecoreContext));

            _httpContext = httpContext;
        }

        /// <summary>
        ///     The default controller action.
        /// </summary>
        /// <returns>An MVC view</returns>
        public override ActionResult Index()
        {
            var d = DataSourceItem;
            var model = GetLayoutItem<ScrollSource>();
            return View(model);
        }
    }
}
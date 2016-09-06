using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace SymDemo.Common.Models.Items
{
    /// <summary>
    ///     An item representing a Page.
    /// </summary>
    /// <seealso cref="SymDemo.Common.Models.GlassBase" />
    /// <seealso cref="SymDemo.Common.Models.Items.IPage" />
    public partial class Page
    {
        [SitecoreField("__semantics")]
        public virtual IEnumerable<Guid> Tags { get; set; }
    }
}
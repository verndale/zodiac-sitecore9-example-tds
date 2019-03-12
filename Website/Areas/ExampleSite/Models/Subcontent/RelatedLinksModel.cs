using System.Collections.Generic;
using System.Web;
using Constellation.Feature.Navigation.Models;

namespace Website.Areas.ExampleSite.Models.Subcontent
{
	public class RelatedLinksModel
	{
		public RelatedLinksModel()
		{
			Links = new List<TargetItem>();
		}

		public HtmlString Heading { get; set; }

		public ICollection<TargetItem> Links { get; set; }
	}
}
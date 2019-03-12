using System.Web;
using Constellation.Foundation.ModelMapping.MappingAttributes;

namespace Website.Areas.ExampleSite.Models.ContextContent
{
	public class BioModel
	{
		public HtmlString FullName { get; set; }

		public HtmlString JobTitle { get; set; }

		public HtmlString Summary { get; set; }

		public HtmlString Copy { get; set; }

		[RenderAsUrl(false)]
		public string Portrait { get; set; }

		public string PortraitAlt { get; set; }
	}
}
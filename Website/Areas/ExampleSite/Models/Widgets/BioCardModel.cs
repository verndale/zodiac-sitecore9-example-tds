using System.Web;
using Constellation.Foundation.ModelMapping.MappingAttributes;

namespace Website.Areas.ExampleSite.Models.Widgets
{
	public class BioCardModel
	{
		public string Url { get; set; }

		public HtmlString FullName { get; set; }

		public HtmlString JobTitle { get; set; }

		public HtmlString Summary { get; set; }

		[RenderAsUrl(false)]
		public string Portrait { get; set; }
	}
}
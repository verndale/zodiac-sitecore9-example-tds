using System.Web;
using Constellation.Foundation.ModelMapping.MappingAttributes;

namespace Website.Areas.ExampleSite.Models.Widgets
{
	public class CallToActionModel
	{
		[FieldRendererParams("class=card-img-top")]
		public HtmlString Thumbnail { get; set; }

		public HtmlString Heading { get; set; }

		public HtmlString Summary { get; set; }

		[RenderAsUrl(true)]
		public HtmlString Link { get; set; }

		public string LinkTarget { get; set; }

		public string LinkText { get; set; }
	}
}
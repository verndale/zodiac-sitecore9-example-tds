using System.Web;
using Constellation.Foundation.ModelMapping.MappingAttributes;

namespace Website.Areas.ExampleSite.Models.Subcontent
{
	public class JumbotronModel
	{
		public HtmlString Heading { get; set; }

		public HtmlString Lead { get; set; }

		public HtmlString Copy { get; set; }

		[FieldRendererParams("class=btn%20btn-primary%20btn-lg")]
		public HtmlString ButtonLink { get; set; }

		[RenderAsUrl(false)]
		public string BackgroundImage { get; set; }
	}
}
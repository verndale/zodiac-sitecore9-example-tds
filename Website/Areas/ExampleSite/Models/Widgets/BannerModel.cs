using System.Web;
using Constellation.Foundation.ModelMapping.MappingAttributes;

namespace Website.Areas.ExampleSite.Models.Widgets
{
	public class BannerModel
	{
		[FieldRendererParams("class=img-fluid")]
		public HtmlString Image { get; set; }

		public string ImageHeight { get; set; }

		public string ImageWidth { get; set; }

		public string ImageAlt { get; set; }
	}
}
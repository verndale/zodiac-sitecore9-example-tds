using System;
using System.Web;

namespace Website.Areas.ExampleSite.Models.ContextContent
{
	public class PressReleaseModel
	{
		public HtmlString Heading { get; set; }

		public HtmlString Summary { get; set; }

		public DateTime ReleaseDate { get; set; }

		public HtmlString Author { get; set; }

		public HtmlString Copy { get; set; }
	}
}
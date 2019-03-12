using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc;
using Website.Areas.ExampleSite.Models.Widgets;

namespace Website.Areas.ExampleSite.Controllers.Widgets
{
	public class CallToActionController : Constellation.Foundation.Mvc.Patterns.Controllers.DatasourceRenderingController<CallToActionModel>
	{
		public CallToActionController(IViewPathResolver viewPathResolver, IModelMapper modelMapper) : base(viewPathResolver, modelMapper)
		{
		}
	}
}
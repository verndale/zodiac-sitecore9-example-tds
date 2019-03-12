using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc;
using Website.Areas.ExampleSite.Models.ContextContent;

namespace Website.Areas.ExampleSite.Controllers.ContextContent
{
	public class PressReleaseController : Constellation.Foundation.Mvc.Patterns.Controllers.DatasourceRenderingController<PressReleaseModel>
	{
		public PressReleaseController(IViewPathResolver viewPathResolver, IModelMapper modelMapper) : base(viewPathResolver, modelMapper)
		{
		}
	}
}
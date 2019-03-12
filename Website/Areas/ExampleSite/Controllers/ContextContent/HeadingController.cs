using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc;
using Website.Areas.ExampleSite.Models.ContextContent;

namespace Website.Areas.ExampleSite.Controllers.ContextContent
{
	public class HeadingController : Constellation.Foundation.Mvc.Patterns.Controllers.DatasourceRenderingController<HeadingModel>
	{
		public HeadingController(IViewPathResolver viewPathResolver, IModelMapper modelMapper) : base(viewPathResolver, modelMapper)
		{
		}
	}
}
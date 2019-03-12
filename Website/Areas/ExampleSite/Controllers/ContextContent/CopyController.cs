using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc;
using Website.Areas.ExampleSite.Models.ContextContent;

namespace Website.Areas.ExampleSite.Controllers.ContextContent
{
	public class CopyController : Constellation.Foundation.Mvc.Patterns.Controllers.DatasourceRenderingController<CopyModel>
	{
		public CopyController(IViewPathResolver viewPathResolver, IModelMapper modelMapper) : base(viewPathResolver, modelMapper)
		{
		}
	}
}
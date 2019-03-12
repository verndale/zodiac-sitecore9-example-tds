using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc;
using Website.Areas.ExampleSite.Models.Subcontent;

namespace Website.Areas.ExampleSite.Controllers.Subcontent
{
	public class RelatedLinksController : Constellation.Foundation.Mvc.Patterns.Controllers.DatasourceRenderingController<RelatedLinksModel>
	{
		public RelatedLinksController(IViewPathResolver viewPathResolver, IModelMapper modelMapper) : base(viewPathResolver, modelMapper)
		{
		}
	}
}
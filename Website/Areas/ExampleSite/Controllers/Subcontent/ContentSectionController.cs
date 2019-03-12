using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc;
using Website.Areas.ExampleSite.Models.Subcontent;

namespace Website.Areas.ExampleSite.Controllers.Subcontent
{
	public class ContentSectionController : Constellation.Foundation.Mvc.Patterns.Controllers.DatasourceRenderingController<ContentSectionModel>
	{
		public ContentSectionController(IViewPathResolver viewPathResolver, IModelMapper modelMapper) : base(viewPathResolver, modelMapper)
		{
		}
	}
}
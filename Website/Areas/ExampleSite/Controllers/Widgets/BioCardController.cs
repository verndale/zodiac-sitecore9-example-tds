using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc;
using Website.Areas.ExampleSite.Models.Widgets;

namespace Website.Areas.ExampleSite.Controllers.Widgets
{
	public class BioCardController : Constellation.Foundation.Mvc.Patterns.Controllers.DatasourceRenderingController<BioCardModel>
	{
		public BioCardController(IViewPathResolver viewPathResolver, IModelMapper modelMapper) : base(viewPathResolver, modelMapper)
		{
		}
	}
}
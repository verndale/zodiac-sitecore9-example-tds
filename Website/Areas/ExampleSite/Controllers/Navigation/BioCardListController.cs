using Constellation.Foundation.Mvc;
using Website.Areas.ExampleSite.Models.Widgets;
using Website.Areas.ExampleSite.Repositories;

namespace Website.Areas.ExampleSite.Controllers.Navigation
{
	public class BioCardListController : Constellation.Foundation.Mvc.Patterns.Controllers.ItemListController<BioCardModel>
	{
		public BioCardListController(IViewPathResolver viewPathResolver, BioListRepository repository) : base(viewPathResolver, repository)
		{
		}
	}
}
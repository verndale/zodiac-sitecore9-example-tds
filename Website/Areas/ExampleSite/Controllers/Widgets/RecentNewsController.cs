using System.Linq;
using Constellation.Foundation.Mvc;
using Constellation.Foundation.Mvc.Patterns;
using Constellation.Foundation.Mvc.Patterns.Repositories;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Website.Areas.ExampleSite.Models.Widgets;

namespace Website.Areas.ExampleSite.Controllers.Widgets
{
	public class RecentNewsController : Constellation.Foundation.Mvc.Patterns.Controllers.ItemListController<PressReleaseCardModel>
	{
		public RecentNewsController(IViewPathResolver viewPathResolver, ItemListRepository<PressReleaseCardModel> repository) : base(viewPathResolver, repository)
		{
		}

		protected override object GetModel(Item datasource, Item contextItem)
		{
			var results = Repository.GetModel(RepositoryContext.FromRenderingContext(RenderingContext.Current));

			// We only need the top 3

			return results.Take(3);
		}
	}
}
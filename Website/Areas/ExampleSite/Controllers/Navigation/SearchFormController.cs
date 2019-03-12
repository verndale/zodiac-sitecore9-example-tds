using Constellation.Foundation.Data;
using Constellation.Foundation.Mvc;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Website.Areas.ExampleSite.Controllers.Navigation
{
	public class SearchFormController : ConventionController
	{
		public SearchFormController(IViewPathResolver viewPathResolver) : base(viewPathResolver)
		{
		}

		protected override object GetModel(Item datasource, Item contextItem)
		{
			var searchResultPage = "/";

			var searchResultsItem = RenderingContext.Current.ContextItem.Database.GetItem(RenderingContext.Current.Rendering.RenderingItem.DataSource);
			if (searchResultsItem != null)
			{
				searchResultPage = searchResultsItem.GetUrl();
			}

			return searchResultPage;
		}
	}
}
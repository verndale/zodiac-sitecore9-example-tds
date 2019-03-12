using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data.Items;
using Website.Areas.ExampleSite.Models.Navigation;
using X.PagedList;

namespace Website.Areas.ExampleSite.Repositories
{
	public class TextSearchRepository
	{
		public IPagedList<TextSearchResultModel> GetPage(string searchText, int pageNum, int pageSize, Item siteRoot)
		{
			var indexable = new SitecoreIndexableItem(siteRoot);

			ISearchIndex index = ContentSearchManager.GetIndex(indexable);
			using (var context = index.CreateSearchContext())
			{
				var slop = 0.0f;
				var headingBoost = 1.0f;
				var copyBoost = 0.9f;
				var summaryBoost = 0.8f;
				var descriptionBoost = 0.8f;
				var keywordsBoost = 0.7f;


				IQueryable<TextSearchResultModel> query = context.GetQueryable<TextSearchResultModel>();

				query = query
					.Filter(i => i.Language == siteRoot.Language.Name)
					.Filter(i => i.Paths.Contains(siteRoot.ID))
					.Filter(i => !i.Name.StartsWith("_"))
					.Where(i =>
						i.Heading.Like(searchText, slop).Boost(headingBoost) ||
						i.Copy.Like(searchText, slop).Boost(copyBoost) ||
						i.Summary.Like(searchText, slop).Boost(summaryBoost) ||
						i.Description.Like(searchText, slop).Boost(descriptionBoost) ||
						i.Keywords.Like(searchText, slop).Boost(keywordsBoost) ||
						i.AggregateText.Like(searchText, slop));

				return query.ToPagedList(pageNum, pageSize);
			}
		}
	}
}

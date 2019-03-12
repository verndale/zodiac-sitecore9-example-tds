using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Website.Areas.ExampleSite.Models.Widgets;
using X.PagedList;

namespace Website.Areas.ExampleSite.Repositories
{
	public class PaginatedPressReleaseListRepository
	{
		// ReSharper disable once InconsistentNaming
		protected static ID PressReleaseID = new ID("{0A911D86-82DB-42E8-8F7B-3B75EB47F5D5}");

		public IPagedList<PressReleaseCardModel> GetPage(int pageNum, int pageSize, Item listRoot)
		{
			var indexable = new SitecoreIndexableItem(listRoot);

			ISearchIndex index = ContentSearchManager.GetIndex(indexable);
			using (var context = index.CreateSearchContext())
			{

				IQueryable<PressReleaseCardModel> query = context.GetQueryable<PressReleaseCardModel>();

				query = query.Filter(i => i.Paths.Contains(listRoot.ID))
							.Filter(i => i.Language == listRoot.Language.Name)
							.Filter(i => i.TemplateId == PressReleaseID)
							.OrderByDescending(i => i.ReleaseDate);


				return query.ToPagedList(pageNum, pageSize);
			}
		}
	}
}
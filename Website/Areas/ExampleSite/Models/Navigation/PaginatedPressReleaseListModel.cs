using Website.Areas.ExampleSite.Models.Widgets;
using X.PagedList;

namespace Website.Areas.ExampleSite.Models.Navigation
{
	public class PaginatedPressReleaseListModel
	{
		public int Page
		{
			get { return List.PageNumber; }
		}

		public int PageSize
		{
			get { return List.PageSize; }
		}

		public int PageCount
		{
			get { return List.PageCount; }
		}

		public string ListHostUrl { get; set; }

		public IPagedList<PressReleaseCardModel> List { get; set; }

	}
}
using X.PagedList;

namespace Website.Areas.ExampleSite.Models.Navigation
{
	public class PaginatedTextSearchResultsModel
	{
		public string Text { get; set; }

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

		public int TotalResults
		{
			get { return List.TotalItemCount; }
		}

		public string ListHostUrl { get; set; }

		public IPagedList<TextSearchResultModel> List { get; set; }
	}
}

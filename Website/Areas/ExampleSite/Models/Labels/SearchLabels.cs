using Constellation.Foundation.ModelMapping.MappingAttributes;
using Feature.Labels;

namespace Website.Areas.ExampleSite.Models.Labels
{
	[Label("{5DEF3DE0-4FFA-4710-9C00-2CDE08A6979F}")]
	public class SearchLabels
	{
		[RawValueOnly]
		public string TextPlaceholder { get; set; }

		[RawValueOnly]
		public string SubmitButton { get; set; }

		[RawValueOnly]
		public string NoResultsFound { get; set; }

		[RawValueOnly]
		public string TotalResults { get; set; }

		[RawValueOnly]
		public string LoadMore { get; set; }
	}
}
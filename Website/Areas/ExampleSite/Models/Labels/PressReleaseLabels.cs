using Constellation.Foundation.ModelMapping.MappingAttributes;
using Feature.Labels;

namespace Website.Areas.ExampleSite.Models.Labels
{
	[Label("{82B82134-C685-4D38-8793-FC4688B711A2}")]
	public class PressReleaseLabels
	{
		[RawValueOnly]
		public string ReadMore { get; set; }

		[RawValueOnly]
		public string LoadMore { get; set; }

		[RawValueOnly]
		public string Author { get; set; }

		[RawValueOnly]
		public string ReleasedOn { get; set; }
	}
}
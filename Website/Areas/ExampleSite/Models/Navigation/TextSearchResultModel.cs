using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Converters;
using Sitecore.Data;
using Sitecore.Links;

namespace Website.Areas.ExampleSite.Models.Navigation
{
	public class TextSearchResultModel
	{
		#region Indexed Content Fields

		[IndexField("heading")]
		public virtual string Heading { get; set; }

		[IndexField("summary")]
		public virtual string Summary { get; set; }

		[IndexField("keywords")]
		[ScriptIgnore]
		public virtual string Keywords { get; set; }

		[IndexField("description")]
		[ScriptIgnore]
		public virtual string Description { get; set; }

		[IndexField("copy")]
		[ScriptIgnore]
		public virtual string Copy { get; set; }
		#endregion

		#region Indexing/Utility Properties
		[IndexField("_group")]
		[ScriptIgnore]
		[TypeConverter(typeof(IndexFieldIDValueConverter))]
		public virtual ID ItemId { get; set; }

		[IndexField("_name")]
		[ScriptIgnore]
		public virtual string Name { get; set; }

		[IndexField("_database")]
		[ScriptIgnore]
		public virtual string DatabaseName { get; set; }

		[IndexField("_language")]
		[ScriptIgnore]
		public virtual string Language { get; set; }

		[IndexField("_template")]
		[ScriptIgnore]
		[TypeConverter(typeof(IndexFieldIDValueConverter))]
		public virtual ID TemplateId { get; set; }

		[IndexField("aggregate_text")]
		[ScriptIgnore]
		public virtual string AggregateText { get; set; }

		/// <summary>
		/// This is here because getting the URL out of the search index is tricky. Note that this property should ONLY
		/// be accessed by something that's going to deal with it immediately to prevent accidentally loading all the items
		/// in a given search result set.
		/// </summary>
		public string FriendlyUrl
		{
			get
			{
				var language = Sitecore.Globalization.Language.Parse(this.Language);

				var database = Sitecore.Configuration.Factory.GetDatabase(DatabaseName);

				var item = database.GetItem(ItemId, language);

				return LinkManager.GetItemUrl(item);
			}
		}

		[IndexField("_path")]
		[ScriptIgnore]
		[TypeConverter(typeof(IndexFieldEnumerableConverter))]
		public IEnumerable<ID> Paths { get; set; }
		#endregion
	}
}

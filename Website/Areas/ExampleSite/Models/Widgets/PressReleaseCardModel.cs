using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using Constellation.Foundation.ModelMapping.MappingAttributes;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Converters;
using Sitecore.Data;
using Sitecore.Links;

namespace Website.Areas.ExampleSite.Models.Widgets
{
	public class PressReleaseCardModel
	{
		[IndexField("_group")]
		[ScriptIgnore]
		[TypeConverter(typeof(IndexFieldIDValueConverter))]
		public virtual ID ItemId { get; set; }

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

		[RawValueOnly]
		public string Heading { get; set; }

		[RawValueOnly]
		public string Summary { get; set; }

		[IndexField("release_date")]
		[ScriptIgnore]
		[TypeConverter(typeof(IndexFieldUtcDateTimeValueConverter))]
		public DateTime ReleaseDate { get; set; }

		public string ReleaseDateFormatted
		{
			get { return ReleaseDate.ToString("MMM dd, yyyy"); }
		}


	}
}
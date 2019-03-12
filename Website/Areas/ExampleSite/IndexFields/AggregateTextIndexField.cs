using System;
using System.Text;
using HtmlAgilityPack;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;

namespace Website.Areas.ExampleSite.IndexFields
{
	public class AggregateTextIndexField : AbstractComputedIndexField
	{
		public override object ComputeFieldValue(IIndexable indexable)
		{
			Item item = indexable as SitecoreIndexableItem;
			if (item == null)
			{
				return null;
			}

			if (!ItemNeedsAggregateTextInIndex(item))
			{
				return null;
			}

			if (!item.Versions.IsLatestVersion())
			{
				return null;
			}


			var valueToIndex = new StringBuilder();

			//the fields of the current item will be added to index
			foreach (Field field in item.Fields)
			{
				if (ShouldAddFieldValue(field))
				{
					valueToIndex.Append(GetFieldValue(field) + " ");
				}
			}
			var renderings = GetRenderingReferences(item, "default");

			foreach (var rendering in renderings)
			{
				if (string.IsNullOrEmpty(rendering.Settings.DataSource))
				{
					continue;
				}

				var dataSourceItem = item.Database.GetItem(rendering.Settings.DataSource);

				//the fields of the rendering of the current item will be added to index
				if (dataSourceItem != null)
				{
					foreach (Field field in dataSourceItem.Fields)
					{
						if (ShouldAddFieldValue(field))
						{
							valueToIndex.Append(GetFieldValue(field) + " ");
						}
					}
				}
			}

			Log.Debug(
				$"Feature.Search.IndexFields.AggregateTextIndexField: Item {item.Name} - adding value \"{valueToIndex.ToString().Trim()}\" to index.",
				this);

			return valueToIndex.ToString().Trim();
		}

		private string GetFieldValue(Field field)
		{
			if (string.Equals(field.Definition.Type, "rich text", StringComparison.InvariantCultureIgnoreCase))
			{

			}

			return field.Value;
		}


		private Sitecore.Layouts.RenderingReference[] GetRenderingReferences(Item item, string deviceName)
		{
			LayoutField layoutField = item.Fields["__final renderings"];
			if (layoutField == null)
			{
				return null;
			}

			RenderingReference[] renderings = layoutField.GetReferences(GetDeviceItem(item.Database, deviceName));

			return renderings;
		}


		private bool ShouldAddFieldValue(Field field)
		{
			if (string.IsNullOrEmpty(field.Value))
			{
				return false;
			}

			if (field.Name.StartsWith("_"))
			{
				return false;
			}

			var fieldType = field.Definition.Type.ToLower();

			switch (fieldType)
			{
				case "single-line text":
					return true;
				case "multi-line text":
					return true;
				case "rich text":
					return true;
				default:
					return false;
			}
		}

		private bool ItemNeedsAggregateTextInIndex(Item item)
		{
			if (!item.Versions.IsLatestVersion())
			{
				return false; // Only waste time computing this on the most recent version.
			}

			if (item.Visualization.Layout == null)
			{
				return false; // No layout, not a Page, no need to aggregate text.
			}

			if (item.Paths.FullPath.StartsWith("/sitecore/content"))
			{
				return true; // Item is real content, has presentation, and is likely a page.
			}

			return false;
		}

		private DeviceItem GetDeviceItem(Sitecore.Data.Database database, string deviceName)
		{
			return database.Resources.Devices[deviceName];
		}

		public string StripHtml(string source)
		{
			try
			{
				var doc = new HtmlDocument();
				doc.Load(source);

				return doc.DocumentNode.InnerText;
			}
			catch (Exception ex)
			{
				Log.Error("Feature.Search.IndexFields.AggregateTextIndexField: Could not parse Rich Text.", ex, this);
				return source;
			}
		}
	}
}

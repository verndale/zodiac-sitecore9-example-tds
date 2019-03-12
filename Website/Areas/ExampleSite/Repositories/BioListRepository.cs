using System.Collections.Generic;
using Constellation.Foundation.Caching;
using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc.Patterns;
using Constellation.Foundation.Mvc.Patterns.Repositories;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
using Website.Areas.ExampleSite.Models.Widgets;

namespace Website.Areas.ExampleSite.Repositories
{
	public class BioListRepository : ItemListRepository<BioCardModel>
	{
		// ReSharper disable once InconsistentNaming
		protected static ID BioPageID = new ID("{B1E2FB15-6D9B-42E8-BE31-18F7FCD87E00}");

		public BioListRepository(IModelMapper modelMapper, ICacheManager cacheManager) : base(modelMapper, cacheManager)
		{
		}

		protected override ICollection<BioCardModel> GetUncachedModel(RepositoryContext context)
		{
			var list = new List<BioCardModel>();
			var items = GetItems(context);

			if (items == null)
			{
				return new BioCardModel[] { };
			}

			// Doing it this way takes one less loop through the collection than getting the collection cast first.
			foreach (var item in items)
			{
				var card = ModelMapper.MapItemToNew<BioCardModel>(item);

				list.Add(card);
			}

			// We want these in reverse-chronological order.
			return list;
		}

		protected override ICollection<Item> GetItems(RepositoryContext context)
		{
			/* We are assuming that Bios are stored in alphabetical folders, so
			 * we want to traverse that folder and just get the Bios.
			 *
			 * We are also assuming that the Datasource for the Rendering is the root Item under which
			 * Bios are stored.
			 *
			 * ./alpha/bio
			 */

			return Query.SelectItems($"./*/*[@@templateId=\"{BioPageID}\"]", context.Datasource);
		}
	}
}
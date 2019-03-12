using Constellation.Feature.Navigation.Repositories;
using Constellation.Foundation.Mvc;
using Sitecore.Data.Items;

namespace Website.Areas.ExampleSite.Controllers.Navigation
{
	public class UtilityNavigationController : ConventionController
	{
		#region Constructor
		public UtilityNavigationController(IViewPathResolver viewPathResolver, IDeclaredNavigationRepository repository) : base(viewPathResolver)
		{
			Repository = repository;
		}
		#endregion

		#region Properties
		public IDeclaredNavigationRepository Repository { get; }
		#endregion

		protected override object GetModel(Item datasource, Item contextItem)
		{
			/* Rendering Datasource should be the root level Navigation Menu Item,
			 * in our case /ExampleSite/Navigation/Utility
			 * We don't care about context highlighting on this menu.
			 */

			var model = Repository.GetNavigation(datasource);

			return model;
		}
	}
}
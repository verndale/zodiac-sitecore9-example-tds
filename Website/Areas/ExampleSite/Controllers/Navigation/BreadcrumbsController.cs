using Constellation.Feature.Navigation.Repositories;
using Constellation.Foundation.Mvc;
using Sitecore.Data.Items;

namespace Website.Areas.ExampleSite.Controllers.Navigation
{
	public class BreadcrumbsController : ConventionController
	{
		#region Constructor
		public BreadcrumbsController(IViewPathResolver viewPathResolver, IBreadcrumbNavigationRepository repository) : base(viewPathResolver)
		{
			Repository = repository;
		}
		#endregion

		#region Properties
		protected IBreadcrumbNavigationRepository Repository { get; }
		#endregion

		protected override object GetModel(Item datasource, Item contextItem)
		{

			/* This is the *only* time you should use Sitecore.Context, as there's no way to
			 * get the Site from the RenderingContext.
			 */

			return Repository.GetNavigation(contextItem, Sitecore.Context.Site.SiteInfo);
		}
	}
}
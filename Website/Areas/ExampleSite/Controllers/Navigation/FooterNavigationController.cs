using Constellation.Feature.Navigation.Repositories;
using Constellation.Foundation.Mvc;
using Sitecore.Data.Items;

namespace Website.Areas.ExampleSite.Controllers.Navigation
{
	public class FooterNavigationController : ConventionController
	{
		#region Constructor
		public FooterNavigationController(IViewPathResolver viewPathResolver, IDeclaredNavigationRepository repository) : base(viewPathResolver)
		{
			Repository = repository;
		}
		#endregion

		#region Properties
		protected IDeclaredNavigationRepository Repository { get; }
		#endregion

		protected override object GetModel(Item datasource, Item contextItem)
		{
			return Repository.GetNavigation(datasource);
		}
	}
}
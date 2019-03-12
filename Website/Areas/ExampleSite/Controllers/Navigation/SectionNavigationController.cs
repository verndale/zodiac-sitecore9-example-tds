using Constellation.Feature.Navigation.Models;
using Constellation.Feature.Navigation.Repositories;
using Constellation.Foundation.ModelMapping;
using Constellation.Foundation.Mvc;
using Sitecore.Data.Items;

namespace Website.Areas.ExampleSite.Controllers.Navigation
{
	public class SectionNavigationController : ConventionController
	{
		#region Constructor

		public SectionNavigationController(IViewPathResolver viewPathResolver, IBranchNavigationRepository repository, IModelMapper modelMapper) : base(viewPathResolver)
		{
			Repository = repository;
			ModelMapper = modelMapper;
		}
		#endregion

		#region Properties
		protected IBranchNavigationRepository Repository { get; }

		protected IModelMapper ModelMapper { get; }
		#endregion

		protected override object GetModel(Item datasource, Item contextItem)
		{
			var model = Repository.GetNavigation(contextItem, true);

			if (model == null)
			{
				model = ModelMapper.MapItemToNew<BranchNode>(contextItem);
			}

			return model;
		}
	}
}
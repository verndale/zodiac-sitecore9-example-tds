using System.Web.Mvc;
using Constellation.Foundation.Mvc;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Website.Areas.ExampleSite.Models.Navigation;
using Website.Areas.ExampleSite.Repositories;

namespace Website.Areas.ExampleSite.Controllers.Navigation
{
	public class PaginatedPressReleaseListController : Controller
	{
		public PaginatedPressReleaseListController(IViewPathResolver viewPathResolver)
		{
			ViewPathResolver = viewPathResolver;
		}

		protected IViewPathResolver ViewPathResolver { get; set; }

		public ActionResult Index(int page = 1, bool json = false)
		{
			var repository = new PaginatedPressReleaseListRepository();

			var model = new PaginatedPressReleaseListModel
			{
				List = repository.GetPage(page, 10, RenderingContext.Current.ContextItem),
				ListHostUrl = LinkManager.GetItemUrl(RenderingContext.Current.PageContext.Item)
			};


			if (json)
			{
				return Json(model, JsonRequestBehavior.AllowGet);
			}

			var viewPath = ViewPathResolver.ResolveViewPath(RenderingContext.Current.Rendering.RenderingItem);
			return View(viewPath, model);
		}
	}
}
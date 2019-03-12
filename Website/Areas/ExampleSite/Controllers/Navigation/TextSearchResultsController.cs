using System.Web.Mvc;
using Constellation.Foundation.Mvc;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Website.Areas.ExampleSite.Models.Navigation;
using Website.Areas.ExampleSite.Repositories;

namespace Website.Areas.ExampleSite.Controllers.Navigation
{
	public class TextSearchResultsController : Controller
	{
		public TextSearchResultsController(IViewPathResolver viewPathResolver)
		{
			ViewPathResolver = viewPathResolver;
		}

		protected IViewPathResolver ViewPathResolver { get; set; }

		public ActionResult Index(string text, int page = 1, bool json = false)
		{
			var searchtext = text.Trim();
			var site = Sitecore.Context.Site;
			var context = RenderingContext.Current.PageContext.Item;

			var siteRoot = context.Database.GetItem(site.StartPath, context.Language);

			var repository = new TextSearchRepository();

			var model = new PaginatedTextSearchResultsModel
			{
				Text = searchtext,
				List = repository.GetPage(searchtext, page, 10, siteRoot),
				ListHostUrl = LinkManager.GetItemUrl(context)
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

using System.Web.Mvc;

namespace Dodo.RestaurantBoard.Site.Controllers
{
	public class ExceptionController : Controller
	{
		/// <summary>
		/// Произошла ошибка
		/// </summary>
		/// <returns></returns>
		public ActionResult HandleError()
		{
			return View();
		}

		public string AjaxHandleError()
		{
			return "Error: " + TempData["Exception"];
		}
	}
}

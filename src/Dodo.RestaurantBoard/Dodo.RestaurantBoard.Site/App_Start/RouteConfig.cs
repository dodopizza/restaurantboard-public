using System.Web.Mvc;
using System.Web.Routing;

namespace Dodo.RestaurantBoard.Site
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"OrdersReadinessToStationary",
				"restaurant/{unitId}",
				new { action = "OrdersReadinessToStationary", controller = "Boards" },
				namespaces: new string[] { "Dodo.RestaurantBoard.Site.Controllers" }
			);

			routes.MapRoute
			(
				"ExpressProducts",
				"express/{unitId}",
				new { action = "ExpressProducts", controller = "Boards" },
				namespaces: new string[] { "Dodo.RestaurantBoard.Site.Controllers" }
			);

			routes.MapRoute(
				"GetDeviceIdFromTicket",
				"Devices/TryGetDeviceIdFromEncryptedTicket",
				new { controller = "Devices", action = "TryGetDeviceIdFromEncryptedTicket" }
			);


			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Boards", action = "Index", id = UrlParameter.Optional },
				namespaces: new string[] { "Dodo.RestaurantBoard.Site.Controllers" }
			);
		}
	}
}

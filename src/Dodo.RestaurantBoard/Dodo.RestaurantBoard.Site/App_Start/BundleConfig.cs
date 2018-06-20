using System.Web.Optimization;

namespace Dodo.RestaurantBoard.Site
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Clear();
			bundles.ResetAll();
			BundleTable.EnableOptimizations = true;

#if DEBUG
			BundleTable.EnableOptimizations = false;
#endif

			bundles.Add(new StyleBundle("~/bundle/css/processing").Include(
				"~/Content/Css/processing.css"
			));
		}
	}
}

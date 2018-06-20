using System.Web.Mvc;
using Dodo.RestaurantBoard.Site.Common.Helpers;
using JetBrains.Annotations;

namespace Dodo.RestaurantBoard.Site.Common.Extensions
{
	public static class UrlHelperExtensions
	{
		public static string ContentVersioned(this UrlHelper helper, [PathReference] string contentPath)
		{
			contentPath = VersionHelper.AddVersionToken(contentPath);
			return helper.Content(contentPath);
		}
	}
}

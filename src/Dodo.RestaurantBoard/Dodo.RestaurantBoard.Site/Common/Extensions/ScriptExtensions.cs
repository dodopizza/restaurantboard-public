using System;
using System.ComponentModel;
using System.Web.Mvc;
using Dodo.RestaurantBoard.Site.Common.Helpers;
using JetBrains.Annotations;

namespace Dodo.RestaurantBoard.Site.Common.Extensions
{
	public static class ScriptExtensions
	{
		[Description("ScriptVersioned у каждой сборки должен быть свой, т.к. он зависит от Assembly")]
		public static MvcHtmlString ScriptVersioned(this HtmlHelper helper, [PathReference] string path, object htmlAttributes = null)
		{
			if (string.IsNullOrEmpty(path))
				throw new ArgumentNullException(nameof(path));

			path = VersionHelper.AddVersionToken(path);

			var tagBuilder = new TagBuilder("script");
			tagBuilder.MergeAttribute("src", UrlHelper.GenerateContentUrl(path, helper.ViewContext.HttpContext));
			tagBuilder.MergeAttribute("type", "text/javascript");

			if (htmlAttributes == null)
				return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			tagBuilder.MergeAttributes(attributes);

			return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
		}
	}
}

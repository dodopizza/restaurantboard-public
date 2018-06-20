using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Dodo.Core.Common.Helpers;
using NLog;

namespace Dodo.RestaurantBoard.Site
{
	public class MvcApplication : HttpApplication
	{
		private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

		protected void Application_Start()
		{
			// Configure NLog to log country information
			GlobalDiagnosticsContext.Set("country", CountryHelper.GetCountryCodeOrDefault.ToString("G"));
			_logger.Info("{0} application is starting...", typeof(MvcApplication).FullName);

			DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacConfig.Register()));

			BundleConfig.RegisterBundles(BundleTable.Bundles);

			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		protected void Application_End()
		{
			_logger.Info($"{typeof(MvcApplication).FullName} application is ending...");
		}

		protected void Application_AcquireRequestState(object sender, EventArgs e)
		{
			SetCurrentCulture();
		}

		private void SetCurrentCulture()
		{
			if (HttpContext.Current.Session == null)
				return;

			var interfaceCulture = (CultureInfo) Session["Culture"];
			var cultureForMainThread = new CultureInfo(ConfigCultureHelper.CurrentThreadCultureInfo);
			
			if (interfaceCulture == null)
			{
				interfaceCulture = cultureForMainThread;
				Session["Culture"] = interfaceCulture;
			}

			CultureInfo.DefaultThreadCurrentUICulture = interfaceCulture;
			Thread.CurrentThread.CurrentUICulture = interfaceCulture;

			CultureInfo.DefaultThreadCurrentCulture = cultureForMainThread;
			Thread.CurrentThread.CurrentCulture = cultureForMainThread;
		}
	}
}

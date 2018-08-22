using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dodo.RestaurantBoard.Site
{
	public static class AutofacConfig
	{
		public static IContainer Register(IServiceCollection services, IConfiguration configuration)
		{
			var builder = new ContainerBuilder()
				.RegisterServices(configuration);

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Populate(services);

			return builder.Build();
		}

		private static ContainerBuilder RegisterServices(this ContainerBuilder builder, IConfiguration configuration)
		{
			builder.RegisterType<DepartmentsStructureService>().As<IDepartmentsStructureService>().SingleInstance();
			builder.RegisterType<ManagementService>().As<IManagementService>().SingleInstance();
			builder.RegisterType<ClientService>().As<IClientsService>().SingleInstance();
			builder.RegisterType<TrackerClient>()
				.As<ITrackerClient>()
				.WithParameter("baseUri", new Uri(configuration["Tracker:Uri"], UriKind.Absolute))
				.SingleInstance();

			return builder;
		}
	}
}

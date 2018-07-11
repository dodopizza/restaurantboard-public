using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dodo.RestaurantBoard.Site
{
	public static class AutofacConfig
	{
		public static IContainer Register(IServiceCollection services)
		{
			var builder = new ContainerBuilder()
				.RegisterServices();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Populate(services);

			return builder.Build();
		}

		private static ContainerBuilder RegisterServices(this ContainerBuilder builder)
		{
			builder.RegisterType<DepartmentsStructureService>().As<IDepartmentsStructureService>().SingleInstance();
			builder.RegisterType<ManagementService>().As<IManagementService>().SingleInstance();
			builder.RegisterType<ClientService>().As<IClientsService>().SingleInstance();

			builder.RegisterType<TrackerClient>().As<ITrackerClient>();
            builder.RegisterType<InMemoryOrdersStorage>().As<IOrdersStorage>();

            return builder;
		}
	}
}

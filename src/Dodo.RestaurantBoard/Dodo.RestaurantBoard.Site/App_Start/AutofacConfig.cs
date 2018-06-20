using Autofac;
using Autofac.Integration.Mvc;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;

namespace Dodo.RestaurantBoard.Site
{
	public static class AutofacConfig
	{
		private static IContainer Container { get; set; }

		public static IContainer Register()
		{
			Container = new ContainerBuilder()
				.RegisterServices()
				.RegisterComponents()
				.Build();

			return Container;
		}

		private static ContainerBuilder RegisterServices(this ContainerBuilder builder)
		{
			builder.RegisterType<DepartmentsStructureService>().As<IDepartmentsStructureService>().SingleInstance();
			builder.RegisterType<ManagementService>().As<IManagementService>().SingleInstance();
			builder.RegisterType<ClientService>().As<IClientsService>().SingleInstance();

			builder.RegisterType<TrackerClient>().As<ITrackerClient>();

			return builder;
		}

		private static ContainerBuilder RegisterComponents(this ContainerBuilder builder)
		{
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterFilterProvider();

			return builder;
		}
	}
}

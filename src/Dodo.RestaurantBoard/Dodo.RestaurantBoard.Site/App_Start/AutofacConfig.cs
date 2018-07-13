using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Domain.Stores;
using Dodo.Tracker.Contracts;
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



            var orderStore = new OrdersStore();
            orderStore.AddOrder(new ProductionOrder(){ OrderDate = DateTime.Now, ClientName = "DoDo", Number = 55});
            orderStore.AddOrder(new ProductionOrder(){ OrderDate = DateTime.Now, ClientName = "DiDi", Number = 56});
            orderStore.AddOrder(new ProductionOrder(){ OrderDate = DateTime.Now, ClientName = "DaDa", Number = 57});
		    builder.RegisterInstance(orderStore).As<IOrdersStore>().SingleInstance();
			return builder;
		}
	}
}

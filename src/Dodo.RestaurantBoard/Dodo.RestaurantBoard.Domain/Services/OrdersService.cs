using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dodo.Core.DomainModel.ClientOrders;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public class OrdersService : IOrdersService
	{
		private readonly IDepartmentsStructureService _departmentsStructureService;
		private readonly IClientsService _clientsService;
		private readonly ITrackerClient _trackerClient;
		private readonly IIconPathService _iconPathService;

		public OrdersService(
			IDepartmentsStructureService departmentsStructureService, 
			IClientsService clientsService, 
			ITrackerClient trackerClient, 
			IIconPathService iconPathService)
		{
			_departmentsStructureService = departmentsStructureService;
			_clientsService = clientsService;
			_trackerClient = trackerClient;
			_iconPathService = iconPathService;
		}

		public GetOrdersResult GetOrdersForUnit(
			int unitId,
			IDictionary<string, object> viewData,
			ICollection<int> currentProductsIds)
		{
			const int maxCountOrders = 16;

			var pizzeria = _departmentsStructureService.GetPizzeriaOrCache(unitId);
			var orders = _trackerClient
				.GetOrdersByType(pizzeria.Uuid, OrderType.Stationary, new[] { OrderState.OnTheShelf }, maxCountOrders)
				.Select(MapToRestaurantReadnessOrders)
				.ToArray();

			var clientTreatment = pizzeria.ClientTreatment;
			ClientIcon[] icons = { };
			if (clientTreatment == ClientTreatment.RandomImage)
			{
				icons = _clientsService.GetIcons();
			}

			var playTineParamIds = orders.Select(x => x.OrderId).ToArray();
			viewData["PlayTune"] = playTineParamIds.Except(currentProductsIds).Any() ? 1 : 0;

			var result = new ClientOrdersModel
			{
				PlayTune = (int)viewData["PlayTune"],
				NewOrderArrived = (int)viewData["PlayTune"] == 1,
				SongName = orders.Length == 0 ? DodoFMProxy.GetSongName() : string.Empty,
				ClientOrders = orders.Select(
						x => new Order
						{
							OrderId = x.OrderId,
							OrderNumber = x.OrderNumber,
							ClientName = x.ClientName,
							ClientIconPath = _iconPathService.GetIconPath(x, clientTreatment, icons),
							OrderReadyTimestamp = x.OrderReadyDateTime.Ticks,
							OrderReadyDateTime = x.OrderReadyDateTime.ToString(CultureInfo.CurrentUICulture)
						})
					.OrderByDescending(x => x.OrderReadyTimestamp)
					.ToArray()
			};

			return new GetOrdersResult
			{
				ClientOrdersModel = result,
				ProductIds = playTineParamIds
			};
		}

		private static RestaurantReadnessOrders MapToRestaurantReadnessOrders(ProductionOrder order) => 
			new RestaurantReadnessOrders(
				order.Id, 
				order.Number, 
				order.ClientName, 
				order.ChangeDate ?? DateTime.Now);
	}
}
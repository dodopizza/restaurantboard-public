﻿using System.Linq;
using Dodo.Core.Common;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit);
	}

	public class TrackerClient : ITrackerClient
	{
		public ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
		{
			var orders = new[]
			{
				new ProductionOrder
				{
					Id = 55,
					Number = 3,
					ClientName = "Пупа"
				},
				new ProductionOrder
				{
					Id = 56,
					Number = 4,
					ClientName = "Лупа"
				},
			};

			orders = LimitOrders(orders, limit);

			return orders;
		}

		private ProductionOrder[] LimitOrders(ProductionOrder[] orders, int limit)
		{
			return orders.Take(limit).ToArray();
		}
	}
}
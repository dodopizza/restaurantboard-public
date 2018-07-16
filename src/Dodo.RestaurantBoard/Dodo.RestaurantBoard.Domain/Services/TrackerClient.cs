using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
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
		
		private int lastOrderId;
		private int lastOrderNumber;

		public List<ProductionOrder> Orders { get; } = new List<ProductionOrder>();

		private TrackerPresenter presenter;
		private BeepService beepService;

		public TrackerClient()
		{
			
		}

		public TrackerClient(TrackerPresenter presenter)
		{
			this.presenter = presenter;
		}

		public TrackerClient(BeepService beepService)
		{
			this.beepService = beepService;
		}
		
		public TrackerClient(TrackerPresenter presenter, BeepService beepService)
		{
			this.presenter = presenter;
			this.beepService = beepService;
		}
		
		public ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
		{			
			
//				new ProductionOrder
//				{
//					Id = 55,
//					Number = 3,
//					ClientName = "Пупа"
//				},
//				new ProductionOrder
//				{
//					Id = 56,
//					Number = 4,
//					ClientName = "Лупа"
//				},
//				new ProductionOrder
//				{
//					Id = 57,
//					Number = 7,
//					ClientName = "Миша"
//				}
//			new ProductionOrder
//			{
//				Id = 58,
//				Number = 11,
//				ClientName = "Лёша"
//			};

			return Orders.ToArray();
		}

		private void IncrementId()
		{
			++lastOrderId;
		}

		public int AddOrder(string clientName)
		{
			this.IncrementId();
			this.IncrementNumber();
			var order = new ProductionOrder()
			{
				ChangeDate = DateTime.Now,
				ClientName = clientName,
				Id = lastOrderId,
				Number = lastOrderNumber
			};

			Orders.Add(order);
			
			this.presenter?.UpdateListOfVisibleOrders();
			this.beepService?.Beep();

			return order.Id;
		}

		public ProductionOrder GetOrder(int id)
		{
			return this.Orders.Single(order => order.Id == id);
		}

		public void RemoveOrder(int id)
		{
			var orderToRemove = this.Orders.First(order => order.Id == id);
			if (orderToRemove != null)
			{
				this.Orders.Remove(orderToRemove);
			}
			this.presenter?.UpdateListOfVisibleOrders();
		}

		private void IncrementNumber()
		{
			++lastOrderNumber;
		}

		public int NumberOfOrders => Orders.Count;
	}
}
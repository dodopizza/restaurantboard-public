using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using Xunit;

namespace Dodo.Tests
{
	public class OrderServiceTests
	{
		private IDictionary<string, object> ViewDataDummy { get; } =
			new Dictionary<string, object>();

		private ICollection<int> OrderIdsDummy { get; } =
			new List<int>();

		[Fact]
		[Description("Метод GetIconPath вызываться столько раз, сколько заказов у клиента")]
		public void GetOrdersForUnit_ForUnitId_ShoudExecuteGetIconPathForEachOrder()
		{
			const int ordersCount = 10;

			var iconPathMock = new Mock<IIconPathService>();
			var orderService = new OrdersService(
				CreateDepartmentStructureService(ClientTreatment.RandomImage),
				Mock.Of<IClientsService>(),
				CreateTrackerClientService(ordersCount),
				iconPathMock.Object);

			orderService.GetOrdersForUnit(
				1,
				ViewDataDummy,
				OrderIdsDummy);

			iconPathMock.Verify(_ =>
				_.GetIconPath(
					It.IsAny<RestaurantReadnessOrders>(),
					It.IsAny<ClientTreatment>(),
					It.IsAny<ClientIcon[]>()),
				Times.Exactly(ordersCount));
		}

		[Fact]
		[Description("Если заказов для клиента 0 - метод GetIconPath вызываться не должен")]
		public void GetOrdersForUnit_IfOrdersCountZero_ShoudNotExecuteGetIconPath()
		{
			var iconPathMock = new Mock<IIconPathService>();
			var orderService = new OrdersService(
				CreateDepartmentStructureService(),
				Mock.Of<IClientsService>(),
				CreateTrackerClientService(ordersCount: 0),
				iconPathMock.Object);

			orderService.GetOrdersForUnit(
				1,
				ViewDataDummy,
				OrderIdsDummy);

			iconPathMock.Verify(_ =>
					_.GetIconPath(
						It.IsAny<RestaurantReadnessOrders>(),
						It.IsAny<ClientTreatment>(),
						It.IsAny<ClientIcon[]>()),
				Times.Never);
		}

		[Fact]
		[Description("Если для клиентов пиццерии генерируются случайные картинки, то метод GetIcons должен быть вызван для их генерации")]
		public void GetOrdersForUnit_IfClientTreatmentIsRandomImage_ShouldExecuteGetIcons()
		{
			var clientsServiceMock = new Mock<IClientsService>();
			var orderService = new OrdersService(
				CreateDepartmentStructureService(ClientTreatment.RandomImage),
				clientsServiceMock.Object,
				CreateTrackerClientService(0),
				Mock.Of<IIconPathService>());

			orderService.GetOrdersForUnit(
				1,
				ViewDataDummy,
				OrderIdsDummy);

			clientsServiceMock.Verify(_ => _.GetIcons(), Times.Once);
		}

		[Fact]
		[Description("Если у клиентов пиццерии есть собственные картинки, то метод GetIcons не должен быть вызван")]
		public void GetOrdersForUnit_IfClientTreatmentIsNotRandomImage_ShouldNeverExecuteGetIcons()
		{
			var clientsServiceMock = new Mock<IClientsService>();
			var orderService = new OrdersService(
				CreateDepartmentStructureService(ClientTreatment.DefaultName),
				clientsServiceMock.Object,
				CreateTrackerClientService(0),
				Mock.Of<IIconPathService>());

			orderService.GetOrdersForUnit(
				1,
				ViewDataDummy,
				OrderIdsDummy);

			clientsServiceMock.Verify(_ => _.GetIcons(), Times.Never);
		}

		[Fact]
		[Description("При каждом вызове GetOrdersForUnit должен быть вызван GetPizzeriaOrCache")]
		public void GetOrdersForUnit_ForUnitId_ShouldExecuteGetPizzeriaOrCacheWithSpecifiedUnitId()
		{
			const int unitId = 1;

			var departmentServiceMock = CreateDepartmentStructureServiceMock(ClientTreatment.RandomImage);
			var orderService = new OrdersService(
				departmentServiceMock.Object,
				Mock.Of<IClientsService>(),
				Mock.Of<ITrackerClient>(),
				Mock.Of<IIconPathService>());

			orderService.GetOrdersForUnit(
				unitId,
				ViewDataDummy,
				OrderIdsDummy);

			departmentServiceMock.Verify(_ => _.GetPizzeriaOrCache(It.IsIn(unitId)), Times.Once);
		}

		[Fact]
		[Description("Если появились новые заказы, то необходимо проигрывать музыку")]
		public void GetOrdersForUnit_IfNewOrdersArrived_ShouldPlayMusic()
		{
			var existingOrderIds = new[] { 1 };
			var orderService = new OrdersService(
				CreateDepartmentStructureService(),
				Mock.Of<IClientsService>(),
				CreateTrackerClientService(ordersCount: 2),
				Mock.Of<IIconPathService>());

			var result = orderService.GetOrdersForUnit(
				1,
				ViewDataDummy,
				existingOrderIds);

			Assert.Equal(1, result.ClientOrdersModel.PlayTune);
		}

		[Fact]
		[Description("Если новых заказов нет, то музыка должна быть выключена")]
		public void GetOrdersForUnit_IfNewOrdesNotArrived_ShouldTurnOffMusic()
		{
			var existingOrderIds = new[] { 1 };
			var orderService = new OrdersService(
				CreateDepartmentStructureService(),
				Mock.Of<IClientsService>(),
				CreateTrackerClientService(ordersCount: 1),
				Mock.Of<IIconPathService>());

			var result = orderService.GetOrdersForUnit(
				1,
				ViewDataDummy,
				existingOrderIds);

			Assert.Equal(0, result.ClientOrdersModel.PlayTune);
		}

		private static ITrackerClient CreateTrackerClientService(int ordersCount)
		{
			var trackerClientStub = new Mock<ITrackerClient>();
			trackerClientStub.Setup(_ => _.GetOrdersByType(
					It.IsAny<Uuid>(),
					It.IsAny<OrderType>(),
					It.IsAny<OrderState[]>(),
					It.IsAny<int>()))
				.Returns((Uuid uuid, OrderType type, OrderState[] states, int limit) =>
					Enumerable.Range(1, ordersCount)
						.Select(id => new ProductionOrder
							{
								Id = id,
								Number = id
							})
						.ToArray());
			return trackerClientStub.Object;
		}

		private static IDepartmentsStructureService CreateDepartmentStructureService(
			ClientTreatment pizzeriaClientTreatment = ClientTreatment.Name)
		{
			return CreateDepartmentStructureServiceMock(pizzeriaClientTreatment).Object;
		}

		private static Mock<IDepartmentsStructureService> CreateDepartmentStructureServiceMock(
			ClientTreatment pizzeriaClientTreatment)
		{
			var departmentStructureServiceStub = new Mock<IDepartmentsStructureService>();
			departmentStructureServiceStub.Setup(
					_ => _.GetPizzeriaOrCache(It.IsAny<int>()))
				.Returns((int id) => CreatePizzeria(id, pizzeriaClientTreatment));
			return departmentStructureServiceStub;
		}

		private static Pizzeria CreatePizzeria(int id, ClientTreatment clientTreatment)
		{
			return new Pizzeria(
				id,
				Uuid.NewUUId(),
				"",
				"",
				"",
				UnitApprove.Approved,
				UnitState.Open,
				1,
				Uuid.NewUUId(),
				1,
				null,
				100,
				new DateTime(2008, 10, 1),
				"",
				true,
				10.54m,
				42.42m,
				clientTreatment,
				true,
				new PizzeriaFormat(
					1,
					"",
					""));
		}
	}
}

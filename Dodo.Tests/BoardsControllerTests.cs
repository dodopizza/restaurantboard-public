using System;
using System.Linq;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Management;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.DomainModel.Products;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tests.DSL;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using NUnit.Framework;

namespace Dodo.Tests
{
    [TestFixture]
    public class BoardsControllerTests
    {
        private readonly ObjectMother _objectMother = new ObjectMother();
        private readonly BoardsControllerBuilder _boardsControllerBuilder = new BoardsControllerBuilder();

        // Behaviour
        [Test]
        public void Index_ShouldCall_GetUnitOrCache()
        {
            var departmentsStructureServiceMock = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceMock
                .Setup(x => x.GetUnitOrCache(Uuid.Empty))
                .Returns(_objectMother.CreateUnitWithUuid(Uuid.Empty));
            var boardsControllerStub = _boardsControllerBuilder.
                CreateBoardsControllerWithDepartmentService(departmentsStructureServiceMock.Object);

            boardsControllerStub.Index();

            departmentsStructureServiceMock.Verify(x => x.GetUnitOrCache(Uuid.Empty), Times.Once);
        }

        // Behaviour
        [Test]
        public void OrdersReadinessToStationary_ShouldCall_GetDepartmentByUnitOrCacheAndGetPizzeriaOrCache()
        {
            var departmentsStructureServiceMock = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceMock
                .Setup(x => x.GetDepartmentByUnitOrCache(1))
                .Returns(_objectMother.CreateDepartment());
            departmentsStructureServiceMock
                .Setup(x => x.GetPizzeriaOrCache(1))
                .Returns(_objectMother.CreatePizzeria());
            var boardsControllerStub = _boardsControllerBuilder.
                    CreateBoardsControllerWithDepartmentService(departmentsStructureServiceMock.Object);

            boardsControllerStub.OrdersReadinessToStationary(1);

            departmentsStructureServiceMock.Verify(x => x.GetDepartmentByUnitOrCache(1), Times.Once);
            departmentsStructureServiceMock.Verify(x => x.GetPizzeriaOrCache(1), Times.Once);
        }

        // State
        [Test]
        public void OrdersReadinessToStationary_ShouldThrowArgumentException_WhenDepartmentNotFoundByUnitId()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetDepartmentByUnitOrCache(1))
                .Returns((Department)null);
            var boardsControllerMock = _boardsControllerBuilder.
                CreateBoardsControllerWithDepartmentService(departmentsStructureServiceStub.Object);

            var argumentException = Assert.Throws<ArgumentException>(() => boardsControllerMock.OrdersReadinessToStationary(1));
            Assert.That(argumentException.Message, Is.EqualTo("unitId"));
        }

        // Behaviour
        [Test]
        public void GetOrderReadinessToStationary_ShouldUseNumberProperty_ForEachOrder()
        {
            var order1 = new Mock<ProductionOrder>();
            order1.Setup(x => x.Number).Returns(1);
            var order2 = new Mock<ProductionOrder>();
            order2.Setup(x => x.Number).Returns(2);
            var boardsControllerStub =
                _boardsControllerBuilder.CreateBoardsControllerWithTrackerProductionOrderFakes(new[] {order1, order2});

            boardsControllerStub.GetOrderReadinessToStationary(1);

            order1.Verify(x => x.Number, Times.Once);
            order2.Verify(x => x.Number, Times.Once);
        }

        // State
        [Test]
        public void GetOrderReadinessToStationary_ShouldInResultJsonForEachOddOrderNumber_HaveRedColor()
        {
            var oddOrder = new Mock<ProductionOrder>();
            oddOrder.Setup(x => x.Number).Returns(1);
            var evenOrder = new Mock<ProductionOrder>();
            evenOrder.Setup(x => x.Number).Returns(2);
            var boardsControllerMock =
                _boardsControllerBuilder.CreateBoardsControllerWithTrackerProductionOrderFakes(new[] {oddOrder, evenOrder});

            var jsonResult = boardsControllerMock.GetOrderReadinessToStationary(1);
            var order = jsonResult.Value as IOrder;
            var oddOrderColor = order.ClientOrders
                .Where(x => x.OrderNumber == 1)
                .Select(s => s.Color)
                .First();

            Assert.AreEqual("red", oddOrderColor);
        }

        // State
        [Test]
        public void GetOrderReadinessToStationary_ShouldInResultJsonForEachOddOrderNumber_HaveGreenColor()
        {
            var oddOrder = new Mock<ProductionOrder>();
            oddOrder.Setup(x => x.Number).Returns(1);
            var evenOrder = new Mock<ProductionOrder>();
            evenOrder.Setup(x => x.Number).Returns(2);
            var boardsControllerMock =
                _boardsControllerBuilder.CreateBoardsControllerWithTrackerProductionOrderFakes(new[] {oddOrder, evenOrder});

            var jsonResult = boardsControllerMock.GetOrderReadinessToStationary(1);
            var order = jsonResult.Value as IOrder;
            var evenOrderColor = order.ClientOrders
                .Where(x => x.OrderNumber == 2)
                .Select(s => s.Color)
                .First();

            Assert.AreEqual("green", evenOrderColor);
        }
    }
}
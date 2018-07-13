using System;
using System.Linq;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;
using Dodo.Tests.DSL;
using Dodo.Tracker.Contracts;
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
                _boardsControllerBuilder.CreateBoardsControllerWithTrackerProductionOrderFakes(new[] { order1, order2 });

            boardsControllerStub.GetOrderReadinessToStationary(1);

            order1.Verify(x => x.Number, Times.Once);
            order2.Verify(x => x.Number, Times.Once);
        }

        // State
        [Test]
        public void GetOrderReadinessToStationary_ShouldInResultJsonForEachOddOrderNumber_HaveRedColor()
        {
            var oddOrder = Mock.Of<ProductionOrder>(x => x.Number == 1);
            var evenOrder = Mock.Of<ProductionOrder>(x => x.Number == 2);
            var boardsControllerMock =
                _boardsControllerBuilder.CreateBoardsControllerWithTrackerProductionOrderFakes(new[] {oddOrder, evenOrder});

            var order = boardsControllerMock.GetOrderReadinessToStationary(1).Value as IOrder;

            var oddClientOrder = order.ClientOrders.First(x => x.OrderNumber == 1);
            Assert.AreEqual("red", oddClientOrder.Color);
        }

        // State
        [Test]
        public void GetOrderReadinessToStationary_ShouldInResultJsonForEachOddOrderNumber_HaveGreenColor()
        {
            var oddOrder = Mock.Of<ProductionOrder>(x => x.Number == 1);
            var evenOrder = Mock.Of<ProductionOrder>(x => x.Number == 2);
            var boardsControllerMock =
                _boardsControllerBuilder.CreateBoardsControllerWithTrackerProductionOrderFakes(new[] {oddOrder, evenOrder});

            var order = boardsControllerMock.GetOrderReadinessToStationary(1).Value as IOrder;

            var evenClientOrder = order.ClientOrders.First(x => x.OrderNumber == 2);
            Assert.AreEqual("green", evenClientOrder.Color);
        }
    }
}
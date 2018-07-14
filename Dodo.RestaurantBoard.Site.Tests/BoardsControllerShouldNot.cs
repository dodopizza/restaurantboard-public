using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardsControllerShouldNot
    {
        [Fact]
        public void CallGetPizzeriaOrCache_IfDepartmentFoundedByUnitOrCache()
        {
            var departmentsStructureServiceMock = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceMock.Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>())).Returns(() => null);
            var boardsController = new BoardsController(
                departmentsStructureServiceMock.Object,
                null,
                null,
                null,
                null,
                null);
            
            Assert.Throws<ArgumentException>(() =>
                boardsController.OrdersReadinessToStationary(200));

            departmentsStructureServiceMock.Verify(foo => foo.GetPizzeriaOrCache(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void CallGetIcons_IfClientTreatmentIsNotRandomImage()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(() => new Pizzeria(29, new Uuid("000D3A240C719A8711E68ABA13F83227"), "Сык-1", string.Empty,
                    string.Empty, UnitApprove.Approved, UnitState.Open, 2, new Uuid("000D3A240C719A8711E68ABA13FC4A39"),
                    1, null, 100, DateTime.MinValue, "Gay", true, 1, 1, ClientTreatment.DefaultName, true,
                    new PizzeriaFormat(0, string.Empty, string.Empty)));
            var trackerClientStub = new Mock<ITrackerClient>();
            trackerClientStub.Setup(x =>
                    x.GetOrdersByType(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<OrderState[]>(),
                        It.IsAny<int>()))
                .Returns(() => new ProductionOrder[]
                {
                    new ProductionOrder
                    {
                        Id = 55,
                        Number = 3,
                        ClientName = "Пупа"
                    }
                });
            var clientsServiceMock = new Mock<IClientsService>();
            var boardsController = new BoardsController(
                departmentsStructureServiceStub.Object,
                clientsServiceMock.Object,
                null,
                trackerClientStub.Object,
                null,
                null);

            boardsController.GetOrderReadinessToStationary(132);

            clientsServiceMock.Verify(foo => foo.GetIcons(), Times.Never());
        }
    }
}
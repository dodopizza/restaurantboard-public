using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Tests.Factories;
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
            departmentsStructureServiceMock
                .Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(() => null);
            var boardsController = BoardsControllerFactory.CreateMock(
                departmentsStructureService: departmentsStructureServiceMock)
                .Object;

            Assert.Throws<ArgumentException>(() =>
                boardsController.OrdersReadinessToStationary(200));

            departmentsStructureServiceMock.Verify(foo => foo.GetPizzeriaOrCache(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void CallGetIcons_IfClientTreatmentIsNotRandomImage()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            var pizzeria = PizzeriaFactory.CreatePizzeria(clientTreatment: ClientTreatment.DefaultName);
            departmentsStructureServiceStub
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(() => pizzeria);
            var trackerClientStub = new Mock<ITrackerClient>();
            trackerClientStub
                .Setup(x =>
                    x.GetOrdersByType(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<OrderState[]>(), It.IsAny<int>()))
                .Returns(() => new ProductionOrder[0]);
            var clientsServiceMock = new Mock<IClientsService>();
            var boardsController = BoardsControllerFactory.CreateMock(
                departmentsStructureService: departmentsStructureServiceStub,
                clientsService: clientsServiceMock,
                trackerClient: trackerClientStub
                )
                .Object;

            boardsController.GetOrderReadinessToStationary(132);

            clientsServiceMock.Verify(foo => foo.GetIcons(), Times.Never());
        }
    }
}
using System.Linq;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace Dodo.Tests.DSL
{
    public class BoardsControllerBuilder
    {
        private readonly ObjectMother _objectMother = new ObjectMother();

        public BoardsController CreateBoardsControllerWithDepartmentService(
            IDepartmentsStructureService departmentsStructureService)
        {
            var clientsServiceDummy = new Mock<IClientsService>();
            var managementServiceDummy = new Mock<IManagementService>();
            var trackerClientDummy = new Mock<ITrackerClient>();
            var hostingEnvironmentDummy = new Mock<IHostingEnvironment>();

            return new BoardsController(
                departmentsStructureService: departmentsStructureService,
                clientsService: clientsServiceDummy.Object,
                managementService: managementServiceDummy.Object,
                trackerClient: trackerClientDummy.Object,
                hostingEnvironment: hostingEnvironmentDummy.Object
            );
        }

        public BoardsController CreateBoardsControllerWithTrackerProductionOrderFakes(ProductionOrder[] trackerOrderFakes)
        {
            var pizzeriaStub = _objectMother.CreatePizzeria();

            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetPizzeriaOrCache(1))
                .Returns(pizzeriaStub);

            var trackerClientStub = new Mock<ITrackerClient>();
            trackerClientStub
                .Setup(x => x.GetOrdersByType(pizzeriaStub.Uuid, OrderType.Stationary, new[] {OrderState.OnTheShelf}, 16))
                .Returns(trackerOrderFakes.ToArray());

            var clientsServiceDummy = new Mock<IClientsService>();
            var managementServiceDummy = new Mock<IManagementService>();
            var hostingEnvironmentDummy = new Mock<IHostingEnvironment>();

            return new BoardsController(
                departmentsStructureService: departmentsStructureServiceStub.Object,
                clientsService: clientsServiceDummy.Object,
                managementService: managementServiceDummy.Object,
                trackerClient: trackerClientStub.Object,
                hostingEnvironment: hostingEnvironmentDummy.Object
            );
        }

        public BoardsController CreateBoardsControllerWithTrackerProductionOrderFakes(
            Mock<ProductionOrder>[] trackerOrderFakeObjects)
        {
            return CreateBoardsControllerWithTrackerProductionOrderFakes(
                trackerOrderFakeObjects.Select(x => x.Object).ToArray());
        }

        public BoardsController CreateBoardsControllerWithDepartmentServiceAndManagementService(
            IDepartmentsStructureService departmentsStructureService,
            IManagementService managementService)
        {
            var clientsServiceDummy = new Mock<IClientsService>();
            var trackerClientDummy = new Mock<ITrackerClient>();
            var hostingEnvironmentDummy = new Mock<IHostingEnvironment>();

            return new BoardsController(
                departmentsStructureService: departmentsStructureService,
                clientsService: clientsServiceDummy.Object,
                managementService: managementService,
                trackerClient: trackerClientDummy.Object,
                hostingEnvironment: hostingEnvironmentDummy.Object
            );
        }
    }
}
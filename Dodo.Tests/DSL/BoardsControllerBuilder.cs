using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace Dodo.Tests.DSL
{
    public class BoardsControllerBuilder
    {
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

        public BoardsController CreateBoardsControllerWithDepartmentServiceAndTrackerClient(
            IDepartmentsStructureService departmentsStructureService,
            ITrackerClient trackerClient)
        {
            var clientsServiceDummy = new Mock<IClientsService>();
            var managementServiceDummy = new Mock<IManagementService>();
            var hostingEnvironmentDummy = new Mock<IHostingEnvironment>();

            return new BoardsController(
                departmentsStructureService: departmentsStructureService,
                clientsService: clientsServiceDummy.Object,
                managementService: managementServiceDummy.Object,
                trackerClient: trackerClient,
                hostingEnvironment: hostingEnvironmentDummy.Object
            );
        }
    }
}
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.Factories
{
    public static class BoardsControllerFactory
    {
        public static Mock<BoardsController> CreateMock(
            Mock<IDepartmentsStructureService> departmentsStructureService = null,
            Mock<IClientsService> clientsService = null,
            Mock<ITrackerClient> trackerClient = null,
            Mock<IManagementService> managementService = null
            )
        {
            departmentsStructureService = departmentsStructureService ?? new Mock<IDepartmentsStructureService>();
            clientsService = clientsService ?? new Mock<IClientsService>();
            trackerClient = trackerClient ?? new Mock<ITrackerClient>();
            managementService = managementService ?? new Mock<IManagementService>();

            var hostingEnvironment = new Mock<IHostingEnvironment>();
            var fileService = new Mock<IFileService>();

            var boardsControllerMock = new Mock<BoardsController>(
                departmentsStructureService.Object,
                clientsService.Object,
                managementService.Object,
                trackerClient.Object,
                hostingEnvironment.Object,
                fileService.Object)
            {
                CallBase = true
            };

            return boardsControllerMock;
        }
    }
}

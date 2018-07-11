using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using Xunit;

namespace Dodo.Tests
{
    public class ApiControllerTests
    {
        [Fact]
        public void OnGetPizzeria_GetPizzeriaOrCacheFromDepartmentsStructureService()
        {
            var departmentsStructureServiceMock = new Mock<IDepartmentsStructureService>();     

            departmentsStructureServiceMock.Setup(d => d.GetPizzeriaOrCache(It.IsAny<int>()));

            var apiController = GetTestApiController(
              departmentsStructureService: departmentsStructureServiceMock.Object);

            apiController.GetPizzeria(5);

            departmentsStructureServiceMock.Verify(d => d.GetPizzeriaOrCache(5), Times.Once);
        }

        [Fact]
        public void OnGetAllOrders_GetAllOrdersFromTrackerClient()
        {
            var trackerClientMock = new Mock<ITrackerClient>();

            //trackerClientMock.Setup(t => t.GetAllOrders());

            var apiController = GetTestApiController(
              trackerClient: trackerClientMock.Object);

            apiController.GetAllOrders();

            trackerClientMock.Verify(t => t.GetAllOrders(), Times.Once);
        }

        public ApiController GetTestApiController(
             IDepartmentsStructureService departmentsStructureService = null,
            IClientsService clientsService = null,
            IManagementService managementService = null,
            ITrackerClient trackerClient = null,
            IHostingEnvironment hostingEnvironment =null)
        {
            return new ApiController(
                departmentsStructureService ?? new Mock<IDepartmentsStructureService>().Object,
                clientsService ?? new Mock<IClientsService>().Object,
                managementService ?? new Mock<IManagementService>().Object,
                trackerClient ?? new Mock<ITrackerClient>().Object,
                hostingEnvironment ?? new Mock<IHostingEnvironment>().Object
                );
        }
    }
}

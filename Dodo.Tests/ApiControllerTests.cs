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
            var clientsServiceStub = new Mock<IClientsService>();
            var managementServiceStub = new Mock<IManagementService>();
            var trackerClientStub = new Mock<ITrackerClient>();
            var hostingEnvironmentStub = new Mock<IHostingEnvironment>();

            departmentsStructureServiceMock.Setup(d => d.GetPizzeriaOrCache(It.IsAny<int>()));

            var apiController = new ApiController(
                departmentsStructureServiceMock.Object,
                clientsServiceStub.Object,
                managementServiceStub.Object,
                trackerClientStub.Object,
                hostingEnvironmentStub.Object);

            apiController.GetPizzeria(5);

            departmentsStructureServiceMock.Verify(d => d.GetPizzeriaOrCache(5), Times.Once);
        }
    }
}

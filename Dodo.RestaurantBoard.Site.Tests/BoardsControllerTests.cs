using System;
using Moq;
using NUnit.Framework;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Hosting;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardsControllerTests
    {
        [Test]
        public void OrdersReadinessToStationary_ThrowsArgumentException_WhenDepartmentIsNull()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub.Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(() => null);
            var dummyClientsService = new Mock<IClientsService>();
            var dummyManagementService = new Mock<IManagementService>();
            var dummyTrackerClient = new Mock<ITrackerClient>();
            var dummyHostingEnvironment = new Mock<IHostingEnvironment>();
            var boardsController = new BoardsController(departmentsStructureServiceStub.Object,
                dummyClientsService.Object,
                dummyManagementService.Object,
                dummyTrackerClient.Object,
                dummyHostingEnvironment.Object);            

            Assert.Throws<ArgumentException>(() => boardsController.OrdersReadinessToStationary(42));
        }
    }
}

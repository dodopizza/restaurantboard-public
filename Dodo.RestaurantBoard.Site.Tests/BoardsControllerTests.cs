using System;
using NUnit.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoFixture;
using Dodo.Core.Services;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardsControllerTests
    {
        // Behaviour test
        [Test]
        public void OrdersReadinessToStationary_ThrowsArgumentException_WhenDepartmentIsNull()
        {
            var stubDepartmentsStructureService = new Mock<IDepartmentsStructureService>();
            stubDepartmentsStructureService.Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(() => null);
            var boardsController = CreateController(stubDepartmentsStructureService.Object);

            Assert.Throws<ArgumentException>(() => boardsController.OrdersReadinessToStationary(42));
        }

        // State test
        [Test]
        public void OrdersReadinessToStationary_ReturnsViewResult_WhenDepartmentIsNotNull()
        {
            var fixture = new Fixture();
            var stubDepartmentsStructureService = new Mock<IDepartmentsStructureService>();
            stubDepartmentsStructureService.Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(fixture.Create<CityDepartment>());
            stubDepartmentsStructureService.Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(fixture.Create<Pizzeria>());
            var boardsController = CreateController(stubDepartmentsStructureService.Object);

            var result = boardsController.OrdersReadinessToStationary(42);

            Assert.IsInstanceOf<ViewResult>(result);
        }

        private BoardsController CreateController(IDepartmentsStructureService departmentsStructureService)
        {
            var dummyClientsService = new Mock<IClientsService>().Object;
            var dummyManagementService = new Mock<IManagementService>().Object;
            var dummyTrackerClient = new Mock<ITrackerClient>().Object;
            var dummyHostingEnvironment = new Mock<IHostingEnvironment>().Object;

            return new BoardsController(departmentsStructureService,
                dummyClientsService,
                dummyManagementService,
                dummyTrackerClient,
                dummyHostingEnvironment);
        }
    }
}

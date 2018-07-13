using System;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Finance;
using Moq;
using NUnit.Framework;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using Dodo.Core.DomainModel.Departments.Units;

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
            var boardsController = CreateController(departmentsStructureServiceStub.Object);

            Assert.Throws<ArgumentException>(() => boardsController.OrdersReadinessToStationary(42));
        }

        [Test]
        public void OrdersReadinessToStationary_ReturnsViewResult_WhenDepartmentIsNotNull()
        {
            var fixture = new Fixture();
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub.Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(fixture.Create<CityDepartment>());
            departmentsStructureServiceStub.Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(fixture.Create<Pizzeria>());
            var boardsController = CreateController(departmentsStructureServiceStub.Object);

            var result = boardsController.OrdersReadinessToStationary(42);

            Assert.IsInstanceOf<ViewResult>(result);
        }

        private BoardsController CreateController(IDepartmentsStructureService departmentsStructureService)
        {
            var dummyClientsService = new Mock<IClientsService>();
            var dummyManagementService = new Mock<IManagementService>();
            var dummyTrackerClient = new Mock<ITrackerClient>();
            var dummyHostingEnvironment = new Mock<IHostingEnvironment>();

            return new BoardsController(departmentsStructureService,
                dummyClientsService.Object,
                dummyManagementService.Object,
                dummyTrackerClient.Object,
                dummyHostingEnvironment.Object);
        }
    }
}

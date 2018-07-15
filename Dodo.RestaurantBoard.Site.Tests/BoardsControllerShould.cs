using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Management;
using Dodo.Core.DomainModel.Products;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Tests.Factories;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardsControllerShould
    {
        [Fact]
        public void ThrowArgumentException_IfGetDepartmentByUnitOrCacheReturnNull()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(() => null);
            var boardsController = BoardsControllerFactory.CreateMock(
                departmentsStructureService: departmentsStructureServiceStub)
                .Object;

            var ex = Assert.Throws<ArgumentException>(() =>
                boardsController.OrdersReadinessToStationary(200));

            Assert.Equal("unitId", ex.Message);
        }

        [Fact]
        public void ThrowNullReferenceException_IfGetPizzeriaOrCacheReturnNull()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(() => new CityDepartment());
            departmentsStructureServiceStub
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(() => null);
            var boardsController = BoardsControllerFactory.CreateMock(
                 departmentsStructureService: departmentsStructureServiceStub)
                 .Object;

            var ex = Assert.Throws<NullReferenceException>(() =>
                boardsController.OrdersReadinessToStationary(200));

            Assert.Equal("Object reference not set to an instance of an object.", ex.Message);
        }

        [Fact]
        public void ReturnBannerUrls_IfGetAvailableBannersReturnNotEmptyArray()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetDepartmentOrCache<CityDepartment>(It.IsAny<int>()))
                .Returns(() => new CityDepartment
                {
                    MenuSpecializationType = MenuSpecializationType.European
                });
            var managementServiceStub = new Mock<IManagementService>();
            managementServiceStub
                .Setup(x => x.GetAvailableBanners(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => new[]
                {
                    new RestaurantBanner()
                    {
                        MenuSpecializationTypes = new []
                        {
                            MenuSpecializationType.European
                        },
                        Url = "ya.ru",
                        DisplayTime = 15
                    }
                });

            var boardsController = BoardsControllerFactory.CreateMock(
                departmentsStructureService: departmentsStructureServiceStub,
                managementService: managementServiceStub)
                .Object;

            var res = (dynamic[])boardsController.GetRestaurantBannerUrl(1, 2, 3).Value;

            Assert.Single(res);
            Assert.Equal("ya.ru", res[0].GetType().GetProperty("BannerUrl").GetValue(res[0], null));
            Assert.Equal(15000, res[0].GetType().GetProperty("DisplayTime").GetValue(res[0], null));
        }

        [Fact]
        public void ReturnDefaultBannerUrls_IfGetAvailableBannersReturnEmptyArray()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetDepartmentOrCache<CityDepartment>(It.IsAny<int>()))
                .Returns(() => new CityDepartment());
            var managementServiceStub = new Mock<IManagementService>();
            managementServiceStub
                .Setup(x => x.GetAvailableBanners(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => new RestaurantBanner[0]);
            var boardsController = BoardsControllerFactory.CreateMock(
                departmentsStructureService: departmentsStructureServiceStub,
                managementService: managementServiceStub);
            boardsController.Setup(x => x.GetLocalizedContext()).Returns(() => "Tracking-Scoreboard-Empty.jpg");

            var res = (dynamic[])boardsController.Object.GetRestaurantBannerUrl(1, 2, 3).Value;

            Assert.Single(res);
            Assert.Equal(60000, res[0].GetType().GetProperty("DisplayTime").GetValue(res[0], null));
            Assert.Equal("Tracking-Scoreboard-Empty.jpg", res[0].GetType().GetProperty("BannerUrl").GetValue(res[0], null));
        }

        [Fact]
        public void ReturnDefaultBannerUrls_IfNotBannesrForDepartments()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetDepartmentOrCache<CityDepartment>(It.IsAny<int>()))
                .Returns(() => new CityDepartment
                {
                    MenuSpecializationType = MenuSpecializationType.European
                });
            var managementServiceStub = new Mock<IManagementService>();
            managementServiceStub
                .Setup(x => x.GetAvailableBanners(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => new[]
                {
                    new RestaurantBanner()
                    {
                        MenuSpecializationTypes = new []
                        {
                            MenuSpecializationType.HalfHalal
                        }
                    }
                });
            var boardsController = BoardsControllerFactory.CreateMock(
                departmentsStructureService: departmentsStructureServiceStub,
                managementService: managementServiceStub);
            boardsController.Setup(x => x.GetLocalizedContext()).Returns(() => "Tracking-Scoreboard-Empty.jpg");

            var res = (dynamic[])boardsController.Object.GetRestaurantBannerUrl(1, 2, 3).Value;

            Assert.Single(res);
            Assert.Equal(60000, res[0].GetType().GetProperty("DisplayTime").GetValue(res[0], null));
            Assert.Equal("Tracking-Scoreboard-Empty.jpg", res[0].GetType().GetProperty("BannerUrl").GetValue(res[0], null));
        }

        [Fact]
        public void CallGetIcons_IfClientTreatmentIsRandomImage()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            var pizzeria = PizzeriaFactory.CreatePizzeria(clientTreatment: ClientTreatment.RandomImage);
            departmentsStructureServiceStub
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(pizzeria);
            var trackerClientStub = new Mock<ITrackerClient>();
            trackerClientStub
                .Setup(x => x.GetOrdersByType(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<OrderState[]>(), It.IsAny<int>()))
                .Returns(() => new ProductionOrder[0]);
            var clientsServiceMock = new Mock<IClientsService>();
            clientsServiceMock
                .Setup(x => x.GetIcons())
                .Returns(() => new ClientIcon[0]);
            var boardsController = BoardsControllerFactory.CreateMock(
                departmentsStructureService: departmentsStructureServiceStub,
                clientsService: clientsServiceMock,
                trackerClient: trackerClientStub)
                .Object;

            boardsController.GetOrderReadinessToStationary(132);

            clientsServiceMock.Verify(foo => foo.GetIcons(), Times.Once());
        }

        [Fact]
        public void CallGetPizzeriaOrCache_IfDepartmentFoundedByUnitOrCache()
        {
            var departmentsStructureServiceMock = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceMock
                .Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(() => new CityDepartment()
                {
                    Country = new Country(777, string.Empty, string.Empty, null, string.Empty, Currency.Ruble, string.Empty)
                });
            var pizzeria = PizzeriaFactory.CreatePizzeria(clientTreatment: ClientTreatment.RandomImage);
            departmentsStructureServiceMock
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(() => pizzeria);
            var boardsController = BoardsControllerFactory.CreateMock(
                departmentsStructureService: departmentsStructureServiceMock)
                .Object;

            boardsController.OrdersReadinessToStationary(132);

            departmentsStructureServiceMock.Verify(foo => foo.GetPizzeriaOrCache(It.IsAny<int>()), Times.Once());
        }
    }
}
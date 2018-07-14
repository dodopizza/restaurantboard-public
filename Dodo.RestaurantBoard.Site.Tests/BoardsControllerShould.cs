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
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Microsoft.AspNetCore.Hosting;
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
            var boardsController = new BoardsController(
                departmentsStructureServiceStub.Object,
                null,
                null,
                null,
                null,
                null);

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
                .Returns(() =>
                new CityDepartment
                {
                    Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty)
                });
            departmentsStructureServiceStub
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(() => null);
            var boardsController = new BoardsController(
                departmentsStructureServiceStub.Object,
                null,
                null,
                null,
                null,
                null);

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
                    Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty),
                    MenuSpecializationType = MenuSpecializationType.European
                });
            var managementServiceStub = new Mock<IManagementService>();
            managementServiceStub
                .Setup(x => x.GetAvailableBanners(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => new RestaurantBanner[]
                {
                    new RestaurantBanner()
                    {
                        MenuSpecializationTypes = new []
                        {
                            MenuSpecializationType.European, MenuSpecializationType.HalfHalal
                        },
                        Url = "ya.ru",
                        DisplayTime = 15
                    }
                });
            var boardsController = new BoardsController(
                departmentsStructureServiceStub.Object,
                null,
                managementServiceStub.Object,
                null,
                null,
                null);

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
                .Returns(() => new CityDepartment
                {
                    Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty),
                    MenuSpecializationType = MenuSpecializationType.European
                });
            var managementServiceStub = new Mock<IManagementService>();
            managementServiceStub
                .Setup(x => x.GetAvailableBanners(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => new RestaurantBanner[0]);
            var hostingEnvironmentStub = new Mock<IHostingEnvironment>();
            hostingEnvironmentStub.SetupGet(x => x.WebRootPath).Returns("/usr/local/sbin:/usr/local/");
            var fileServiceStub = new Mock<IFileService>();
            fileServiceStub.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            var boardsController = new BoardsController(
                departmentsStructureServiceStub.Object,
                null,
                managementServiceStub.Object,
                null,
                hostingEnvironmentStub.Object,
                fileServiceStub.Object);

            var res = (dynamic[])boardsController.GetRestaurantBannerUrl(1, 2, 3).Value;

            Assert.Single(res);
            Assert.Equal(60000, res[0].GetType().GetProperty("DisplayTime").GetValue(res[0], null));
            Assert.Equal("//LocalizedResources/ru/Tracking-Scoreboard-Empty.jpg", res[0].GetType().GetProperty("BannerUrl").GetValue(res[0], null));
        }

        [Fact]
        public void ReturnDefaultBannerUrls_IfNotBannesrForDepartments()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetDepartmentOrCache<CityDepartment>(It.IsAny<int>()))
                .Returns(() => new CityDepartment
                {
                    Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty),
                    MenuSpecializationType = MenuSpecializationType.European
                });
            var managementServiceStub = new Mock<IManagementService>();
            managementServiceStub
                .Setup(x => x.GetAvailableBanners(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => new RestaurantBanner[]
                {
                    new RestaurantBanner()
                    {
                        MenuSpecializationTypes = new []
                        {
                            MenuSpecializationType.HalfHalal
                        },
                        Url = "ya.ru",
                        DisplayTime = 15
                    }
                });
            var hostingEnvironmentStub = new Mock<IHostingEnvironment>();
            hostingEnvironmentStub.SetupGet(x => x.WebRootPath).Returns("/usr/local/sbin:/usr/local/");
            var fileServiceStub = new Mock<IFileService>();
            fileServiceStub.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            var boardsController = new BoardsController(
                departmentsStructureServiceStub.Object,
                null,
                managementServiceStub.Object,
                null,
                hostingEnvironmentStub.Object,
                fileServiceStub.Object);

            var res = (dynamic[])boardsController.GetRestaurantBannerUrl(1, 2, 3).Value;

            Assert.Single(res);
            Assert.Equal(60000, res[0].GetType().GetProperty("DisplayTime").GetValue(res[0], null));
            Assert.Equal("//LocalizedResources/ru/Tracking-Scoreboard-Empty.jpg", res[0].GetType().GetProperty("BannerUrl").GetValue(res[0], null));
        }

        [Fact]
        public void CallGetIcons_IfClientTreatmentIsRandomImage()
        {
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(() => new Pizzeria(29, new Uuid("000D3A240C719A8711E68ABA13F83227"), "Сык-1", string.Empty,
                    string.Empty, UnitApprove.Approved, UnitState.Open, 2, new Uuid("000D3A240C719A8711E68ABA13FC4A39"),
                    1, null, 100, DateTime.MinValue, "Gay", true, 1, 1, ClientTreatment.RandomImage, true,
                    new PizzeriaFormat(0, string.Empty, string.Empty)));
            var trackerClientStub = new Mock<ITrackerClient>();
            trackerClientStub.Setup(x =>
                    x.GetOrdersByType(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<OrderState[]>(),
                        It.IsAny<int>()))
                .Returns(() => new ProductionOrder[]
                {
                    new ProductionOrder
                    {
                        Id = 55,
                        Number = 3,
                        ClientName = "Пупа"
                    }
                });
            var clientsServiceMock = new Mock<IClientsService>();
            clientsServiceMock
                .Setup(x => x.GetIcons())
                .Returns(() => new ClientIcon[0]);
            var boardsController = new BoardsController(
                departmentsStructureServiceStub.Object,
                clientsServiceMock.Object,
                null,
                trackerClientStub.Object,
                null,
                null);

            boardsController.GetOrderReadinessToStationary(132);

            clientsServiceMock.Verify(foo => foo.GetIcons(), Times.Once());
        }

        [Fact]
        public void CallGetPizzeriaOrCache_IfDepartmentFoundedByUnitOrCache()
        {
            var departmentsStructureServiceMock = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceMock.Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>())).Returns(() =>
                new CityDepartment
                {
                    Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty)
                });
            departmentsStructureServiceMock.Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>())).Returns(() =>
                new Pizzeria(29, new Uuid("000D3A240C719A8711E68ABA13F83227"), "Сык-1", string.Empty, string.Empty,
                    UnitApprove.Approved, UnitState.Open, 2, new Uuid("000D3A240C719A8711E68ABA13FC4A39"), 1,
                    null, 100, DateTime.MinValue, "Gay", true, 1, 1, ClientTreatment.Name, true,
                    new PizzeriaFormat(0, string.Empty, string.Empty)));

            var boardsController = new BoardsController(
                departmentsStructureServiceMock.Object,
                null,
                null,
                null,
                null,
                null);

            boardsController.OrdersReadinessToStationary(132);

            departmentsStructureServiceMock.Verify(foo => foo.GetPizzeriaOrCache(It.IsAny<int>()), Times.Once());
        }
    }
}
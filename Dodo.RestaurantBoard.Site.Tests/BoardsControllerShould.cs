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
using Dodo.RestaurantBoard.Site.Models;
using Dodo.RestaurantBoard.Site.Tests.Dsl;
using Dodo.RestaurantBoard.Site.Tests.Dsl.Asserts;
using Dodo.RestaurantBoard.Site.Tests.Dsl.Verifies;
using Dodo.RestaurantBoard.Site.Tests.Factories;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardsControllerShould
    {
        [Fact]
        public void ThrowArgumentException_IfGetDepartmentByUnitOrCacheReturnNull()
        {
            var boardsController = Create.BoardController
                .With(Create
                    .DepartmentsStructureService
                    .WithoutDepartments()
                    .Please())
                .Please();

            AssertIt(boardsController)
                .ExecuteOrdersReadinessToStationary<ArgumentException>()
                .CousesErrorWithParamName("unitId");
        }

        [Fact]
        public void ThrowNullReferenceException_IfGetPizzeriaOrCacheReturnNull()
        {
            var boardsController = Create.BoardController
                .With(Create
                    .DepartmentsStructureService
                    .WithDepartment()
                    .WithOutPizzeria()
                    .Please())
                .Please();


            AssertIt(boardsController)
                .ExecuteOrdersReadinessToStationary<NullReferenceException>()
                .CousesErrorWithMessage(message: "Object reference not set to an instance of an object.");
        }

        [Fact]
        public void ReturnBannerUrls_IfGetAvailableBannersReturnNotEmptyArray()
        {
            var cityDepartment = Create
                .CityDepartment
                .WithMenuSpecializationTypeAsEuropean()
                .Please();
            var restaurantBanner = Create
                .RestaurantBanner
                .WithMenuSpecializationTypeAsEuropean()
                .Please();
            var boardsController = Create
                .BoardController
                .With(Create.DepartmentsStructureService
                    .WithDepartment(cityDepartment)
                    .Please())
                .With(Create.ManagementServiceMock
                    .WithAvailableBanner(restaurantBanner)
                    .Please())
                .Please();

            var bannerModels = (BannerModel[])boardsController.GetRestaurantBannerUrl(1, 2, 3).Value;

            AssertIt(bannerModels).OnlyOne().SameAs(restaurantBanner);
        }

        [Fact]
        public void CallGetIcons_IfClientTreatmentIsRandomImage()
        {
            var pizzeria = Create
                .Pizzeria
                .WithClientTreatmentAsRandomImage()
                .Please();
            var departmentsStructureService = Create
                .DepartmentsStructureService
                .WithPizzeria(pizzeria)
                .Please();
            var trackerClient = Create
                .TrackerClient
                .WithEmptyOrderList()
                .Please();
            var clientsServiceMock = Create
                .ClientsService
                .WithEmptyClientIconList()
                .MockPlease();
            var boardsController = Create.BoardController
                .With(departmentsStructureService)
                .With(trackerClient)
                .With(clientsServiceMock.Object)
                .Please();

            boardsController.GetOrderReadinessToStationary(132);

            VerifyIt(clientsServiceMock).CallGetIconsOnce();

        }

        [Fact]
        public void CallGetPizzeriaOrCache_IfDepartmentFoundedByUnitOrCache()
        {
            var cityDepartment = Create.CityDepartment.WithContry().Please();
            var pizzeria = Create.Pizzeria.WithClientTreatmentAsRandomImage().Please();
            var departmentsStructureServiceMock =
                Create.DepartmentsStructureService
                    .WithDepartment(cityDepartment)
                    .WithPizzeria(pizzeria)
                    .MockPlease();
            var boardsController = Create.BoardController.With(departmentsStructureServiceMock.Object).Please();

            boardsController.OrdersReadinessToStationary(132);

            VerifyIt(departmentsStructureServiceMock).CallGetPizzeriaOrCacheOnce();
        }

        private DepartmentsStructureServiceMockVerify VerifyIt(Mock<IDepartmentsStructureService> departmentsStructureServiceMock)
        {
            return new DepartmentsStructureServiceMockVerify(departmentsStructureServiceMock);
        }

        public static ControllerAssert AssertIt(BoardsController controller)
        {
            return new ControllerAssert(controller);
        }
        public static BannersAssert AssertIt(BannerModel[] banners)
        {
            return new BannersAssert(banners);
        }

        public static ClientsServiceMockVerify VerifyIt(Mock<IClientsService> clientsService)
        {
            return new ClientsServiceMockVerify(clientsService);
        }
    }


}
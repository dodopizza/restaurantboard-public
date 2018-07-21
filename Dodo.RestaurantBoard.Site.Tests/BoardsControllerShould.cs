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
using Dodo.RestaurantBoard.Site.Tests.Dsl.Extensions;
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

            AssertThat(boardsController)
                .Throws<ArgumentException>()
                .On(() => boardsController.OrdersReadinessToStationary(200));
        }

        [Fact]
        public void ThrowNullReferenceException_IfGetPizzeriaOrCacheReturnNull()
        {
            var boardsController = Create.BoardController
                .With(Create
                    .DepartmentsStructureService
                    .WithDepartment()
                    .WithoutPizzeria()
                    .Please())
                .Please();

            AssertThat(boardsController)
                .Throws<NullReferenceException>()
                .On(() => boardsController.OrdersReadinessToStationary(200));
        }

        [Fact]
        public void ReturnBannerModel_OnGetRestaurantBannerUrl()
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

            AssertThat(bannerModels).Contains(new BannerModel(restaurantBanner));
        }

        [Fact]
        public void CallClientsServiceGetIcons_IfPizzeriavClientTreatmentIsRandomImage()
        {
            var pizzeria = Create
                .Pizzeria
                .WithClientTreatmentAsRandomImage()
                .Please();
            var departmentsStructureService = Create
                .DepartmentsStructureService
                .WithPizzeria(pizzeria)
                .Please();
            var clientsServiceMock = Create
                .ClientsService
                .WithEmptyClientIconList()
                .MockPlease();
            var boardsController = Create.BoardController
                .With(departmentsStructureService)
                .With(clientsServiceMock.Object)
                .Please();

            boardsController.GetOrderReadinessToStationary(132);

            VerifyThat(clientsServiceMock).CallGetIcons(1.Times());

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

            VerifyThat(departmentsStructureServiceMock).CallGetPizzeriaOrCache(1.Times());
        }

        private DepartmentsStructureServiceMockVerify VerifyThat(Mock<IDepartmentsStructureService> departmentsStructureServiceMock)
        {
            return new DepartmentsStructureServiceMockVerify(departmentsStructureServiceMock);
        }

        public static ControllerAssert AssertThat(BoardsController controller)
        {
            return new ControllerAssert(controller);
        }
        public static BannersAssert AssertThat(BannerModel[] banners)
        {
            return new BannersAssert(banners);
        }

        public static ClientsServiceMockVerify VerifyThat(Mock<IClientsService> clientsService)
        {
            return new ClientsServiceMockVerify(clientsService);
        }
    }


}
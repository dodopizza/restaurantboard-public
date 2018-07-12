using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Management.Organizations;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.Tracker.Contracts.Enums;
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
        [Fact]
        public void OnGetAllOrdersFromPizzeria_CallGetOrdersByTypeWithUuidFromDepartmentsStructureService()
        {
            var expectedUuid = new Uuid("11111111111111111111111111111111");

            var trackerClientMock = new Mock<ITrackerClient>();
            var departmentsStructureServiceStub = new Mock<IDepartmentsStructureService>();
            departmentsStructureServiceStub.Setup(d => d.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(CreatePizzeria(uuid: expectedUuid));
            var apiController = GetTestApiController(departmentsStructureService: departmentsStructureServiceStub.Object,
              trackerClient: trackerClientMock.Object);

            apiController.GetAllOrdersFromPizzeria(1);

            trackerClientMock.Verify(t => t.GetOrdersByType(
                    expectedUuid, 
                    It.IsAny<OrderType>(), 
                    It.IsAny<OrderState[]>(), 
                    It.IsAny<int>()), 
                Times.Once);
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

        private Pizzeria CreatePizzeria(
          Int32 id = 0, Uuid uuid = null, String name = "", String alias = "", String translitAlias = "", UnitApprove approve = new UnitApprove(), UnitState state = new UnitState(),
          Int32 departmentId = 0, Uuid departmentUuid = null, Int32 countryId = 0, OrganizationShortInfo organization = null, Double square = 0,
          DateTime? beginDateTimeWork = null, String orientation = "", Boolean? cardPaymentPickup = null, Decimal? coordinateX = null, Decimal? coordinateY = null,
          ClientTreatment clientTreatment = new ClientTreatment(), Boolean terminalAtCourier = false, PizzeriaFormat pizzeriaFormat = null)
        {
            return new Pizzeria(
                id, uuid, name, alias, translitAlias, approve, state,
                departmentId, departmentUuid, countryId, organization, square,
                beginDateTimeWork, orientation, cardPaymentPickup, coordinateX, coordinateY,
                clientTreatment, terminalAtCourier, pizzeriaFormat);
        }
    }
}

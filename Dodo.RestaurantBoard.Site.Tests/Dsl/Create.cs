using System.Collections.Generic;
using System.Text;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Site.Tests.Dsl.Builders;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl
{
    public static class Create
    {
        public static BoardControllerBuilder BoardController => new BoardControllerBuilder();
        public static DepartmentsStructureServiceMockBuilder DepartmentsStructureService
            => new DepartmentsStructureServiceMockBuilder();

        public static ManagementServiceMockBuilder ManagementServiceMock
            => new ManagementServiceMockBuilder();

        public static CityDepartmentBuilder CityDepartment => new CityDepartmentBuilder();

        public static PizzeriaBuilder Pizzeria => new PizzeriaBuilder();

        public static RestaurantBannerBuilder RestaurantBanner => new RestaurantBannerBuilder();

        public static TrackerClientMockBuilder TrackerClient => new TrackerClientMockBuilder();

        public static ClientsServiceMockBuilder ClientsService => new ClientsServiceMockBuilder();
    }

    public class ClientsServiceMockBuilder
    {
        private Mock<IClientsService> _service;

        public ClientsServiceMockBuilder()
        {
            _service = new Mock<IClientsService>();
        }

        public ClientsServiceMockBuilder WithEmptyClientIconList()
        {
            _service
                .Setup(x => x.GetIcons())
                .Returns(() => new ClientIcon[0]);

            return this;
        }

        public IClientsService Please()
        {
            return _service.Object;
        }

        public Mock<IClientsService> MockPlease()
        {
            return _service;
        }
    }
}

using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Site.Tests.Factories;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Builders
{
    public class BoardControllerBuilder
    {
        private IDepartmentsStructureService _departmentsStructureService;
        private IManagementService _managementService;
        private ITrackerClient _trackerClient;
        private IClientsService _clientsService;

        public BoardControllerBuilder()
        {
            _trackerClient = Create.TrackerClient.WithEmptyOrderList().Please();
        }

        public BoardControllerBuilder With(IDepartmentsStructureService departmentsStructureService)
        {
            _departmentsStructureService = departmentsStructureService;
            return this;
        }

        public BoardControllerBuilder With(IManagementService managementService)
        {
            _managementService = managementService;
            return this;
        }


        public BoardControllerBuilder With(ITrackerClient trackerClient)
        {
            _trackerClient = trackerClient;
            return this;
        }

        public BoardControllerBuilder With(IClientsService clientsService)
        {
            _clientsService = clientsService;
            return this;
        }

        public BoardsController Please()
        {
            return BoardsControllerFactory.CreateMock(
                departmentsStructureService: _departmentsStructureService,
                managementService: _managementService,
                trackerClient: _trackerClient,
                clientsService: _clientsService
            ).Object;
        }
    }
}
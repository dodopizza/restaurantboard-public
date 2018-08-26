using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;

namespace Dodo.RestaurantBoard.Test.DSL
{
    public class BoardsControllerBuilder
    {
        private IPizzeriaOrdersService _pizzeriaOrdersService;
        
        public BoardsControllerBuilder WithPizzeriaOrdersService(IPizzeriaOrdersService pizzeriaOrdersService)
        {
            _pizzeriaOrdersService = pizzeriaOrdersService;
            return this;
        }
        
        public BoardsController Please()
        {
            return new BoardsController(new DepartmentsStructureService(), new ClientService(), new ManagementService(), null, _pizzeriaOrdersService);
        }
    }
}
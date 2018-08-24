using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;

namespace Dodo.Tests.DSL
{
    public class BoardBuilder
    {
        private IPizzeriaOrdersService _pizzeriaOrdersService;

        public BoardBuilder With(IPizzeriaOrdersService pizzeriaOrdersService)
        {
            _pizzeriaOrdersService = pizzeriaOrdersService;
            return this;
        }

        public BoardsController Please()
        {
            return new BoardsController(clientsService: new ClientService(),
                managementService: null,
                hostingEnvironment: null,
                pizzeriaOrdersService: _pizzeriaOrdersService);
        }
    }
}
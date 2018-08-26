using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Site.Core.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    internal class BoardControllerBuilder
    {
        private ITrackerService _trackerService;

        public BoardControllerBuilder WithTrackerService(ITrackerService trackerService)
        {
            _trackerService = trackerService;
            return this;
        }

        public BoardsController Please()
        {
            var boardsController = new BoardsController(new DepartmentsStructureService(), null, null, _trackerService, null)
            {
                ControllerContext = GetControllerContext(),
            };

            return boardsController;
        }

        private ControllerContext GetControllerContext()
        {
            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            };
            controllerContext.HttpContext.Session = new Mock<ISession>().Object;

            return controllerContext;
        }
    }
}

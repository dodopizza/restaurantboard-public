using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    public class BoardContollerBuilder
    {
        private ITrackerClient _trackerClient;
        public BoardsController Build()
        {
            var controller = new BoardsController(
                new DepartmentsStructureService(), 
                new ClientService(), 
                new ManagementService(), 
                _trackerClient,
                null);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Session = new Mock<ISession>().Object;

            return  controller;
        }

        public BoardContollerBuilder WithTrackerClient(ITrackerClient client)
        {
            _trackerClient = client;
            return this;
        }
    }
}

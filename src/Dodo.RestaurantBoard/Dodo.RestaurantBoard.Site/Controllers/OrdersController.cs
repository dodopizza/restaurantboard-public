using System;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Dodo.RestaurantBoard.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ITrackerClient _trackerClient;

        public OrdersController(ITrackerClient trackerClient)
        {
            _trackerClient = trackerClient;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductionOrder order)
        {
            try
            {
                _trackerClient.AddOrder(order);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dodo.Tracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{unitUuid}/{type}")]
        public ActionResult<IEnumerable<ProductionOrder>> GetOrdersByType(
            [FromRoute] string unitUuid,
            [FromRoute] OrderType type,
            [FromQuery] OrderState[] states,
            [FromQuery] int limit)
        {
            var orders = new[]
            {
                new ProductionOrder
                {
                    Id = 55,
                    Number = 3,
                    ClientName = "Пупа",
                    ChangeDate = DateTime.Now.AddMinutes(-5),
                },
                new ProductionOrder
                {
                    Id = 56,
                    Number = 4,
                    ClientName = "Лупа",
                    ChangeDate = DateTime.Now.AddMinutes(-3),
                },
            };

            return orders;
        }
    }
}
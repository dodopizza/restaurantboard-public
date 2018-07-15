﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dodo.RestaurantBoard.Site.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private readonly IDepartmentsStructureService _departmentsStructureService;
        private readonly IClientsService _clientsService;
        private readonly IManagementService _managementService;
        private readonly ITrackerClient _trackerClient;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ApiController(
            IDepartmentsStructureService departmentsStructureService,
            IClientsService clientsService,
            IManagementService managementService,
            ITrackerClient trackerClient,
            IHostingEnvironment hostingEnvironment)
        {
            _departmentsStructureService = departmentsStructureService;
            _clientsService = clientsService;
            _managementService = managementService;
            _trackerClient = trackerClient;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("/pizzeria/{id}")]
        public JsonResult GetPizzeria(int id)
        {
            return Json(_departmentsStructureService.GetPizzeriaOrCache(id));
        }
      
        [HttpGet("/orders")]
        public JsonResult GetAllOrders()
        {
            return Json(_trackerClient.GetAllOrders());
        }

        public JsonResult GetAllOrdersFromPizzeria(int unitId)
        {
            var pizzeria = _departmentsStructureService.GetPizzeriaOrCache(unitId);

            var pizzeriaOrders = _trackerClient
                .GetOrdersByType(
                    pizzeria.Uuid, OrderType.Stationary, new[] { OrderState.OnTheShelf }, int.MaxValue)                
                .ToArray();

            return Json(pizzeriaOrders);
        }

       
    }
}

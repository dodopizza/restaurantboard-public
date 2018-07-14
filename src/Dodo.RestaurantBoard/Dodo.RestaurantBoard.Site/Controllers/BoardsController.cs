using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Models;
using Dodo.RestaurantBoard.Site.Models.DodoFM;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using ViewResult = Microsoft.AspNetCore.Mvc.ViewResult;

namespace Dodo.RestaurantBoard.Site.Controllers
{
    public class BoardsController : Controller
    {
        private readonly IDepartmentsStructureService _departmentsStructureService;
        private readonly IClientsService _clientsService;
        private readonly IManagementService _managementService;
        private readonly ITrackerClient _trackerClient;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileService _fileService;
        
        public BoardsController(
            IDepartmentsStructureService departmentsStructureService,
            IClientsService clientsService,
            IManagementService managementService,
            ITrackerClient trackerClient,
            IHostingEnvironment hostingEnvironment, 
            IFileService fileService
            )
        {
            _departmentsStructureService = departmentsStructureService;
            _clientsService = clientsService;
            _managementService = managementService;
            _trackerClient = trackerClient;
            _hostingEnvironment = hostingEnvironment;
            _fileService = fileService;
        }


        private int[] CurrentProductsIds
        {
            get
            {
                var currentProductsIds = HttpContext?.Session?.GetString("IdProductUnit");
                return !string.IsNullOrEmpty(currentProductsIds)
                    ? JsonConvert.DeserializeObject<int[]>(currentProductsIds)
                    : new int[0];
            }
            set
            {
                var serialized = JsonConvert.SerializeObject(value);
                HttpContext?.Session?.SetString("IdProductUnit", serialized);
            }
        }

        public ActionResult Index()
        {
            var unit = _departmentsStructureService.GetUnitOrCache(Uuid.Empty);

            return RedirectToAction("OrdersReadinessToStationary", new { unitId = unit.Id });
        }

        #region Ресторан.Готовность заказов

        public ViewResult OrdersReadinessToStationary(int unitId)
        {
            var department = _departmentsStructureService.GetDepartmentByUnitOrCache(unitId);
            if (department == null) throw new ArgumentException(nameof(unitId));

            var pizzeria = _departmentsStructureService.GetPizzeriaOrCache(unitId);

            bool isNewBoard = true;
            var model = new OrdersReadinessToStationaryModel(department.Id, department.Country.Id, unitId, isNewBoard, pizzeria.ClientTreatment);

            return View(model);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public JsonResult GetOrderReadinessToStationary(int unitId)
        {
            const int maxCountOrders = 16;

            var pizzeria = _departmentsStructureService.GetPizzeriaOrCache(unitId);

            var orders = _trackerClient
                .GetOrdersByType(pizzeria.Uuid, OrderType.Stationary, new[] { OrderState.OnTheShelf }, maxCountOrders)
                .Select(MapToRestaurantReadnessOrders)
                .ToArray();


            var clientTreatment = pizzeria.ClientTreatment;
            ClientIcon[] icons = { };
            if (clientTreatment == ClientTreatment.RandomImage)
            {
                icons = _clientsService.GetIcons();
            }

            var playTineParamIds = orders.Select(x => x.OrderId).ToArray();
            ViewData["PlayTune"] = playTineParamIds.Except(CurrentProductsIds).Any() ? 1 : 0;
            CurrentProductsIds = playTineParamIds;

            var result = new
            {
                PlayTune = (int)ViewData["PlayTune"],
                NewOrderArrived = (int)ViewData["PlayTune"] == 1,
                SongName = orders.Length == 0 ? DodoFMProxy.GetSongName() : string.Empty,
                ClientOrders = orders.Select(
                        x => new
                        {
                            x.OrderId,
                            x.OrderNumber,
                            x.ClientName,
                            ClientIconPath = clientTreatment == ClientTreatment.RandomImage && icons.Any()
                                ? GetIconPath(x.OrderNumber, icons, "https://wedevstorage.blob.core.windows.net/")
                                : null,
                            OrderReadyTimestamp = x.OrderReadyDateTime.Ticks,
                            OrderReadyDateTime = x.OrderReadyDateTime.ToString(CultureInfo.CurrentUICulture)
                        })
                    .OrderByDescending(x => x.OrderReadyTimestamp)
            };

            return Json(result);
        }

        private static RestaurantReadnessOrders MapToRestaurantReadnessOrders(ProductionOrder order)
        {
            return new RestaurantReadnessOrders(order.Id, order.Number, order.ClientName, order.ChangeDate ?? DateTime.Now);
        }

        private static string GetIconPath(int orderNumber, IReadOnlyList<ClientIcon> icons, string fileStorageHost)
        {
            var iconIndex = orderNumber % icons.Count;
            return icons[iconIndex].GetUrl(fileStorageHost);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public JsonResult GetRestaurantBannerUrl(int countryId, int departmentId, int unitId)
        {
            var department = _departmentsStructureService.GetDepartmentOrCache<CityDepartment>(departmentId);
            var restaurantBanners = _managementService
                .GetAvailableBanners(countryId, unitId, department.CurrentDateTime)
                .Where(x => x.MenuSpecializationTypes.Any(q => q == department.MenuSpecializationType));

            IEnumerable<object> result;

            if (restaurantBanners.Any())
            {
                result = restaurantBanners.Select(
                    x => new
                    {
                        BannerUrl = x.Url.Replace('\\', '/'),
                        DisplayTime = x.DisplayTime * 1000
                    }).ToArray();
            }
            else
            {
                result = new[] { new { BannerUrl = LocalizedContext.LocalizedContent(_hostingEnvironment, _fileService, "Tracking-Scoreboard-Empty.jpg"), DisplayTime = 60000 } };
            }

            return Json(result);
        }

        #endregion Ресторан.Готовность заказов
    }
}
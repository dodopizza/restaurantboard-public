using System;
using System.Collections.Generic;
using System.Linq;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Models;
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
	    private readonly IManagementService _managementService;
	    private readonly IHostingEnvironment _hostingEnvironment;
	    private readonly IOrdersService _ordersService;

		public BoardsController(
            IDepartmentsStructureService departmentsStructureService,
            IManagementService managementService,
            IHostingEnvironment hostingEnvironment,
            IOrdersService ordersService)
        {
            _departmentsStructureService = departmentsStructureService;
	        _managementService = managementService;
	        _hostingEnvironment = hostingEnvironment;
	        _ordersService = ordersService;
        }


        private int[] CurrentProductsIds
        {
            get
            {
                var currentProductsIds = HttpContext.Session.GetString("IdProductUnit");
                return !string.IsNullOrEmpty(currentProductsIds)
                    ? JsonConvert.DeserializeObject<int[]>(currentProductsIds)
                    : new int[0];
            }
            set
            {
                var serialized = JsonConvert.SerializeObject(value);
                HttpContext.Session.SetString("IdProductUnit", serialized);
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
	        var result = _ordersService.GetOrdersForUnit(
		        unitId,
		        ViewData,
		        CurrentProductsIds);

	        CurrentProductsIds = result.ProductIds;

			return Json(result.ClientOrdersModel);
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
                    });
            }
            else
            {
                result = new[] { new { BannerUrl = LocalizedContext.LocalizedContent(_hostingEnvironment, "Tracking-Scoreboard-Empty.jpg"), DisplayTime = 60000 } };
            }

            return Json(result);
        }

        #endregion Ресторан.Готовность заказов
    }
}
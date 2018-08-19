using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Management;
using Dodo.Core.Services;

namespace Dodo.RestaurantBoard.Site.Services
{
    public class RestaurantBannerService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDepartmentsStructureService _departmentsStructureService;
        private readonly IManagementService _managementService;

        public RestaurantBannerService(
            IHostingEnvironment hostingEnvironment, 
            IDepartmentsStructureService departmentsStructureService, 
            IManagementService managementService)
        {
            _hostingEnvironment = hostingEnvironment;
            _departmentsStructureService = departmentsStructureService;
            _managementService = managementService;
        }
        
        public IEnumerable<object> GetBanners(int countryId, int departmentId, int unitId)
        {
            var restaurantBanners = GetAvailableBanners(countryId, departmentId, unitId);

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
                result = GetDefaultResult();
            }

            return result;
        }

        public virtual IEnumerable<object> GetDefaultResult()
        {
            return new[] 
            {
                GetDefaultObject()
            };
        }

        protected virtual object GetDefaultObject()
        {
           return new
           {
               BannerUrl = GetDefaultBannerUrl(),
               DisplayTime = 60000
           };
        }

        protected virtual string GetDefaultBannerUrl()
        {
            return new LocalizedContext().LocalizedContent(_hostingEnvironment.WebRootPath, "Tracking-Scoreboard-Empty.jpg");
        }

        protected virtual IEnumerable<RestaurantBanner> GetAvailableBanners(int countryId, int departmentId, int unitId)
        {
            var department = _departmentsStructureService.GetDepartmentOrCache<CityDepartment>(departmentId);
            var restaurantBanners = _managementService
                .GetAvailableBanners(countryId, unitId, department.CurrentDateTime)
                .Where(x => x.MenuSpecializationTypes.Any(q => q == department.MenuSpecializationType));
            return restaurantBanners;
        }
    }
}

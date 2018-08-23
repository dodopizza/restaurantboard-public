using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class PizzeriaOrdersService : IPizzeriaOrdersService
    {
        private readonly IDepartmentsStructureService _departmentsStructureService;
        private readonly ITrackerClient _trackerClient;

        public PizzeriaOrdersService(IDepartmentsStructureService departmentsStructureService,
                                     ITrackerClient trackerClient)
        {           
            _departmentsStructureService = departmentsStructureService;
            _trackerClient = trackerClient;
        }

        public Department GetDepartmentByUnitOrCache(int unitId)
        {
            return _departmentsStructureService.GetDepartmentByUnitOrCache(unitId);
        }

        public T GetDepartmentOrCache<T>(int departmentId) where T : Department
        {
           return  _departmentsStructureService.GetDepartmentOrCache<T>(departmentId);
        }

        public Task<ProductionOrder[]> GetOrdersByTypeAsync(Uuid unitUuid, OrderType type, int limit)
        {
          return  _trackerClient.GetOrdersByTypeAsync(unitUuid, type, limit);
        }

        public Pizzeria GetPizzeriaOrCache(int id)
        {
            return _departmentsStructureService.GetPizzeriaOrCache(id);
        }

        public Unit GetUnitOrCache(Uuid unitUuid)
        {
            return _departmentsStructureService.GetUnitOrCache(unitUuid);
        }
    }

}
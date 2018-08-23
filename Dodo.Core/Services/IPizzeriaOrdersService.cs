using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dodo.Core.Services
{
  public  interface IPizzeriaOrdersService
    {
        Unit GetUnitOrCache(Uuid unitUuid);
        Department GetDepartmentByUnitOrCache(int unitId);
        T GetDepartmentOrCache<T>(int departmentId) where T : Department;
        Task<ProductionOrder[]> GetOrdersByTypeAsync(Uuid unitUuid, OrderType type, int limit);
        Pizzeria GetPizzeriaOrCache(int id);
    }
}

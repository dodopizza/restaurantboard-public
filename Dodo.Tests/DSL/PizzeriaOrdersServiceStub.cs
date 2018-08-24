using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System;
using System.Threading.Tasks;

namespace Dodo.Tests.DSL
{
    public class PizzeriaOrdersServiceStub : IPizzeriaOrdersService
    {
        private readonly string _clientName;
        public PizzeriaOrdersServiceStub(string clientName)
        {
            _clientName = clientName;

        }
        public Department GetDepartmentByUnitOrCache(int unitId)
        {
            return new CityDepartment();
        }

        public T GetDepartmentOrCache<T>(int departmentId) where T : Department
        {
            return new CityDepartment() as T;
        }

        public Task<ProductionOrder[]> GetOrdersByTypeAsync(Uuid unitUuid, OrderType type, int limit)
        {
            var orders = new[]
          {
                new ProductionOrder
                {
                    Id = 56,
                    Number = 4,
                    ClientName = _clientName,
                    ChangeDate = DateTime.Now.AddMinutes(-3)
                }
            };
            return Task.FromResult(orders);
        }

        public Pizzeria GetPizzeriaOrCache(int id)
        {
            return PizzeriaStub.GetPizzeria();
        }


        public Unit GetUnitOrCache(Uuid unitUuid)
        {
            return PizzeriaStub.GetPizzeria();
        }
    }
}

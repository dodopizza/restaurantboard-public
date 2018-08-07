using Dodo.Core.Common;
using Dodo.Core.Common.Helpers;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public class DepartmentsStructureService : IDepartmentsStructureService
	{
		public T GetDepartmentOrCache<T>(int departmentId) where T : Department => new CityDepartment {Country = DefaultModelsHelper.DefaultCountry} as T;

	    public Department GetDepartmentByUnitOrCache(int unitId) => new CityDepartment {Country = DefaultModelsHelper.DefaultCountry };

	    public Unit GetUnitOrCache(Uuid unitUuid) => DefaultModelsHelper.DefaultPizzeria;

	    public Pizzeria GetPizzeriaOrCache(int id) => DefaultModelsHelper.DefaultPizzeria;
	}
}
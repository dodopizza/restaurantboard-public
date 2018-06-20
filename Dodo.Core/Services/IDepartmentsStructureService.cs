using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;

namespace Dodo.Core.Services
{
	public interface IDepartmentsStructureService
	{
		Unit GetUnitOrCache(Uuid unitUuid);
		Department GetDepartmentByUnitOrCache(int unitId);
		Pizzeria GetPizzeriaOrCache(int id);
		T GetDepartmentOrCache<T>(int departmentId) where T : Department;
	}
}
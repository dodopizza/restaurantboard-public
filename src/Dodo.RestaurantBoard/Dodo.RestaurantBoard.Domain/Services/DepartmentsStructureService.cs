using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Management.Organizations;
using Dodo.Core.Services;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public class DepartmentsStructureService : IDepartmentsStructureService
	{
		private static readonly OrganizationShortInfo organizationShortInfo = new OrganizationShortInfo(0, string.Empty, string.Empty, OrganizationType.Rus_OOO, string.Empty, string.Empty, string.Empty, 1, string.Empty, string.Empty, string.Empty);
		private readonly Unit unit = new Pizzeria(29, new Uuid("000D3A240C719A8711E68ABA13F83227"), "Сык-1", string.Empty,
			string.Empty, UnitApprove.Approved, UnitState.Open, 2, new Uuid("000D3A240C719A8711E68ABA13FC4A39"), 1,
			organizationShortInfo, 100, DateTime.MinValue, "Gay", true, 1, 1, ClientTreatment.Name,
			true, new PizzeriaFormat(0, string.Empty, string.Empty));
		
		
		public T GetDepartmentOrCache<T>(int departmentId) where T : Department
		{
			return new CityDepartment {Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty)} as T;
		}

		public Department GetDepartmentByUnitOrCache(int unitId)
		{
			return new CityDepartment {Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty)};
		}

		public Unit GetUnitOrCache(Uuid unitUuid)
		{
			return unit;
		}
		public Pizzeria GetPizzeriaOrCache(int id)
		{
			return new Pizzeria(29, new Uuid("000D3A240C719A8711E68ABA13F83227"), "Сык-1", string.Empty, string.Empty, UnitApprove.Approved, UnitState.Open, 2, new Uuid("000D3A240C719A8711E68ABA13FC4A39"), 1, organizationShortInfo, 100, DateTime.MinValue, "Gay", true, 1, 1, ClientTreatment.Name, true, new PizzeriaFormat(0, string.Empty, string.Empty));
		}
	}
}
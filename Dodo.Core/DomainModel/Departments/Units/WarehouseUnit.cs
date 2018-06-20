using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.DomainModel.Departments.Units
{
	[Serializable]
	public class WarehouseUnit : Unit
	{
		public WarehouseUnit(Int32 id, Uuid uuid, String name, String alias, UnitState state, Int32 departmentId, Uuid departmentUuid,
            Int32 countryId, OrganizationShortInfo organization)
			: base(id, uuid, name, alias, UnitType.Warehouse, state, departmentId, departmentUuid, countryId, organization)
		{
		}
	}
}
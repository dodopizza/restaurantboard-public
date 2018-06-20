using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.DomainModel.Departments
{
	[Serializable]
	public abstract class Unit
	{
		public Int32 Id { get; private set; }
		public Uuid Uuid { get; private set; }
		public String Name { get; private set; }
		public String Alias { get; private set; }
		public UnitType Type { get; private set; }

		public String TranslitAlias { get; set; }
		public UnitApprove Approve { get; set; }
		public UnitState State { get; private set; }
		public OrganizationShortInfo Organization { get; private set; }
		public Int32 DepartmentId { get; private set; }
        public Uuid DepartmentUuid { get; private set; }
        public Int32 CountryId { get; private set; }

		protected Unit(Int32 id, Uuid uuid, String name, String alias, UnitType type, UnitState state, Int32 departmentId, Uuid departmentUuid,
            Int32 countryId, OrganizationShortInfo organization)
		{
			Id = id;
			Uuid = uuid;
            Name = name;
			Alias = alias;
			Type = type;
			State = state;
			DepartmentId = departmentId;
            DepartmentUuid = departmentUuid;
            CountryId = countryId;
            Organization = organization;
		}

		public override String ToString()
		{
			return $"{Name} Type: {Type} State: {State}";
		}
	}
}
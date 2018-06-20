using System;
using Dodo.Core.Common;

namespace Dodo.Core.DomainModel.Departments.Units
{
	[Serializable]
    public sealed class UnitShortInfo
    {
        public Int32 Id { get; private set; }
        public Uuid Uuid { get; private set; }
        public String Name { get; private set; }
        public String Alias { get; private set; }
        public UnitType Type { get; private set; }
        public UnitState State { get; private set; }
        public Int32 DepartmentId { get; private set; }
        public Int32? OrganizationId { get; private set; }

        public UnitShortInfo(Int32 id, Uuid uuid, String name, String alias, UnitType type, UnitState state, Int32 departmentId, Int32? organizationId)
        {
            Id = id;
            Uuid = uuid;
            Name = name;
            Alias = alias;
            Type = type;
            State = state;
            DepartmentId = departmentId;
            OrganizationId = organizationId;
        }

        public UnitShortInfo(Int32 id, String name)
        {
            Id = id;
            Name = name;
        }
    }
}

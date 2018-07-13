using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.Tests.DomainModel
{
    public class UnitStb : Unit
    {
        private UnitStub(int id, Uuid uuid, string name, string alias, UnitType type, UnitState state, int departmentId, Uuid departmentUuid, int countryId, OrganizationShortInfo organization) : base(id, uuid, name, alias, type, state, departmentId, departmentUuid, countryId, organization)
        {
        }

        public static UnitStub Create(UnitType type)
        {
            return new UnitStub(0, null, "", "", type, UnitState.Open, 0, null, 0, null);
        }
    }
}
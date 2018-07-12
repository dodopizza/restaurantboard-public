using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.Tests.DomainModel
{
    public class UnitStub : Unit
    {
        public int ToStringOfficeCounter { get; private set; }
        public int ToStringPizzeriaCounter { get; private set; }
        public int ToStringCallCenterCounter { get; private set; }
        public int ToStringWarehouseCounter { get; private set; }
        public int ToStringServiceDeliveryCounter { get; private set; }
        public int ToStringDefaultCounter { get; private set; }
        
        public UnitStub(int id, Uuid uuid, string name, string alias, UnitType type, UnitState state, int departmentId, Uuid departmentUuid, int countryId, OrganizationShortInfo organization) : base(id, uuid, name, alias, type, state, departmentId, departmentUuid, countryId, organization)
        {
        }

        public static UnitStub Create(UnitType type)
        {
            return new UnitStub(0, null, "", "", type, UnitState.Open, 0, null, 0, null);
        }
        
        public override String ToStringOffice()
        {
            ToStringOfficeCounter++;
            return $"{Name} Type: Office State: {State}";
        }
		
        public override String ToStringPizzeria()
        {
            ToStringPizzeriaCounter++;
            return $"{Name} Type: Pizzeria State: {State}";
        }
		
        public override String ToStringCallCenter()
        {
            ToStringCallCenterCounter++;
            return $"{Name} Type: CallCenter State: {State}";
        }
		
        public override String ToStringWarehouse()
        {
            ToStringWarehouseCounter++;
            return $"{Name} Type: Warehouse State: {State}";
        }
		
        public override String ToStringServiceDelivery()
        {
            ToStringServiceDeliveryCounter++;
            return $"{Name} Type: ServiceDelivery State: {State}";
        }
        
        public override String ToString()
        {
            ToStringDefaultCounter++;
            return $"{Name} Type: {Type} State: {State}";
        }
    }
}
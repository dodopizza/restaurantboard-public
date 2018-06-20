using System;
using Dodo.Core.Common;

namespace Dodo.Core.DomainModel.Departments.Units
{
    public class UnitWarehouse
    {
        public Int32 UnitId { get; }
        public Uuid UnitUuid { get; }
        public String WarehouseNumber { get; }

        public UnitWarehouse(int unitId, Uuid unitUuid, string warehouseNumber)
        {
            UnitId = unitId;
            UnitUuid = unitUuid;
            WarehouseNumber = warehouseNumber;
        }
    }
}
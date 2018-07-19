using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using System;

namespace Dodo.RestaurantBoard.Site.Tests.Factories
{
    public static class PizzeriaFactory
    {
        public static Pizzeria CreatePizzeria(ClientTreatment? clientTreatment = null)
        {
            return new Pizzeria(1, new Uuid("000D3A240C719A8711E68ABA13F83227"), "Сык-1", string.Empty, string.Empty,
                UnitApprove.Approved, UnitState.Open, 777, new Uuid("000D3A240C719A8711E68ABA13FC4A39"), 1,
                null, 100, DateTime.MinValue, "Gay", true, 1, 1, clientTreatment ?? ClientTreatment.DefaultName, true,
                new PizzeriaFormat(0, string.Empty, string.Empty));
        }
    }
}

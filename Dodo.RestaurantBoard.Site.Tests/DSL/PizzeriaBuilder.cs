using System;
using System.Runtime.InteropServices;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    public class PizzeriaBuilder
    {
        private int _unitId;

        public PizzeriaBuilder With(int id)
        {
            _unitId = id;

            return this;
        }

        public Pizzeria Please()
        {
            return new Pizzeria(
                _unitId, 
                new Uuid("000D3A240C719A8711E68ABA13F83227"), 
                "Сык-1",
                string.Empty,
                string.Empty,
                UnitApprove.Approved,
                UnitState.Open, 
                2, 
                new Uuid("000D3A240C719A8711E68ABA13FC4A39"), 
                1,
                new OrganizationShortInfo(0, string.Empty, string.Empty, OrganizationType.Rus_OOO, string.Empty, string.Empty, string.Empty, 1, string.Empty, string.Empty, string.Empty), 
                100, 
                DateTime.MinValue, 
                "Gay", 
                true, 
                1, 
                1, 
                ClientTreatment.Name,
                true, 
                new PizzeriaFormat(0, string.Empty, string.Empty));
        }
    }
}
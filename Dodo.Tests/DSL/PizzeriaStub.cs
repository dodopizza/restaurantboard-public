using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Management.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.Tests.DSL
{
    class PizzeriaStub
    {
        public static Pizzeria GetPizzeria()
        {
            OrganizationShortInfo organizationShortInfo = new OrganizationShortInfo(0, string.Empty, string.Empty, OrganizationType.Rus_OOO, string.Empty, string.Empty, string.Empty, 1, string.Empty, string.Empty, string.Empty);

            return new Pizzeria(
               29, 
               new Uuid("000D3A240C719A8711E68ABA13F83227"),
               "Сык-1", 
               string.Empty,
                string.Empty,
                UnitApprove.Approved, 
                UnitState.Open, 
                2, 
                new Uuid("000D3A240C719A8711E68ABA13FC4A39"),
                1,
                organizationShortInfo, 
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

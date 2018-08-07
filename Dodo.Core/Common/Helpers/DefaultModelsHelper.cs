using System;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.Common.Helpers
{
    public static class DefaultModelsHelper
    {
        public static Country DefaultCountry => new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty);

        public static Pizzeria DefaultPizzeria => new Pizzeria(29, new Uuid("000D3A240C719A8711E68ABA13F83227"),
            "Сык-1", string.Empty, string.Empty, UnitApprove.Approved, UnitState.Open, 2,
            new Uuid("000D3A240C719A8711E68ABA13FC4A39"), 1, OrganizationShortInfo, 100, DateTime.MinValue, "Gay", true,
            1, 1, ClientTreatment.Name, true, new PizzeriaFormat(0, string.Empty, string.Empty));

        public static OrganizationShortInfo OrganizationShortInfo => new OrganizationShortInfo(0, string.Empty,
            string.Empty, OrganizationType.Rus_OOO, string.Empty, string.Empty, string.Empty, 1, string.Empty,
            string.Empty, string.Empty);
    }
}

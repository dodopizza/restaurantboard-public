using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Tests.DSL
{
    public class OrganizationBuilder
    {
        public OrganizationFake Please()
        {
            var country = new Country(0, "", "", 0, "", Currency.Dollar, "");

            return new OrganizationFake(0,
                new Uuid(),
                "",
                "",
                "",
                "",
                "",
                country,
                "",
                "",
                "");
        }
    }
}
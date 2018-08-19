using Dodo.Core.Common;
using Dodo.Core.Common.Enums;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Tests.DSL
{
    public class OrganizationBuilder
    {
        private CountryCode _countryCode;

        public OrganizationBuilder WithCountryCode(CountryCode countryCode)
        {
            _countryCode = countryCode;
            return this;
        }
        
        public OrganizationFake Please()
        {
            var country = new Country(0, "", "", 0, "", Currency.Dollar, "");

            var organization = new OrganizationFake(0,
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

            organization.CountryCode = _countryCode;

            return organization;
        }
    }
}
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Management.Organizations;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class OrganizationTests
    {
        [Fact]
        public void Test1()
        {
            var country = new Country(0, "", "", 0, "", Currency.Ruble, "");
            var organization = new Organization(0, new Uuid(), "", "", "", "", "", country, "", "", "");
        }
    }
}
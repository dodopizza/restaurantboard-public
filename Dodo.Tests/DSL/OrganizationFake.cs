using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Tests.DSL
{
    public class OrganizationFake : Organization
    {
        public OrganizationFake(int id, Uuid uuid, string nameFull, string nameShort, string address, string pozitionOfHead, string headManagerName, Country country, string bankName, string checkingAccount, string shareCapital) : base(id, uuid, nameFull, nameShort, address, pozitionOfHead, headManagerName, country, bankName, checkingAccount, shareCapital)
        {
        }

        public override string GetShortInfo()
        {
            throw new System.NotImplementedException();
        }

        public override string GetFullDescription()
        {
            throw new System.NotImplementedException();
        }

        public override string GetShortDescription()
        {
            throw new System.NotImplementedException();
        }

        public override OrganizationShortInfo OrganizationShortInfo { get; }
    }
}
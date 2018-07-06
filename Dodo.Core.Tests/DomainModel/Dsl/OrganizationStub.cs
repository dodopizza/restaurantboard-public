using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.Tests.DomainModel.Dsl
{
    public class OrganizationStub : Core.DomainModel.Management.Organizations.Organization
    {
        public OrganizationStub(int id, Uuid uuid, string nameFull, string nameShort, string address, string pozitionOfHead, string headManagerName, Country country, string bankName, string checkingAccount, string shareCapital) : base(id, uuid, nameFull, nameShort, address, pozitionOfHead, headManagerName, country, bankName, checkingAccount, shareCapital)
        {
        }

        public override string GetShortInfo()
        {
            throw new NotImplementedException();
        }

        public override string GetFullDescription()
        {
            throw new NotImplementedException();
        }

        public override string GetShortDescription()
        {
            throw new NotImplementedException();
        }

        public override OrganizationShortInfo OrganizationShortInfo { get; }
    }
}

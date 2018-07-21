using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.Tests.DomainModel.DSL
{
    public class OrganizationBuilder
    {
        private string _headManagerName;
        
        public OrganizationBuilder WithHeadManagerName(string headManagerName)
        {
            _headManagerName = headManagerName;
            return this;
        }

        public Organization Please()
        {
            return new Organization(_headManagerName);
        }
    }
}
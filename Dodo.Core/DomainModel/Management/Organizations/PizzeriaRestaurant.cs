namespace Dodo.Core.DomainModel.Management.Organizations
{
    public class PizzeriaRestaurant: Organization
    {
        public PizzeriaRestaurant(string headManagerName) : base(5, null, null, null, null, null, headManagerName, null, null, null,
            null)
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
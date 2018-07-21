namespace Dodo.Core.Tests.DomainModel.DSL
{
    public static class Create
    {
        public static DepartmentBuilder Department => new DepartmentBuilder();
        public static OrganizationBuilder Organization => new OrganizationBuilder();
        public static UnitBuilder Unit => new UnitBuilder();
        public static UnitMockBuilder UnitMock => new UnitMockBuilder();
    }
}
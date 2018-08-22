namespace Dodo.RestaurantBoard.Tests.DSL
{
    public class Create
    {
        public static TrackerBuilder TrackerClient => new TrackerBuilder();
        public static PizzeriaOrdersServiceBuilder PizzeriaOrdersService => new PizzeriaOrdersServiceBuilder();
        public static PizzeriaBuilder Pizzeria => new PizzeriaBuilder();
        public static DepartmentsStructureServiceBuilder DepartmentsStructureService => new DepartmentsStructureServiceBuilder();
    }
}

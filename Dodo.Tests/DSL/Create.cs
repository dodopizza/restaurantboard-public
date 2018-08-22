namespace Tests.DSL
{
    public static class Create
    {
        public static DepartmentsStructureServiceBuilder DepartmentsStructureService => new DepartmentsStructureServiceBuilder();
        public static TrackerClientBuilder TrackerClient => new TrackerClientBuilder();
        public static ClientServiceBuilder ClientService => new ClientServiceBuilder();
        public static UnitOrderServiceBuilder UnitOrderService => new UnitOrderServiceBuilder();
    }
}
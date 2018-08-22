namespace Tests.DSL
{
    public static class Create
    {
        public static DepartmentsStructureServiceBuilder DepartmentsStructureService => new DepartmentsStructureServiceBuilder();
        public static TrackerClientBuilder TrackerClient => new TrackerClientBuilder();
    }
}
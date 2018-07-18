namespace Tests.Dsl
{
    public static class Create
    {
        public static CallCenterPhoneParametersBuilder CallCenterPhoneParameter =>
            new CallCenterPhoneParametersBuilder();

        public static PizzeriaBuilder Pizzeria =>
            new PizzeriaBuilder();
    }
}
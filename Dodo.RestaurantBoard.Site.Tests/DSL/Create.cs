using System;
using System.Collections.Generic;
using System.Text;
using Dodo.Core.DomainModel.Departments.Units;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    public class Create
    {
        public static DepartmentsStructureServiceBuilder DepartmentsStructureServiceBuilder => new DepartmentsStructureServiceBuilder();
        public static PizzeriaBuilder PizzeriaBuilder => new PizzeriaBuilder();    
        public static ClientsServiceBuilder ClientsServiceBuilder => new ClientsServiceBuilder();
        public static TrackerClientBuilder TrackerClientBuilder => new TrackerClientBuilder();

    }
}

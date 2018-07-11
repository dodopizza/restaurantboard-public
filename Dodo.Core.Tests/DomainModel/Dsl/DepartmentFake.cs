using System;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.Tests.DomainModel.Dsl
{
    public class DepartmentFake : Core.DomainModel.Departments.Department
    {
        public Int16 TempTimeZoneShift { get; set; }

        public override Int16 TimeZoneShift => TempTimeZoneShift;
    }
}

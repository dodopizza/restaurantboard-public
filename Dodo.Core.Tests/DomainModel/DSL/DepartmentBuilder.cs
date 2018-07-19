using System.Collections.Generic;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.Tests.DomainModel.DSL
{
    public class DepartmentBuilder
    {
        private int _timeZoneShift;
        private List<Unit> _units = new List<Unit>();
        
        public DepartmentBuilder WithTimeZoneShift(int timeZoneShift)
        {
            _timeZoneShift = timeZoneShift;
            return this;
        }

        public Department Please()
        {
            var department = new Department {ManagedTimeZoneShift = _timeZoneShift};
            foreach (var unit in _units)
            {
                department.AddUnit(unit);
            }

            return department;
        }

        public DepartmentBuilder WithUnit(Unit unit)
        {
            _units.Add(unit);
            return this;
        }

        public DepartmentBuilder WithUnits(int unitsCount)
        {
            for (var i = 0; i < unitsCount; i++)
            {
                var unit = Create.Unit.Please();
                _units.Add(unit);
            }

            return this;
        }
    }
}
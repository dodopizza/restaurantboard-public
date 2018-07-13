using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.Services;
using Moq;

namespace Dodo.Tests.DSL
{
    public class DepartmentStructureServiceMockBuilder
    {
        private readonly ObjectMother _objectMother = new ObjectMother();
        private readonly Mock<IDepartmentsStructureService> _service;

        public DepartmentStructureServiceMockBuilder()
        {
            _service = new Mock<IDepartmentsStructureService>();
        }

        public DepartmentStructureServiceMockBuilder WithGetUnitOrCache(Uuid unitUuid)
        {
            _service
                .Setup(x => x.GetUnitOrCache(unitUuid))
                .Returns(_objectMother.CreateUnitWithUuid(unitUuid));
            return this;
        }

        public DepartmentStructureServiceMockBuilder WithGetDepartmentByUnitOrCache(int unitId)
        {
            _service
                .Setup(x => x.GetDepartmentByUnitOrCache(unitId))
                .Returns(_objectMother.CreateDepartment());
            return this;
        }

        public DepartmentStructureServiceMockBuilder WithGetDepartmentByUnitOrCacheWithResult(
            int unitId, Department resultDepartment)
        {
            _service
                .Setup(x => x.GetDepartmentByUnitOrCache(unitId))
                .Returns(resultDepartment);
            return this;
        }

        public DepartmentStructureServiceMockBuilder WithGetPizzeriaOrCache(int id)
        {
            _service
                .Setup(x => x.GetPizzeriaOrCache(id))
                .Returns(_objectMother.CreatePizzeria());
            return this;
        }

        public Mock<IDepartmentsStructureService> Build()
        {
            return _service;
        }
    }
}
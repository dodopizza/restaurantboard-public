using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.Services;
using Moq;

namespace Tests.DSL
{
    public class DepartmentsStructureServiceBuilder
    {
        private readonly ObjectMother _objectMother = new ObjectMother();
        private readonly Mock<IDepartmentsStructureService> _service;

        public DepartmentsStructureServiceBuilder()
        {
            _service = new Mock<IDepartmentsStructureService>();
        }

        public DepartmentsStructureServiceBuilder WithPizzeria(int id)
        {
            _service
                .Setup(x => x.GetPizzeriaOrCache(id))
                .Returns(_objectMother.CreatePizzeriaWithId(id));
            return this;
        }

        public IDepartmentsStructureService Build()
        {
            return _service.Object;
        }
    }
}
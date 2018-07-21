using Dodo.Core.Services;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Verifies
{
    public class DepartmentsStructureServiceMockVerify
    {
        private readonly Mock<IDepartmentsStructureService> _service;

        public DepartmentsStructureServiceMockVerify(Mock<IDepartmentsStructureService> service)
        {
            _service = service;
        }

        public void CallGetPizzeriaOrCache(int callCount)
        {
            _service.Verify(foo => foo.GetPizzeriaOrCache(It.IsAny<int>()), Times.Exactly(callCount));
        }
    }
}

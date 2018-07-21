using Dodo.Core.DomainModel.Departments;
using Moq;

namespace Dodo.Core.Tests.DomainModel.DSL
{
    public class UnitMockBuilder
    {
        private UnitType _unitType;
        
        public UnitMockBuilder WithType(UnitType unitType)
        {
            _unitType = unitType;
            return this;
        }

        public Mock<Unit> Please()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(_unitType);

            return unitMock;
        }
    }
}
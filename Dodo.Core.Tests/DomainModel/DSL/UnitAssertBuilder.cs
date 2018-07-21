using System;
using Dodo.Core.DomainModel.Departments;
using Moq;

namespace Dodo.Core.Tests.DomainModel.DSL
{
    public class UnitAssertBuilder
    {
        private Mock<Unit> _unitMock;

        public UnitAssertBuilder(Mock<Unit> UnitMock)
        {
            _unitMock = UnitMock;
        }
        
        
        public void ToStringOfficeMethod()
        {
            _unitMock.Verify(x => x.ToStringOffice(), Times.Once);
        }
        
        public void ToStringPizzeriaMethod()
        {
            _unitMock.Verify(x => x.ToStringPizzeria(), Times.Once);
        }
        
        public void ToStringCallCenterMethod()
        {
            _unitMock.Verify(x => x.ToStringCallCenter(), Times.Once);
        }
        
        public void ToStringWarehouseMethod()
        {
            _unitMock.Verify(x => x.ToStringWarehouse(), Times.Once);
        }
        
        public void ToStringServiceDeliveryMethod()
        {
            _unitMock.Verify(x => x.ToStringServiceDelivery(), Times.Once);
        }
        
        public void ToStringMethod()
        {
            _unitMock.Verify(x => x.ToString(), Times.Once);
        }
    }
}
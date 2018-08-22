using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dodo.Core.AppServices;
using Dodo.Core.DomainModel;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using Moq;

namespace Tests.DSL
{
    public class UnitOrderServiceBuilder
    {
        private readonly ObjectMother _objectMother = new ObjectMother();
        private readonly Mock<IUnitOrdersService> _service;
        private int _unitId;
        private RestaurantReadnessOrders[] _orders;

        public UnitOrderServiceBuilder()
        {
            _service = new Mock<IUnitOrdersService>();
        }

        public UnitOrderServiceBuilder WithPizzeria(int unitId)
        {
            _unitId = unitId;
            return this;
        }

        public UnitOrderServiceBuilder WithOrders(params RestaurantReadnessOrders[] orders)
        {
            _orders = orders;
            return this;
        }

        public IUnitOrdersService Build()
        {
            _service
                .Setup(x => x.GetUnitOrders(_unitId))
                .Returns(Task.FromResult(new UnitOrders
                {
                    Unit = _objectMother.CreatePizzeriaWithId(_unitId),
                    Orders = _orders?.ToList()
                }));
            return _service.Object;
        }
    }
}
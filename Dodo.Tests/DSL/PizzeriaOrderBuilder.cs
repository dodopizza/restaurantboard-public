using Dodo.Core.Services;
using static Dodo.Tests.BoardsControllerTests;

namespace Dodo.Tests.DSL
{
    public class PizzeriaOrderBuilder
    {
        string _clientName;      

        public PizzeriaOrderBuilder AddClientWithName(string name)
        {
            _clientName = name;
            return this;
        }

        public IPizzeriaOrdersService Please()
        {
            return new PizzeriaOrdersServiceStub(_clientName);
        }
    }
}
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.Services;
using Moq;

namespace Dodo.Tests.DSL
{
    public class ClientServiceMockBuilder
    {
        private readonly Mock<IClientsService> _service;

        public ClientServiceMockBuilder()
        {
            _service = new Mock<IClientsService>();
        }

        public ClientServiceMockBuilder WithGetIcons()
        {
            _service
                .Setup(x => x.GetIcons())
                .Returns(new ClientIcon[0]);
            return this;
        }

        public Mock<IClientsService> Build()
        {
            return _service;
        }
    }
}
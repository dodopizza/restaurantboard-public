using Dodo.Core.DomainModel.Clients;
using Dodo.Core.Services;
using Moq;

namespace Tests.DSL
{
    public class ClientServiceBuilder
    {
        private readonly Mock<IClientsService> _service;

        public ClientServiceBuilder()
        {
            _service = new Mock<IClientsService>();
        }

        public ClientServiceBuilder WithoutIcons()
        {
            _service
                .Setup(x => x.GetIcons())
                .Returns(new ClientIcon[0]);
            return this;
        }

        public IClientsService Build()
        {
            return _service.Object;
        }
    }
}
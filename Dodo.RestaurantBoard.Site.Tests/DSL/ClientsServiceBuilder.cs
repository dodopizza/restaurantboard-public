using System;
using System.Collections.Generic;
using System.Text;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.Services;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    public class ClientsServiceBuilder
    {
        private readonly Mock<IClientsService> _service;

        public ClientsServiceBuilder()
        {
            _service = new Mock<IClientsService>();
        }

        public ClientsServiceBuilder WithoutIcons()
        {
            _service.Setup(x => x.GetIcons()).Returns(new ClientIcon[] { });
            return this;
        }

        public IClientsService Please()
        {
            return _service.Object;
        }
    }
}

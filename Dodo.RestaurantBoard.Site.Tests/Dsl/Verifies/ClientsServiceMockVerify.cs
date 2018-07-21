using System;
using System.Collections.Generic;
using System.Text;
using Dodo.Core.Services;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Verifies
{
    public class ClientsServiceMockVerify
    {
        private readonly Mock<IClientsService> _clientsService;

        public ClientsServiceMockVerify(Mock<IClientsService> clientsService)
        {
            _clientsService = clientsService;
        }

        public void CallGetIcons(int callCount)
        {
            _clientsService.Verify(foo => foo.GetIcons(), Times.Exactly(callCount));
        } 
    }
}

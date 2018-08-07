using System;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public class ClientService : IClientsService
	{
		public ClientIcon[] GetIcons(ClientTreatment clientTreatment)
		{
		    return clientTreatment == ClientTreatment.RandomImage
		        ? new ClientIcon[] {new ClientIcon(1, "")}
		        : new ClientIcon[] { };
		}
	}
}
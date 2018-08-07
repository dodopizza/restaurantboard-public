using System;
using System.Collections.Generic;
using System.Linq;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public class ClientService : IClientsService
	{
        private const string DefaultFileStorageHost = "https://wedevstorage.blob.core.windows.net/";

        public ClientIcon[] GetIcons(ClientTreatment clientTreatment)
		{
		    return clientTreatment == ClientTreatment.RandomImage
		        ? new ClientIcon[] {new ClientIcon(1, "")}
		        : new ClientIcon[] { };
		}

	    public string GetClientIconPath(int orderNumber, ClientTreatment clientTreatment, string fileStorageHost = null)
        {
	        var icons = GetIcons(clientTreatment);

	        if (clientTreatment == ClientTreatment.RandomImage && icons.Any())
	        {
	            var iconIndex = orderNumber % icons.Length;
	            return icons[iconIndex].GetUrl(fileStorageHost ?? DefaultFileStorageHost);
            }

	        return null;
	    }
    }
}
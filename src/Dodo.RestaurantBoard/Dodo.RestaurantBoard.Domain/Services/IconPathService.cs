using System.Linq;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public class IconPathService : IIconPathService
	{
	    public string GetIconPath(RestaurantReadnessOrders orders, ClientTreatment clientTreatment, ClientIcon[] icons)
	    {
		    if (!(clientTreatment == ClientTreatment.RandomImage && icons.Any()))
		    {
			    return null;
		    }

		    const string host = "https://wedevstorage.blob.core.windows.net/";
		    var iconIndex = orders.OrderNumber % icons.Length;
		    return icons[iconIndex].GetUrl(host);
		}
	}
}

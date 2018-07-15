using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;

namespace Dodo.Core.Services
{
	public interface IIconPathService
	{
		string GetIconPath(RestaurantReadnessOrders orders, ClientTreatment clientTreatment, ClientIcon[] icons);
	}
}
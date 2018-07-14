using System.Collections.Generic;
using Dodo.Core.DomainModel.ClientOrders;

namespace Dodo.Core.Services
{
	public interface IOrdersService
	{
		GetOrdersResult GetOrdersForUnit(int unitId, IDictionary<string, object> viewData, ICollection<int> currentProductsIds);
	}
}
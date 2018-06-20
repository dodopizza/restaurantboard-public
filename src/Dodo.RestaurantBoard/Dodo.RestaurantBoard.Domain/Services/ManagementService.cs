using System;
using Dodo.Core.DomainModel.Management;
using Dodo.Core.Services;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public class ManagementService : IManagementService
	{
		public RestaurantBanner[] GetAvailableBanners(int countryId, int unitId, DateTime timePoint)
		{
			return new RestaurantBanner[0];
		}
	}
}
using System;
using Dodo.Core.DomainModel.Management;

namespace Dodo.Core.Services
{
	public interface IManagementService
	{
		RestaurantBanner[] GetAvailableBanners(Int32 countryId, Int32 unitId, DateTime timePoint);
	}
}
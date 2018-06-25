angular.module('app')
	.service('OrdersReadinessToStationary', function($http, $q)
	{
		this.getOrders = function(unitId)
		{
			var dfd = $q.defer();
			$http({ method: 'GET', cache: false, url: '/Boards/GetOrderReadinessToStationary', params: { unitId: unitId } })
				.then(function(result)
				{
					dfd.resolve(result.data);
				})
				.catch(function(result)
				{
					dfd.reject(result.data.Error);
				});
			return dfd.promise;
		}

		this.getBanners = function(countryId, departmentId, unitId)
		{
			var dfd = $q.defer();
			$http({ method: 'GET', cache: false, url: '/Boards/GetRestaurantBannerUrl', params: { countryId: countryId, departmentId: departmentId, unitId: unitId } })
				.then(function(result)
				{
					dfd.resolve(result.data);
				})
				.catch(function(result)
				{
					dfd.reject(result.data.Error);
				});
			return dfd.promise;
		}
	});
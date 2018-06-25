angular.module('app')
	.service('expressProducts', function($http, $q)
	{
		this.getProducts = function(unitId)
		{
			var dfd = $q.defer();

			$http(
			{
				method: 'GET',
				cache: false,
				url: '/Boards/GetExpressProducts',
				params: { unitId: unitId }
			})
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

			$http(
			{
				method: 'GET',
				cache: false,
				url: '/Boards/GetRestaurantBannerUrl',
				params: { countryId: countryId, departmentId: departmentId, unitId: unitId }
			})
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
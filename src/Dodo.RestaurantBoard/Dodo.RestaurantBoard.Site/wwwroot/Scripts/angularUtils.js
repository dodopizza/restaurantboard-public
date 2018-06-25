var http = new function()
{
	function execute($http, $q, method, url, params)
	{
		var dfd = $q.defer();

		$http({ method: method, url: url, params: params })
			.then(function(result)
			{
				dfd.resolve(result.data);
			})
			.catch(function(result)
			{
				var error;

				if (result.data != null)
				{
					error = result.data.Error || result.data.message;
					
					if (error == null)
					{
						var root = document.createElement('html');
						root.innerHTML = result.data;
						var title = root.getElementsByTagName('title')[0];

						if (title != null) error = title.innerHTML;
					}
				}
				else
				{
					error = result.status + ' ' + result.statusText;
				}

				dfd.reject(error);
			});

		return dfd.promise;
	}

	this.get = function($http, $q, url, params)
	{
		return execute($http, $q, 'GET', url, params);
	};

	this.post = function($http, $q, url, params)
	{
		return execute($http, $q, 'POST', url, params);
	};
};
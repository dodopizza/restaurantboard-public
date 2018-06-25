angular.module('app')
	.controller('ExpressProductsController', function($scope, $interval, $timeout, $q,  $sce, expressProducts)
	{
		function SizeParameters(maxElementsCount, minElementsCount, columnsCount)
		{
			this.maxElementsCount = maxElementsCount;
			this.minElementsCount = minElementsCount;
			this.columnsCount = columnsCount;
		}

		var productTypes =
		{
			bakedProduct: 3,
			compositeProduct: 2,
			simpleProduct: 1
		};

		var productionStage =
		{
			waitingMake: 0,
			onDoughTable: 1,
			onHotAssemblyTable: 2,
			onColdAssemblyTable: 8,
			onPackingTable: 3,
			inOven: 4,

			waitingDelivery: 5,
		};

		var productPreparationStages =
		[
			productionStage.waitingMake,
			productionStage.onDoughTable,
			productionStage.onHotAssemblyTable,
			productionStage.onColdAssemblyTable,
			productionStage.onPackingTable,
			productionStage.inOven
		];

		var display =
		{
			size16: new SizeParameters(16, 9, 4),
			size9: new SizeParameters(9, 4, 3),
			size4: new SizeParameters(4, 2, 2),
			size1: new SizeParameters(1, 0, 1)
		};

		var timeTools =
		{
			formatToTwoDigits: function(value)
			{
				return ("00" + value).slice(-2);
			},

			getTotalMinutes: function(totalSeconds)
			{
				return Math.floor(totalSeconds / 60);
			},

			getTimeFormat: function(totalSeconds)
			{
				var negative = totalSeconds < 0;
				totalSeconds = Math.abs(totalSeconds);

				var hours = Math.floor(totalSeconds / 3600);
				var minutes = Math.floor((totalSeconds % 3600) / 60);
				var seconds = totalSeconds % 60;

				var colon = "<span class='colon-center'>:</span>";

				if (!negative) return timeTools.formatToTwoDigits(minutes) + colon + timeTools.formatToTwoDigits(seconds);

				if (hours > 0) return "+" + hours + colon + timeTools.formatToTwoDigits(minutes) + colon + timeTools.formatToTwoDigits(seconds);

				return "+" + timeTools.formatToTwoDigits(minutes) + colon + timeTools.formatToTwoDigits(seconds);
			}
		};
		$scope.renderHtml = function (html_code) {
			return $sce.trustAsHtml(html_code);
		};


		$scope.message = '';
		$scope.resources = window.resources;
		$scope.sizeClass = '';
		$scope.banners =
		{
			containerStyle: {},

			slideShowOn: false,
			currentIndex: 0,
			updateIntervalInMilliseconds: 1800000,

			list: [],

			init: function()
			{
				$scope.banners.load();

				$interval($scope.banners.load, $scope.banners.updateIntervalInMilliseconds);
			},

			load: function()
			{
				expressProducts.getBanners($scope.resources.countryId, $scope.resources.departmentId, $scope.resources.unitId)
					.then(function(banners)
					{
						$scope.banners.list = banners;
						$scope.message = '';

						if (!$scope.banners.slideShowOn) $scope.banners.showNext();
					})
					.catch(function()
					{
						$scope.message = $scope.resources.message;
					});
			},

			showNext: function()
			{
				if ($scope.products.list.length > 0)
				{
					$scope.banners.slideShowOn = false;

					return;
				}

				if ($scope.banners.list.length == 0)
				{
					if ($scope.banners.slideShowOn) $scope.banners.clear();
					$scope.banners.slideShowOn = false;

					return;
				}

				$scope.banners.slideShowOn = true;

				if ($scope.banners.currentIndex >= $scope.banners.list.length) $scope.banners.currentIndex = 0;

				$scope.banners.containerStyle =
				{
					'background-image': 'url(' + $scope.banners.list[$scope.banners.currentIndex].BannerUrl + ')',
					'background-size': 'cover'
				};

				$timeout($scope.banners.showNext, $scope.banners.list[$scope.banners.currentIndex].DisplayTime);

				$scope.banners.currentIndex += 1;
			},

			clear: function()
			{
				$scope.banners.containerStyle = {};
			}
		};

		$scope.products =
		{
			list: [],
			rows: [],

			updateIntervalInMilliseconds: 10000,
			statusUpdateIntervalInMilliseconds: 1000,

			init: function()
			{
				$scope.products.load();

				$interval($scope.products.load, $scope.products.updateIntervalInMilliseconds);
				$interval($scope.products.tick, $scope.products.statusUpdateIntervalInMilliseconds);
			},

			load: function()
			{
				expressProducts.getProducts($scope.resources.unitId)
					.then(function(products)
					{
						products = products || [];
						$scope.message = '';

						$scope.products.filter(products);
					})
					.catch(function()
					{
						$scope.message = $scope.resources.message;
						$scope.products.list = [];
					})
					.finally(function()
					{
						if (!$scope.banners.slideShowOn) $scope.banners.showNext();
					});
			},

			filter: function(products)
			{
				$scope.products.list = [];

				if (products.length == 0) return;

				for (var i = 0; i < products.length; i++)
				{
					if (products[i].PreparationTime > 420) continue;

					var product =
					{
						clientName: products[i].ClientName,
						clientIconPath: products[i].ClientIconPath,
						orderNumber: products[i].OrderNumber,
						productName: products[i].ProductName,
						productType: products[i].ProductTypeNumber,
						productState: products[i].StateNumber,
						verifiedStandart: products[i].VerifiedStandart,
						preparationTime: products[i].PreparationTime,
						preparationTimeTotalMinutes: timeTools.getTotalMinutes(products[i].PreparationTime),
						preparationTimeString: timeTools.getTimeFormat(products[i].PreparationTime),
						elementClass: 'b-product',
						statusElementClass: 'productStatus',
						productTimeStyle: '',
						status: $scope.resources.makingStatusName
					};

					if (products[i].ProductTypeNumber == productTypes.bakedProduct)
					{
						product.elementClass += ' baked';
					}
					else if (products[i].ProductTypeNumber == productTypes.compositeProduct)
					{
						product.elementClass += ' snack';
					}

					if (productPreparationStages.indexOf(products[i].StateNumber) >= 0)
					{
						product.elementClass += ' yellow';
						product.statusElementClass += ' normal';
						product.status = $scope.resources.makingStatusName;
					}
					else if (products[i].StateNumber == productionStage.waitingDelivery)
					{
						if (products[i].VerifiedStandart && products[i].ProductTypeNumber == productTypes.bakedProduct || products[i].ProductTypeNumber == productTypes.compositeProduct)
						{
							product.elementClass += ' green';
							product.statusElementClass += ' normal';
							product.productTimeStyle = 'text_green';
							product.status = $scope.resources.readyStatusName;
						}
						//else if (products[i].ProductTypeNumber == productTypes.compositeProduct)
						//{
						//	product.elementClass += ' green';
						//	product.statusElementClass += ' normal';
						//	product.status = $scope.resources.readyStatusName;
						//}
						else
						{
							product.elementClass += ' red';
							product.productTimeStyle = 'text_red';
							product.statusElementClass += ' critical';
							product.status = $scope.resources.notManagedStatusName;
						}
					}

					$scope.products.list.push(product);
				}

				$scope.products.tick(0, 0);
			},

			tick: function(tickCount, step)
			{
				if (step == null) step = -1;
				if ($scope.products.list.length == 0) return;

				var reloadProducts = false;
				var i = $scope.products.list.length;

				while (i > 0)
				{
					i -= 1;

					var product = $scope.products.list[i];

					product.preparationTime += step;

					if (product.productType == productTypes.compositeProduct)
					{
						product.preparationTimeString = product.status;
					}
					else if ($scope.products.list[i].productState == productionStage.waitingDelivery)
					{
						product.preparationTimeString = product.status;
					}
					else if (product.preparationTime > 60)
					{
						product.preparationTimeString = timeTools.getTimeFormat(product.preparationTime);
						product.statusElementClass = 'productStatus normal';
					}
					else
					{
						product.preparationTimeString = timeTools.getTimeFormat(product.preparationTime);
						product.statusElementClass = 'productStatus critical';
					}

					if (product.preparationTime == 0) reloadProducts = true;
				}

				if (reloadProducts)
				{
					$scope.products.load();
					return;
				}

				$scope.products.updateTable();
			},

			updateTable: function()
			{
				$scope.products.rows = [];
				if ($scope.products.list.length == 0) return;

				var currentSize = updateCurrentSize($scope.products.list.length);
				var rowsCount = currentSize.maxElementsCount / currentSize.columnsCount;

				for (var j = 0; j < rowsCount; j++)
				{
					var row = [];

					for (var i = 0; i < currentSize.columnsCount; i++)
					{
						var index = j * currentSize.columnsCount + i;

						if (index < $scope.products.list.length)
						{
							row.push($scope.products.list[index]);
						}
						else
						{
							row.push({ elementClass: 'b-product empty' });
						}
					}

					$scope.products.rows.push(row);
				}
			}
		};

		function updateCurrentSize(length)
		{
			if (length > display.size16.minElementsCount)
			{
				$scope.sizeClass = 'size16';

				return display.size16;
			}
			else if (length > display.size9.minElementsCount)
			{
				$scope.sizeClass = 'size9';

				return display.size9;
			}
			else if (length >= display.size4.minElementsCount)
			{
				$scope.sizeClass = 'size4';

				return display.size4;
			}
			else
			{
				$scope.sizeClass = 'size1';

				return display.size1;
			}
		}

		$scope.products.init();
		$scope.banners.init();
	});
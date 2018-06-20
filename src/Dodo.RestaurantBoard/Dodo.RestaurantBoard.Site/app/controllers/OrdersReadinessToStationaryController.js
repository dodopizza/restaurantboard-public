angular.module('app')
	.controller('OrdersReadinessToStationaryController', function ($scope, $interval, $timeout, $q, OrdersReadinessToStationary, $http) {
		function SizeParameters(maxElementsCount, minElementsCount, partitionsCount, fontRatio) {
			this.maxElementsCount = maxElementsCount;
			this.minElementsCount = minElementsCount;
			this.partitionsCount = partitionsCount;
			this.fontRatio = fontRatio;
		}

		var displayConstants =
		{
			size16: new SizeParameters(16, 9, 4, 70),
			size9: new SizeParameters(9, 4, 3, 55),
			size4: new SizeParameters(4, 2, 2, 35),
			size1: new SizeParameters(1, 0, 1, 20)
		};

		var rowCountClasses = {
			empty: "",
			count1: "restaurant-board_count_1",
			count2: "restaurant-board_count_2",
			count3: "restaurant-board_count_3",
			count5: "restaurant-board_count_5",
			count6: "restaurant-board_count_6",
			count7: "restaurant-board_count_7",
			count14: "restaurant-board_count_14"
		};

		var rowCount = {
			count2: 2,
			count3: 3,
			count4: 4,
			count5: 5,
			count6: 6,
			count7: 7,
			count14: 14
		}

		$scope.currentBannerIndex = 0;
		$scope.currentIndex = 0;
		$scope.currentPartitionLength = 0;
		$scope.orderLength = 0;

		$scope.resources = window.resources;

		$scope.message = "";
		$scope.newOrderArrived = false;
		$scope.currentPartition = null;
		$scope.partitions = [];
		$scope.prevOrderList = [];
		$scope.on = false;

		$scope.musicShow = false;
		$scope.track = "";

		function init() {
			$scope.loadOrders();
		}

		$scope.loadOrders = function () {
			OrdersReadinessToStationary.getOrders($scope.resources.unitId)
				.then(function (model) {
					$scope.message = "";
					$scope.newOrderArrived = model.NewOrderArrived;

					$scope.fillPartitions(model.ClientOrders);

					$scope.music(model.SongName);
				})
	    		.catch(function () {
	    			$scope.message = $scope.resources.message;
	    			$scope.partitions = [];
	    		})
	    		.finally(function () {
	    			if ($scope.newOrderArrived) document.getElementById('myTune').play();

	    			if (!$scope.banners.slideShowOn) $scope.banners.showNext();
	    		});
		}

		$scope.music = function (songName) {
			// Временное решение отображения Dodo FM для России, Казахстана, Узбекистана и Кырзыстана
			if (songName != '' && (window.resources.countryId == 643 || window.resources.countryId == 398 || window.resources.countryId == 417 || window.resources.countryId == 860)) {
				$scope.musicShow = true;
				$scope.track = '';
				$scope.track = songName;
			}
			else {
				$scope.musicShow = false;
				$scope.track = '';
			}

			//var url = "http://dodofm.ru:8080/api/history/?server=1&limit=1&callback=JSON_CALLBACK";
			//$http.jsonp(url)
			//	.success(function (data) {
			//		$scope.musicShow = true;
			//		console.log(data.objects[0].metadata);
			//		$scope.track = data.objects[0].metadata;
			//	})
			//	.error(function () {
			//		$scope.musicShow = false;
			//	});
		}

		$scope.banners =
		{
			containerStyle: {},

			slideShowOn: false,
			currentIndex: 0,
			updateIntervalInMilliseconds: 1800000,

			list: [],

			init: function () {
				$scope.banners.load();

				$interval($scope.banners.load, $scope.banners.updateIntervalInMilliseconds);
			},

			load: function () {
				OrdersReadinessToStationary.getBanners($scope.resources.countryId, $scope.resources.departmentId, $scope.resources.unitId)
					.then(function (banners) {
						$scope.banners.list = banners;
						$scope.message = '';

						if (!$scope.banners.slideShowOn) $scope.banners.showNext();
					})
					.catch(function () {
						$scope.message = $scope.resources.message;
					});
			},

			showNext: function () {
				if ($scope.partitions.length > 0) {
					$scope.banners.slideShowOn = false;

					return;
				}

				if ($scope.banners.list.length == 0) {
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

			clear: function () {
				$scope.banners.containerStyle = {};
			}
		};

		$scope.fillPartitions = function (orders) {
			$scope.partitions = [];
			$scope.currentSize = 1;
			var ordersLength = orders.length;

			if (ordersLength == 0) return;

			var currentSize = null;

			if (ordersLength > displayConstants.size16.minElementsCount) {
				currentSize = displayConstants.size16;
				$scope.currentSize = 16;
			}
			else if (ordersLength > displayConstants.size9.minElementsCount) {
				currentSize = displayConstants.size9;
				$scope.currentSize = 9;
			}
			else if (ordersLength >= displayConstants.size4.minElementsCount) {
				currentSize = displayConstants.size4;
				$scope.currentSize = 4;
			}
			else {
				currentSize = displayConstants.size1;
				$scope.currentSize = 1;
			}

			var difference = currentSize.maxElementsCount - ordersLength;
			var currentPartitionLength = Math.ceil(currentSize.maxElementsCount / currentSize.partitionsCount);

			for (var i = 0; i < difference; i++) {
				orders.push({ ClientName: "", OrderNumber: 0, ClientIconPath: "" });
			}

			for (var l = 0; l < currentPartitionLength; l++) {
				var currentPartition = [];

				for (var m = 0; m < currentSize.partitionsCount; m++) {
					currentPartition.push(orders[l * currentSize.partitionsCount + m]);
				}

				$scope.partitions.push(currentPartition);
			}

			// #begin New Restaurant Board
			// поддерживает ли браузер ед. измерения vh для изменения вида экрана
			var isCssPropertySupported = cssPropertyValueSupported("margin", "1vh");

			if (isCssPropertySupported) {
				if (ordersLength > 0 && ordersLength < rowCount.count2) {
					$scope.currentRowCountClass = rowCountClasses.count1;
				} else if (ordersLength === rowCount.count2) {
					$scope.currentRowCountClass = rowCountClasses.count2;
				} else if (ordersLength === rowCount.count3 || ordersLength === rowCount.count4) {
					$scope.currentRowCountClass = rowCountClasses.count3;
				} else if (ordersLength === rowCount.count5) {
					$scope.currentRowCountClass = rowCountClasses.count5;
				} else if (ordersLength === rowCount.count6) {
					$scope.currentRowCountClass = rowCountClasses.count6;
				} else if (ordersLength === rowCount.count7) {
					$scope.currentRowCountClass = rowCountClasses.count7;
				} else if (ordersLength > rowCount.count7) {
					$scope.currentRowCountClass = rowCountClasses.count14;
				} else {
					$scope.currentRowCountClass = null;
				}
			} else {
				if (ordersLength > 0 && ordersLength < rowCount.count5) {
					$scope.currentRowCountClass = rowCountClasses.count3;
				} else if (ordersLength === rowCount.count5) {
					$scope.currentRowCountClass = rowCountClasses.count5;
				} else if (ordersLength === rowCount.count6) {
					$scope.currentRowCountClass = rowCountClasses.count6;
				} else if (ordersLength >= rowCount.count7) {
					$scope.currentRowCountClass = rowCountClasses.count7;
				} else {
					$scope.currentRowCountClass = null;
				}
			}

			// определить трехначные числа, задать класс для всего табло
			for (var ind = 0; ind < ordersLength; ind++) {
				if (orders[ind].OrderNumber.toString().length >= 3) {
					$scope.currentRowCountClass += " restaurant-board_3x";
					break;
				}
			}

			$scope.restaurantOrders = orders;

			if ($scope.prevOrderList && $scope.prevOrderList.length) {
				var newOrders = findNewElementsInOrders($scope.prevOrderList, $scope.restaurantOrders);

				for (var t = 0; t < newOrders.length; t++) {
					for (var w = 0; w < $scope.restaurantOrders.length; w++) {
						if (parseInt(newOrders[t]) === $scope.restaurantOrders[w].OrderId) {
							$scope.restaurantOrders[w].rowClass = "restaurant-board__row_lighting";
						}
					}
				}

				$scope.prevOrderList = $scope.restaurantOrders;
			} else {
				//comm
				for (var p = 0; p < $scope.restaurantOrders.length; p++) {
					if ($scope.restaurantOrders[p].OrderId && typeof $scope.restaurantOrders[p].OrderId !== "undefined") {
						$scope.restaurantOrders[p].rowClass = "restaurant-board__row_lighting";
					}
				}

				$scope.prevOrderList = $scope.restaurantOrders;
			}

			// #end New Restaurant Board

			$scope.banners.clear();

			redrawBody(currentSize.fontRatio);
		}

		function redrawBody(ratio) {
			$('body').flowtype({ fontRatio: ratio });
		}

		function findNewElementsInOrders(prevOrders, orders) {
			var prevOrdersLen = prevOrders.length,
				ordersLen = orders.length,
				tempArr = [], diff = [];

			// пробегаемся по свежему списку заказов
			for (var i = 0; i < ordersLen; i++) {
				var orderId = orders[i].OrderId;

				if (typeof orderId !== 'undefined') {
					tempArr[orderId] = true;
				}
			}

			// пробегаемся по сохраненному списку заказов
			// удаляем элементы, которые были сохранены
			// остаются только новые
			for (var i = 0; i < prevOrdersLen; i++) {
				var prevOrderId = prevOrders[i].OrderId;

				if (typeof prevOrderId !== 'undefined') {
					if (tempArr[prevOrderId]) {
						delete tempArr[prevOrderId];
					}
				}
			}

			for (var k in tempArr) {
				diff.push(k);
			}

			return diff;
		}

		function cssPropertyValueSupported(prop, value) {
			var div = document.createElement('div');
			div.style[prop] = value;
			return div.style[prop] === value;
		}

		init();
		$scope.banners.init();

		var orderUpdateIntervalInMilliseconds = 15000;
		var bannerUpdateIntervalInMilliseconds = 1800000;

		$interval($scope.loadOrders, orderUpdateIntervalInMilliseconds);
		$interval($scope.loadBanners, bannerUpdateIntervalInMilliseconds);
	});
(function () {
    'use strict';

    function sendQrCtrl($scope, $controller, $http, $filter, guestFilterService, hubProxyService) {
        angular.extend(this, $controller('baseGuestController', { $scope: $scope }));

        var vm = this;

        var qrHubProxy = hubProxyService(hubProxyService.defaultServer, 'qrHub', { logging: true });

        qrHubProxy.on('notifyEmailSent', function (data) {
            var item = $filter('getByEmail')(guestFilterService.getGuestsData(), data);
            item.QrEmailSent = true;
            vm.sentEmailCount++;
        });

        vm.init = function () {
            vm.baseInit({ QrEmailSent: false });

            vm.guests = {};
            vm.guestsList = {};
            vm.shouldFilter = false;
            vm.searchText = "";
            vm.pageSize = 5;
            vm.currentPage = 1;
            vm.qrInProgress = false;

            $http.get("/Admin/Guest/GetAll")
                .then(function (result) {
                    guestFilterService.setGuestsData(result.data);
                    vm.guestsList = guestFilterService.getFilteredItems(false, "", 5, 1);
                    vm.tableOfPages = guestFilterService.getPages();
                }, function (errorData) {
                    console.log(errorData.message);
                });
        }

        vm.sendQrToAll = function () {
            vm.qrInProgress = true;
            $http.post("/Admin/Guest/SendQr", {
                connectionId: qrHubProxy.connection.id
            }).then(function (result) {
                vm.qrInProgress = false;
                console.log(result);
            }, function (errorData) {
                vm.qrInProgress = false;
                console.log(errorData);
            });
        }

        vm.sendQr = function (id) {
            vm.qrInProgress = true;
            $http.post("/Admin/Guest/SendQrEmail", {
                id: id
            }).then(function (result) {
                var item = $filter('getById')(guestFilterService.getGuestsData(), id);
                item.QrEmailSent = true;
                console.log(result);
            }, function (errorData) {
                console.log(errorData);
            });
        }
    }

    angular.module('adminApp')
        .controller('sendQrCtrl', sendQrCtrl);
})();
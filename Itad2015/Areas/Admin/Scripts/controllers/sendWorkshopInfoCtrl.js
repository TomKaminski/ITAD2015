(function () {
    'use strict';

    function sendWorkshopInfoCtrl($scope, $controller, $http, $filter, guestFilterService, hubProxyService) {
        angular.extend(this, $controller('baseGuestController', { $scope: $scope }));

        var vm = this;

        var qrHubProxy = hubProxyService(hubProxyService.defaultServer, 'qrHub', { logging: true });

        qrHubProxy.on('notifyEmailSent', function (data) {
            var item = $filter('getByEmail')(guestFilterService.getGuestsData(), data);
            item.AgendaEmailSent = true;
        });

        vm.init = function () {
            vm.baseInit({ AgendaEmailSent: false });

            vm.guests = {};
            vm.guestsList = {};
            vm.shouldFilter = false;
            vm.searchText = "";
            vm.pageSize = 5;
            vm.currentPage = 1;
            vm.emailInProgress = false;

            $http.get("/Admin/WorkshopGuest/GetAll")
                .then(function (result) {
                    guestFilterService.setGuestsData(result.data);
                    vm.guestsList = guestFilterService.getFilteredItems(false, "", 5, 1);
                    vm.tableOfPages = guestFilterService.getPages();
                }, function (errorData) {
                    console.log(errorData.message);
                });
        }

        vm.sendWorkshopInfoToAll = function () {
            vm.emailInProgress = true;
            $http.post("/Admin/WorkshopGuest/SendWorkshopInfoEmails", {
                connectionId: qrHubProxy.connection.id
            }).then(function (result) {
                vm.emailInProgress = false;
                console.log(result);
            }, function (errorData) {
                vm.emailInProgress = false;
                console.log(errorData);
            });
        }     
    }

    angular.module('adminApp')
        .controller('sendWorkshopInfoCtrl', sendWorkshopInfoCtrl);
})();
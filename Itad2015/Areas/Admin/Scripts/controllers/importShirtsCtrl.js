(function () {
    'use strict';

    function importShirtsCtrl($scope, $controller, $http, guestFilterService) {
        angular.extend(this, $controller('baseGuestController', { $scope: $scope }));

        var vm = this;

        vm.init = function () {
            vm.baseInit({ ShirtOrdered: false });

            vm.guests = {};
            vm.guestsList = {};
            vm.shouldFilter = false;
            vm.searchText = "";
            vm.pageSize = 5;
            vm.currentPage = 1;


            $http.get("/Admin/Guest/GetAll")
                .then(function (result) {
                    guestFilterService.setGuestsData(result.data);
                    vm.guestsList = guestFilterService.getFilteredItems(false, "", 5, 1);
                    vm.tableOfPages = guestFilterService.getPages();
                }, function (errorData) {
                    console.log(errorData.message);
                });
        }
    }

    angular.module('adminApp')
        .controller('importShirtsCtrl', importShirtsCtrl);
})();
(function () {
    'use strict';

    function guestAdminCtrl($http, $filter, hubProxyService, guestFilterService) {
        var vm = this;

        var guestHubProxy = hubProxyService(hubProxyService.defaultServer, 'guestHub', { logging: true });

        guestHubProxy.on('notifyCheck', function (data) {
            var item = $filter('getById')(guestFilterService.getGuestsData(), data.id);
            item.IsCheckIn = data.checkedIn;

            vm.guestsList = guestFilterService.getFilteredItems(vm.showCheckedIn, vm.searchText, vm.pageSize,vm.currentPage);
            vm.tableOfPages = guestFilterService.getPages();
        });

        vm.init = function () {
            vm.showCheckedIn = false;
            vm.searchText = "";
            vm.pageSize = 5;
            vm.currentPage = 1;
            vm.guests = {};
            vm.guestsList = {};

            $http.get("/Admin/Guest/GetAll")
                .then(function (result) {
                    guestFilterService.setGuestsData(result.data);

                    vm.guestsList = guestFilterService.getFilteredItems(vm.showCheckedIn, vm.searchText, vm.pageSize, vm.currentPage);
                    vm.tableOfPages = guestFilterService.getPages();
                }, function (errorData) {
                    console.log(errorData.message);
                });
        }

        vm.checkIn = function (id) {
            $http.post("/Admin/Guest/CheckIn", { id: id }).then(function (result) {
                if (result.data.status === true) {
                    guestHubProxy.invoke('notifyCheck', id, true);
                }
            }, function (error) {
                console.log(error);
            });
        }

        vm.checkOut = function (id) {
            $http.post("/Admin/Guest/CheckOut", { id: id }).then(function (result) {
                if (result.data.status === true) {
                    guestHubProxy.invoke('notifyCheck', id, false);
                }
            }, function (error) {
                console.log(error);
            });
        }

        vm.filterList = function () {
            vm.guestsList = guestFilterService.getFilteredItems(vm.showCheckedIn, vm.searchText, vm.pageSize, vm.currentPage);
            vm.tableOfPages = guestFilterService.getPages();
        }

        vm.setPage = function (page) {
            if (page <= 0)
                page = 1;
            if (page > vm.tableOfPages.length)
                page = vm.tableOfPages.length;

            vm.currentPage = page;

            vm.guestsList = guestFilterService.getFilteredItems(vm.showCheckedIn, vm.searchText, vm.pageSize, vm.currentPage);
            vm.tableOfPages = guestFilterService.getPages();
        }

        vm.setPageSize = function (size) {
            vm.pageSize = parseInt(size);
            
            if (vm.pageSize == undefined || vm.pageSize === 0) {
                vm.pageSize = 25;
            }

            vm.guestsList = guestFilterService.getFilteredItems(vm.showCheckedIn, vm.searchText, vm.pageSize, vm.currentPage);
            vm.tableOfPages = guestFilterService.getPages();
        }
    }

    angular.module('guestAdminApp')
        .controller('guestAdminCtrl', guestAdminCtrl);
})();
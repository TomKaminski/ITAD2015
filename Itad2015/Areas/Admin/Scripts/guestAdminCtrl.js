(function () {
    'use strict';

    function guestAdminCtrl($http, $filter, hubProxyService) {
        var vm = this;

        vm.showCheckedIn = false;
        vm.searchText = "";
        vm.pageSize = 5;
        vm.currentPage = 1;
        vm.guests = {};
        vm.guestsList = {};

        var guestHubProxy = hubProxyService(hubProxyService.defaultServer, 'guestHub', { logging: true });

        function filterGuests() {
            var filteredGuests;

            if (!vm.showCheckedIn) {
                filteredGuests = $filter('filter')(vm.guests, { IsCheckIn: false });
            } else {
                filteredGuests = vm.guests;
            }

            if (vm.searchText !== "") {
                filteredGuests = $filter('filter')(vm.guests, vm.searchText);
            }

            vm.pages = Math.ceil(filteredGuests.length / vm.pageSize);
            var pages = [];
            for (var i = 1; i <= vm.pages; i++) {
                pages.push(i);
            }

            vm.tableOfPages = pages;
            if (vm.currentPage > pages.length)
                vm.currentPage = 1;

            return filteredGuests.slice(vm.currentPage * vm.pageSize - vm.pageSize, vm.currentPage * vm.pageSize);
        }

        guestHubProxy.on('notifyCheck', function (data) {
            var item = $filter('getById')(vm.guests, data.id);
            item.IsCheckIn = data.checkedIn;
            vm.guestsList = filterGuests();
        });

        vm.init = function () {
            $http.get("/Admin/Guest/GetAll").then(function (result) {
                vm.guests = result.data;
                vm.guestsList = filterGuests();
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
                console.log("Nie udało się zameldować uczestnika o id: " + id);
                console.log("błąd:");
                console.log(error);
            });
        }
        vm.checkOut = function (id) {
            $http.post("/Admin/Guest/CheckOut", { id: id }).then(function (result) {
                if (result.data.status === true) {
                    guestHubProxy.invoke('notifyCheck', id, false);
                }
            }, function (error) {
                console.log("Nie udało się odmeldować uczestnika o id: " + id);
                console.log("błąd:");
                console.log(error);
            });
        }
        vm.filterList = function () {
            vm.guestsList = filterGuests();
        }

        vm.setPage = function (page) {
            if (page <= 0)
                page = 1;
            if (page > vm.pages)
                page = vm.pages;
            vm.currentPage = page;
            vm.guestsList = filterGuests();
        }

        vm.setPageSize = function (size) {
            vm.pageSize = parseInt(size);
            if (vm.pageSize == undefined)
                vm.pageSize = 25;
            vm.guestsList = filterGuests();
        }
    }

    angular.module('guestAdminApp')
        .controller('guestAdminCtrl', guestAdminCtrl);
})();
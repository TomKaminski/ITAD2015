(function () {
    'use strict';

    function importShirtsCtrl($http, $filter) {
        var vm = this;

        function getFilteredItems() {
            var filteredGuests = !vm.showAll ? $filter('filter')(vm.guests, { ShirtOrdered: false }) : vm.guests;

            if (vm.searchText !== "") {
                filteredGuests = $filter('filter')(filteredGuests, vm.searchText);
            }

            vm.tableOfPages = [];
            for (var i = 1; i <= Math.ceil(filteredGuests.length / vm.pageSize) ; i++) {
                vm.tableOfPages.push(i);
            }

            if (vm.currentPage > vm.tableOfPages.length)
                vm.currentPage = 1;

            return filteredGuests.slice(vm.currentPage * vm.pageSize - vm.pageSize, vm.currentPage * vm.pageSize);
        }

        vm.init = function () {
            vm.showAll = false;
            vm.searchText = "";
            vm.pageSize = 5;
            vm.currentPage = 1;
            vm.guests = {};
            vm.guestsList = {};

            $http.get("/Admin/Excel/GetAllShirtsData")
                .then(function (result) {
                    vm.guests = result.data;
                    vm.guestsList = getFilteredItems();
                }, function (errorData) {
                    console.log(errorData);
                });
        }
        vm.filterList = function () {
            vm.guestsList = getFilteredItems();
        }

        vm.setPage = function (page) {
            if (page <= 0)
                page = 1;
            if (page > vm.tableOfPages.length)
                page = vm.tableOfPages.length;

            vm.currentPage = page;
            vm.guestsList = getFilteredItems();
        }

        vm.setPageSize = function (size) {
            vm.pageSize = parseInt(size);

            if (vm.pageSize == undefined || vm.pageSize === 0) {
                vm.pageSize = 25;
            }
            vm.guestsList = getFilteredItems();
        }
    }

    angular.module('importShirtsApp')
        .controller('importShirtsCtrl', importShirtsCtrl);
})();
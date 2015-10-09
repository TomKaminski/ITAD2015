angular.module('guestAdminApp', []);

(function () {
    function getById() {
        return function (input, id) {
            var i = 0, len = input.length;
            for (; i < len; i++) {
                if (+input[i].Id === +id) {
                    return input[i];
                }
            }
            return null;
        }
    };

    angular.module('guestAdminApp').filter('getById', getById);
})();


(function() {
    'use strict';

    function guestAdminCtrl($http, $filter) {
        var vm = this;
        vm.guests = [];
        vm.showCheckedIn = false;
        vm.searchText = "";
        vm.pageSize = 1;
        vm.currentPage = 1;

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

        vm.init = function() {
            $http.get("/Admin/Guest/GetAll").then(function(result) {
                vm.guests = result.data;
                vm.guestsList = filterGuests();
            }, function(errorData) {
                console.log(errorData.message);
            });
        }

        vm.checkIn = function(id) {
            $http.post("/Admin/Guest/CheckIn", { id: id }).then(function(result) {
                if (result.data.status === true) {
                    var item = $filter('getById')(vm.guests, id);
                    item.IsCheckIn = true;
                }
            }, function(error) {
                console.log("Nie udało się zameldować uczestnika o id: " + id);
                console.log("błąd:");
                console.log(error);
            });
        }

        vm.checkOut = function (id) {
            $http.post("/Admin/Guest/CheckOut", { id: id }).then(function (result) {
                if (result.data.status === true) {
                    var item = $filter('getById')(vm.guests, id);
                    item.IsCheckIn = false;
                }
            }, function(error) {
                console.log("Nie udało się odmeldować uczestnika o id: " + id);
                console.log("błąd:");
                console.log(error);
            });
        }
        vm.filterList = function() {
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
    }
    

    angular.module('guestAdminApp')
        .controller('guestAdminCtrl', guestAdminCtrl);
})();
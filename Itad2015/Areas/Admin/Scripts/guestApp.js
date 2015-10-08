angular.module('guestAdminApp', []);

(function() {
    'use strict';

    function guestAdminCtrl($http) {
        var vm = this;
        vm.guests = [];
        vm.init = function() {
            $http.get("/Admin/Guest/GetAll").then(function(result) {
                vm.guests = result.data;
            }, function(errorData) {
                console.log(errorData.message);
            });
        }

        vm.checkIn = function(id) {
            $http.post("/Admin/Guest/CheckIn", new { id: id }).then(function(data) {

            }, function(error) {

            });
        }

        vm.CheckOut = function (id) {
            $http.post("/Admin/Guest/CheckOut", new { id: id }).then(function (data) {

            }, function(error) {

            });
        }
    }

    angular.module('guestAdminApp')
        .controller('guestAdminCtrl', guestAdminCtrl);
})();
(function () {
    'use strict';

    function guestAdminCtrl($scope, $http, $filter, $controller, hubProxyService, guestFilterService, registeredPersonService) {
        angular.extend(this, $controller('baseGuestController', { $scope: $scope }));

        var vm = this;

        var guestHubProxy = hubProxyService(hubProxyService.defaultServer, 'guestHub', { logging: true });
        var checkInHub = hubProxyService(hubProxyService.defaultServer, 'checkInHub', { logging: true });

        guestHubProxy.on('notifyCheck', function (data) {
            var item = $filter('getById')(guestFilterService.getGuestsData(), data.id);
            item.IsCheckIn = data.checkedIn;

            vm.guestsList = guestFilterService.getFilteredItems(vm.shouldFilter, vm.searchText, vm.pageSize, vm.currentPage);
            vm.tableOfPages = guestFilterService.getPages();
        });

        checkInHub.on('notifyUserConnectedCallback', function () {
            vm.connectedToDevice = true;
        });

        checkInHub.on('notifyDeviceConnected', function() {
            vm.deviceOnline = true;
        });

        checkInHub.on('checkDeviceOnline', function () {
            vm.deviceOnline = true;
        });

        vm.userAwaits = function() {
            return registeredPersonService.userAwaits();
        }

        vm.userInitials = function () {
            return registeredPersonService.userInitials();
        }

        vm.userEmail= function () {
            return registeredPersonService.userEmail();
        }

        vm.userShirt = function () {
            return registeredPersonService.userShirt();
        }

        function checkIn(id) {
            $http.post("/Admin/Guest/CheckIn", { id: id }).then(function (result) {
                if (result.data.status === true) {
                    guestHubProxy.invoke('notifyCheck', id, true);
                }
            }, function (error) {
                console.log(error);
            });
        }

        checkInHub.on('lockDevice', function (data) {
            if (data.Status === true) {
                registeredPersonService.fillPerson(data);
                vm.modalStatus = true;
                var item = $filter('getByEmail')(guestFilterService.getGuestsData(), registeredPersonService.userEmail());
                checkIn(item.Id);
            } else {
                vm.modalStatus = false;
                vm.errorValue = data.Error;
            }
            $('#appModal').modal('show');
        });

        vm.init = function (userEmail) {
            vm.baseInit({ IsCheckIn: false });
            vm.connectedToDevice = false;
            vm.deviceBlocked = false;
            vm.deviceOnline = false;
            vm.guests = {};
            vm.guestsList = {};
            vm.shouldFilter = false;
            vm.searchText = "";
            vm.pageSize = 5;
            vm.currentPage = 1;
            vm.userEmail = userEmail;

            $http.get("/Admin/Guest/GetAll")
                .then(function (result) {
                    guestFilterService.setGuestsData(result.data);
                    vm.guestsList = guestFilterService.getFilteredItems(false, "", 5, 1);
                    vm.tableOfPages = guestFilterService.getPages();
                }, function (errorData) {
                    console.log(errorData.message);
                });
        }
        vm.checkIn = function (id) {
            checkIn(id);
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

        vm.blockDevice = function() {
            checkInHub.invoke('LockDevice', vm.userEmail);
        }

        vm.unblockDevice = function () {
            vm.modalStatus = null;
            vm.errorValue = null;
            registeredPersonService.clearPerson();
            $('#appModal').modal('hide');
            checkInHub.invoke('UnlockDevice', vm.userEmail);
        }

        vm.connectToDevice = function() {
            checkInHub.invoke('connect', vm.userEmail, checkInHub.connection.id, 1);
            checkInHub.invoke('checkDeviceOnline', vm.userEmail);
        }
    }

    angular.module('adminApp')
        .controller('guestAdminCtrl', guestAdminCtrl);
})();
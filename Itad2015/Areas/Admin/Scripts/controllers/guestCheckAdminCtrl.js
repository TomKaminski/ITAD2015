(function () {
    'use strict';

    function guestAdminCtrl($scope, $http, $filter, $controller, hubProxyService, guestFilterService, registeredPersonService, appEmailService) {
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

        checkInHub.on('notifyDeviceConnected', function () {
            vm.deviceOnline = true;
        });

        checkInHub.on('notifyDeviceDisconnected', function () {
            vm.deviceOnline = false;
        });

        vm.userAwaits = function () {
            return registeredPersonService.userAwaits();
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
                vm.userResultEmail = registeredPersonService.userEmail();
                vm.userShirt = registeredPersonService.userShirt();
                vm.userInitials = registeredPersonService.userInitials();
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
            appEmailService.setEmail(userEmail);

            checkInHub.start(function () {
                checkInHub.invoke('connect', appEmailService.getEmail(), checkInHub.connection.id, 1);
                checkInHub.invoke('checkDeviceOnline', vm.userEmail);
            });

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

        vm.blockDevice = function () {
            checkInHub.invoke('LockDevice', vm.userEmail);
        }

        function checkIfDeviceCallbackOccures() {
            setTimeout(function () {
                if (vm.modalStatus != null) {
                    checkIfDeviceCallbackOccures();
                }
            }, 3000);
        }

        vm.unblockDevice = function () {
            checkInHub.invoke('UnlockDevice', vm.userEmail);
            checkIfDeviceCallbackOccures();
        }
        checkInHub.on('unlockDeviceUserCallback', function () {
            vm.modalStatus = null;
            vm.errorValue = null;
            registeredPersonService.clearPerson();
            $('#appModal').modal('hide');
        });

        vm.connectToDevice = function () {
            checkInHub.start(function () {
                checkInHub.invoke('connect', appEmailService.getEmail(), checkInHub.connection.id, 1);
                checkInHub.invoke('checkDeviceOnline', vm.userEmail);
            });
        }
    }

    angular.module('adminApp')
        .controller('guestAdminCtrl', guestAdminCtrl);
})();
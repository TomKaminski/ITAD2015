(function () {
    'use strict';

    function excelAdminCtrl($http, $filter, hubProxyService) {
        var vm = this;

        var excelHubProxy = hubProxyService(hubProxyService.defaultServer, 'excelHub', { logging: true });

        excelHubProxy.on('notifyEmailSent', function (data) {
            var item = $filter('getByEmail')(vm.data, data);
            item.EmailSent = true;
            vm.sentEmailCount++;
        });

        vm.init = function (workSheetNumber, emailPosition, namePosition, lastNamePosition, fileName, hasHeader) {
            vm.workSheetNumber = workSheetNumber;
            vm.emailPosition = emailPosition;
            vm.namePosition = namePosition;
            vm.lastNamePosition = lastNamePosition;
            vm.fileName = fileName;
            vm.hasHeader = hasHeader.toLowerCase() === "true";
            vm.data = {}
            vm.sentEmailCount = 0;
            vm.sendBtnClicked = false;

            $http.post("/Admin/Excel/GetUploadedFileData", {
                WorkSheetNumber: vm.workSheetNumber, EmailPosition: vm.emailPosition,
                NamePosition: vm.namePosition, LastNamePosition: vm.lastNamePosition,
                FileName: vm.fileName, HasHeader: vm.hasHeader
            }).then(function (result) {
                vm.data = result.data;
            }, function (errorData) {
                console.log(errorData.message);
            });
        }

        vm.inviteAll = function () {
            debugger;
            vm.sendBtnClicked = true;
            $http.post("/Admin/Excel/SendInvites", {
                WorkSheetNumber: vm.workSheetNumber, EmailPosition: vm.emailPosition,
                NamePosition: vm.namePosition, LastNamePosition: vm.lastNamePosition,
                FileName: vm.fileName, HasHeader: vm.hasHeader, ConnectionId: excelHubProxy.connection.id
            }).then(function (result) {
                console.log(result);
            }, function (errorData) {
                console.log(errorData);
            });
        }

    }

    angular.module('excelAdminApp')
        .controller('excelAdminCtrl', excelAdminCtrl);
})();
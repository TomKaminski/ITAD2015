(function () {
    'use-strict';

    function appEmailService() {
        var appEmail;

        var setEmail = function (email) {
            appEmail = email;
        };

        var clearEmail = function () {
            appEmail = null;
        };

        var getEmail = function () {
            return appEmail;
        }

        return {
            setEmail: setEmail,
            clearEmail: clearEmail,
            getEmail: getEmail
        };
    }

    angular.module('adminApp').service('appEmailService', appEmailService);
})();

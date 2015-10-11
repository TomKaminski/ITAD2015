(function () {
    'use strict';

    function getByEmail() {
        return function (input, email) {
            var i = 0, len = input.length;
            for (; i < len; i++) {
                if (input[i].Email === email) {
                    return input[i];
                }
            }
            return null;
        }
    };

    angular.module('excelAdminApp').filter('getByEmail', getByEmail);
})();
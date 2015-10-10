//filter by ID
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
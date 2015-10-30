(function () {
    'use strict';

    var sizeEnum = [
        "damska S",
        "damska M",
        "damska L",
        "damska XL",
        "damska XXL",
        "męska S",
        "męska M",
        "męska L",
        "męska XL",
        "męska XXL",
        "Brak"
    ];

    function registeredPersonService() {
        var registeredPerson = {};

        var fillPerson = function (person) {
            var r = person.Person;
            registeredPerson = {
                fullName: r.FirstName + " " + r.LastName,
                email: r.Email,
                size: sizeEnum[r.Size+1]
            };
        };

        var clearPerson = function () {
            registeredPerson = {};
        }

        var userInitials = function () {
            return registeredPerson.fullName;
        }

        var userAwaits = function() {
            return registeredPerson.email!=null;
        }

        var userEmail = function() {
            return registeredPerson.email;
        }

        var userShirt = function() {
            return registeredPerson.size;
        }

        return {
            fillPerson: fillPerson,
            userInitials: userInitials,
            clearPerson: clearPerson,
            userAwaits: userAwaits,
            userShirt: userShirt,
            userEmail: userEmail
        };
    }

    angular.module('adminApp').service('registeredPersonService', registeredPersonService);
})();

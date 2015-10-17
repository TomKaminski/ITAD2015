(function() {
    'use strict';

    function guestFilterService($filter) {
        
        var guestsData = {}
        var tableOfPages = [];
        
        function setGuestsData(data) {
            guestsData = data;
        }

        function getGuestsData() {
            return guestsData;
        }

        function getPages() {
            return tableOfPages;
        }

        function getFilteredItems(showCheckedIn, searchText, pageSize, currentPage) {
            var filteredGuests = !showCheckedIn ? $filter('filter')(guestsData, { IsCheckIn: false }) : guestsData;

            if (searchText !== "") {
                filteredGuests = $filter('filter')(filteredGuests, searchText);
            }

            tableOfPages = [];
            for (var i = 1; i <= Math.ceil(filteredGuests.length / pageSize) ; i++) {
                tableOfPages.push(i);
            }

            if (currentPage > tableOfPages.length)
                currentPage = 1;

            return filteredGuests.slice(currentPage * pageSize - pageSize, currentPage * pageSize);
        }

        return {
            getPages: getPages,
            getFilteredItems: getFilteredItems,
            setGuestsData: setGuestsData,
            getGuestsData: getGuestsData
        }
    }

    angular.module('guestAdminApp').factory('guestFilterService', guestFilterService);
})();
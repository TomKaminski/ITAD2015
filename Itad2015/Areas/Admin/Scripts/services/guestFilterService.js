(function() {
    'use strict';

    function guestFilterService($filter) {
        
        var guestsData = {}
        var tableOfPages = [];
        var filterBy = {};
        
        function setFilterBy(fB) {
            filterBy = fB;
        }

        function setGuestsData(data) {
            guestsData = data;
        }

        function getGuestsData() {
            return guestsData;
        }

        function getPages() {
            return tableOfPages;
        }

        function getFilteredItems(shouldFilter, searchText, pageSize, currentPage) {
            var filteredGuests = !shouldFilter ? $filter('filter')(guestsData, filterBy) : guestsData;

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
            setFilterBy: setFilterBy,
            getPages: getPages,
            getFilteredItems: getFilteredItems,
            setGuestsData: setGuestsData,
            getGuestsData: getGuestsData
        }
    }

    angular.module('adminApp').factory('guestFilterService', guestFilterService);
})();
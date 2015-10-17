(function () {
    'use strict';

    function baseGuestController($scope, guestFilterService) {
        this.baseInit = function (filterByProperty) {
            guestFilterService.setFilterBy(filterByProperty);
        }

        this.filterList = function () {
            this.guestsList = guestFilterService.getFilteredItems(this.shouldFilter, this.searchText, this.pageSize, this.currentPage);
            this.tableOfPages = guestFilterService.getPages();
        }

        this.setPage = function (page) {
            if (page <= 0)
                page = 1;
            if (page > this.tableOfPages.length)
                page = this.tableOfPages.length;

            this.currentPage = page;

            this.guestsList = guestFilterService.getFilteredItems(this.shouldFilter, this.searchText, this.pageSize, this.currentPage);
            this.tableOfPages = guestFilterService.getPages();
        }

        this.setPageSize = function (size) {
            this.pageSize = parseInt(size);

            if (this.pageSize == undefined || this.pageSize === 0) {
                this.pageSize = 25;
            }

            this.guestsList = guestFilterService.getFilteredItems(this.shouldFilter, this.searchText, this.pageSize, this.currentPage);
            this.tableOfPages = guestFilterService.getPages();
        }
    }

    angular.module('adminApp')
        .controller('baseGuestController', baseGuestController);
})();
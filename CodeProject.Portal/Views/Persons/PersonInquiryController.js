
console.log("person inquiry");

angular.module("codeProject").register.controller('personInquiryController', ['$routeParams', '$location', 'ajaxService', 'dataGridService', 'alertService',
    function ($routeParams, $location, ajaxService, dataGridService, alertService) {

    "use strict";

    var vm = this;

    this.initializeController = function () {

        vm.title = "Person Inquiry";

        vm.alerts = [];
        vm.closeAlert = alertService.closeAlert;

        dataGridService.initializeTableHeaders();

        dataGridService.addHeader("Person Code", "PersonCode");
        dataGridService.addHeader("Company Name", "CompanyName");
        dataGridService.addHeader("Contact Name", "ContactName");
        dataGridService.addHeader("City", "City");
        dataGridService.addHeader("Region", "Region");

        vm.tableHeaders = dataGridService.setTableHeaders();
        vm.defaultSort = dataGridService.setDefaultSort("Company Name");

        vm.currentPageNumber = 1;
        vm.sortExpression = "CompanyName";
        vm.sortDirection = "ASC";
        vm.pageSize = 15;
      
        this.executeInquiry();

    }

    this.closeAlert = function (index) {
        vm.alerts.splice(index, 1);
    };

    this.changeSorting = function (column) {

        dataGridService.changeSorting(column, vm.defaultSort, vm.tableHeaders);

        vm.defaultSort = dataGridService.getSort();
        vm.sortDirection = dataGridService.getSortDirection();
        vm.sortExpression = dataGridService.getSortExpression();
        vm.currentPageNumber = 1;

        vm.executeInquiry();

    };

    this.setSortIndicator = function (column) {
        return dataGridService.setSortIndicator(column, vm.defaultSort);
    };

    this.pageChanged = function () {
        this.executeInquiry();
    }

    this.executeInquiry = function () {
        var inquiry = vm.prepareSearch();
        ajaxService.ajaxPost(inquiry, "api/PersonService/GetPersons", this.getPersonsOnSuccess, this.getPersonsOnError);
    }

    this.prepareSearch = function () {

        var inquiry = new Object();
      
        inquiry.currentPageNumber = vm.currentPageNumber;
        inquiry.sortExpression = vm.sortExpression;
        inquiry.sortDirection = vm.sortDirection;
        inquiry.pageSize = vm.pageSize;
        
        return inquiry;

    }

    this.getPersonsOnSuccess = function (response) {
        vm.persons = response.persons;
        vm.totalPersons = response.totalRows;
        vm.totalPages = response.totalPages;
    }

    this.getPersonsOnError = function (response) {
        alertService.RenderErrorMessage(response.ReturnMessage);
    }


}]);

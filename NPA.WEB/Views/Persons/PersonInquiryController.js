
console.log("person inquiry");

angular.module("npaAngularJS").register.controller('personInquiryController', ['$scope', '$uibModal', '$routeParams', '$location', 'ajaxService', 'dataGridService', 'alertService',
    function ($scope, $uibModal, $routeParams, $location, ajaxService, dataGridService, alertService) {

    "use strict";

    var vm = this;

    this.initializeController = function () {

        vm.title = "People";

        vm.alerts = [];
        vm.closeAlert = alertService.closeAlert;

        dataGridService.initializeTableHeaders();


        dataGridService.addHeader("Person ID", "PersonID");
        dataGridService.addHeader("Name", "Name");
        dataGridService.addHeader("Company Name", "CompanyName");
        dataGridService.addHeader("City", "City");
        dataGridService.addHeader("Region", "Region");

        vm.tableHeaders = dataGridService.setTableHeaders();
        vm.defaultSort = dataGridService.setDefaultSort("Person ID", true);

        vm.currentPageNumber = 1;
        vm.sortExpression = "PersonID";
        vm.sortDirection = "DESC";
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

        var array = [];

        for (var a = 0; a < vm.persons.length; a++) {
            array[vm.persons[a].personID] = false;
        }

        $scope.status = {
            isopen: array
        };
    }

    this.getPersonsOnError = function (response) {
        alertService.RenderErrorMessage(response.ReturnMessage);
    }

    this.viewPerson = function (personID) {
        var person = new Object();
        person.personID = personID;
        ajaxService.ajaxPost(person, "api/PersonService/GetPerson", this.viewPersonOnSuccess, this.viewPersonOnError);
    }

    this.viewPersonOnSuccess = function (response) {
        vm.ViewPerson = new Object();
        vm.ViewPerson.personID = response.personID;
        vm.ViewPerson.companyName = response.companyName;
        vm.ViewPerson.name = response.name;
        vm.ViewPerson.contactTitle = response.contactTitle;
        vm.ViewPerson.address = response.address;
        vm.ViewPerson.city = response.city;
        vm.ViewPerson.region = response.region;
        vm.ViewPerson.country = response.country;
        vm.ViewPerson.mobileNumber = response.mobileNumber;
        vm.ViewPerson.imageUrl = response.imageUrl;

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'Views/Persons/PersonView.html',
            controller: 'modalInstanceController',
            size: 'lg',
            resolve: {
                ViewPerson: function () {
                    return vm.ViewPerson;
                }
            }
        });
    };

    this.viewPersonOnError = function (response) {
    };

    this.deletePerson = function (personID) {
        var inquiry = new Object();
        inquiry.personID = personID;
        ajaxService.ajaxPost(inquiry, "api/PersonService/DeletePerson", this.deletePersonOnSuccess, this.deletePersonOnError);
    }

    this.deletePersonOnSuccess = function (response) {
        vm.actionOnSuccess(response);
    }

    this.deletePersonOnError = function (response) {
        vm.actionOnError(response);
    }

    this.activatePerson = function (person) {
        var inquiry = new Object();
        inquiry.personID = person.personID;
        inquiry.IsActive = !person.isActive;

        ajaxService.ajaxPost(inquiry, "api/PersonService/ActivatePerson", this.activatePersonOnSuccess, this.activatePersonOnError);
    }

    this.activatePersonOnSuccess = function (response) {
        vm.actionOnSuccess(response);
    }

    this.activatePersonOnError = function (response) {
        vm.actionOnError(response);
    }

    this.actionOnSuccess = function (response) {
        alertService.renderSuccessMessage(response.returnMessage);
        vm.messageBox = alertService.returnFormattedMessage();
        vm.alerts = alertService.returnAlerts();
        vm.executeInquiry();
    }


    this.actionOnError = function (response) {
        alertService.renderErrorMessage(response.returnMessage);
        vm.messageBox = alertService.returnFormattedMessage();
        vm.alerts = alertService.returnAlerts();
    }
}]);


angular.module('npaAngularJS').register.controller('modalInstanceController', [
    '$scope', '$uibModalInstance', 'ViewPerson', function ($scope, $uibModalInstance, ViewPerson) {
        $scope.ViewPerson = ViewPerson;
    }
]);
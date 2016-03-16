
console.log("person handling");

angular.module("npaAngularJS").register.controller('personHandlingController', ['$scope', 'Upload', '$routeParams', '$location', 'ajaxService', 'alertService',
    function ($scope, Upload, $routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function() {

            vm.title = "Person Handling";

            vm.messageBox = "";
            vm.alerts = [];

            var personID = ($routeParams.id || "");

            if (personID == "") {
                vm.title = "Create Person";
                vm.personID = "0";
            } else {
                vm.title = "Update Person";
                vm.personID = personID;
                var person = new Object();
                person.personID = personID;
                ajaxService.ajaxPost(person, "api/PersonService/GetPerson", this.getPersonOnSuccess, this.getPersonOnError);
            }

        }

        this.closeAlert = function(index) {
            vm.alerts.splice(index, 1);
        };

        this.getPersonOnSuccess = function(response) {

            vm.personID = response.personID;
            vm.companyName = response.companyName;
            vm.name = response.name;
            vm.contactTitle = response.contactTitle;
            vm.address = response.address;
            vm.city = response.city;
            vm.region = response.region;
            vm.country = response.country;
            vm.mobileNumber = response.mobileNumber;
            vm.imageUrl = response.imageUrl;
        }

        this.getPersonOnError = function(response) {

        }


        this.savePerson = function() {

            var person = new Object();
            person.personID = vm.personID;
            person.companyName = vm.companyName;
            person.name = vm.name;
            person.contactTitle = vm.contactTitle;
            person.address = vm.address;
            person.city = vm.city;
            person.region = vm.region;
            person.country = vm.country;
            person.mobileNumber = vm.mobileNumber;

            Upload.base64DataUrl(vm.picFile).then(function (url) {

                person.ImageUrl = url;
                if (person.personID == "0") {
                    ajaxService.ajaxPost(person, "api/PersonService/CreatePerson", vm.createPersonOnSuccess, vm.createPersonOnError);
                } else {
                    ajaxService.ajaxPost(person, "api/PersonService/UpdatePerson", vm.updatePersonOnSuccess, vm.updatePersonOnError);
                }
            });
        }

        this.createPersonOnSuccess = function(response) {
            $location.path('/Persons/PersonRegistry');
        }

        this.createPersonOnError = function(response) {
            vm.clearValidationErrors();
            alertService.renderErrorMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            alertService.setValidationErrors(vm, response.validationErrors);
        }

        this.updatePersonOnSuccess = function(response) {
            $location.path('/Persons/PersonRegistry');
        }

        this.updatePersonOnError = function(response) {
            vm.clearValidationErrors();
            alertService.renderErrorMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            alertService.setValidationErrors(vm, response.validationErrors);
        }

        this.clearValidationErrors = function() {
            vm.nameInputError = false;
            vm.companyNameInputError = false;
        }
    }
]);

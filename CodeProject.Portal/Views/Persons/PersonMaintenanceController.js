
console.log("person maintenance");

angular.module("codeProject").register.controller('personMaintenanceController',
    ['$routeParams', '$location', 'ajaxService', 'alertService',
    function ($routeParams, $location, ajaxService, alertService) {

    "use strict";

    var vm = this;

    this.initializeController = function () {

        vm.title = "Person Maintenance";

        vm.messageBox = "";
        vm.alerts = [];

        var personID = ($routeParams.id || "");

        if (personID == "")
        {
            vm.personID = "0";
            vm.personCode = "Micro";
            vm.companyName = "Microsoft Corporation";
            vm.contactName = "William Gates";
            vm.contactTitle = "Founder & CEO";
            vm.address = "One Microsoft Way";
            vm.city = "Redmond";
            vm.region = "WA";
            vm.postalCode = "98052-7329";
            vm.country = "USA";
            vm.phoneNumber = "(425) 882-8080";
            vm.mobileNumber = "(425) 706-7329";           
        }
        else
        {
            vm.personID = personID;
            var person = new Object();
            person.personID = personID                  
            ajaxService.ajaxPost(person, "api/PersonService/GetPerson", this.getPersonOnSuccess, this.getPersonOnError);            
        }
              
    }

    this.closeAlert = function (index) {
        vm.alerts.splice(index, 1);
    };

    this.getPersonOnSuccess = function (response) {

        vm.personCode = response.personCode;
        vm.companyName = response.companyName;
        vm.contactName = response.contactName;
        vm.contactTitle = response.contactTitle;
        vm.address = response.address;
        vm.city = response.city;
        vm.region = response.region;
        vm.postalCode = response.postalCode;
        vm.country = response.country;
        vm.phoneNumber = response.phoneNumber;
        vm.mobileNumber = response.mobileNumber;

    }
        
    this.getPersonOnError = function (response) {

    }

    
    this.savePerson = function () {

        var person = new Object();
        person.personID = vm.personID
        person.personCode = vm.personCode;
        person.companyName = vm.companyName;
        person.contactName = vm.contactName;
        person.contactTitle = vm.contactTitle;
        person.address = vm.address;
        person.city = vm.city;
        person.region = vm.region;
        person.postalCode = vm.postalCode;
        person.country = vm.country;
        person.phoneNumber = vm.phoneNumber;
        person.mobileNumber = vm.mobileNumber;
   
        if (person.personID=="0") {
            ajaxService.ajaxPost(person, "api/PersonService/CreatePerson", this.createPersonOnSuccess, this.createPersonOnError);
        }
        else {
            ajaxService.ajaxPost(person, "api/PersonService/UpdatePerson", this.updatePersonOnSuccess, this.updatePersonOnError);
        }

    }

    this.createPersonOnSuccess = function (response)
    {
        vm.clearValidationErrors();
        alertService.renderSuccessMessage(response.returnMessage);
        vm.messageBox = alertService.returnFormattedMessage();
        vm.alerts = alertService.returnAlerts();
        vm.personID = response.personID;
    }

    this.createPersonOnError = function (response) {
        vm.clearValidationErrors();
        alertService.renderErrorMessage(response.returnMessage);
        vm.messageBox = alertService.returnFormattedMessage();
        vm.alerts = alertService.returnAlerts();
        alertService.setValidationErrors(vm, response.validationErrors);
    }

    this.updatePersonOnSuccess = function (response)
    {
        vm.clearValidationErrors();
        alertService.renderSuccessMessage(response.returnMessage);
        vm.messageBox = alertService.returnFormattedMessage();
        vm.alerts = alertService.returnAlerts();
    }

    this.updatePersonOnError = function (response) {
        vm.clearValidationErrors();
        alertService.renderErrorMessage(response.returnMessage);
        vm.messageBox = alertService.returnFormattedMessage();
        vm.alerts = alertService.returnAlerts();
        alertService.setValidationErrors(vm, response.validationErrors);
    }

    this.clearValidationErrors = function () {
        vm.personCodeInputError = false;
        vm.companyNameInputError = false;
    } 

}]);

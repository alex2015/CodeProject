console.log("NpaAngularJS Bootstrap START");

(function () {

    var app = angular.module('npaAngularJS', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ngSanitize', 'blockUI', 'ngFileUpload']);

    app.config(['$controllerProvider', '$provide', function ($controllerProvider, $provide) {
        app.register =
          {
              controller: $controllerProvider.register,
              service: $provide.service
          };
    }]);

})();

console.log("NpaAngularJS Bootstrap FINISHED");





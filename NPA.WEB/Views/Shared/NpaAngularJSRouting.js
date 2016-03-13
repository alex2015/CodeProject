
console.log("code project routing");

angular.module("npaAngularJS").config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {

    var baseSiteUrlPath = $("base").first().attr("href");
   
    $routeProvider.when('/:section/:tree',
    {
        templateUrl: function (rp) { return baseSiteUrlPath + 'views/' + rp.section + '/' + rp.tree + '.html' },

        resolve: {

            load: ['$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                var path = $location.path().split("/");
                var directory = path[1];
                var controllerName = path[2];
               
                var controllerToLoad = "Views/" + directory + "/" + controllerName + "Controller.js";

                var deferred = $q.defer();
                require([controllerToLoad], function () {
                    $rootScope.$apply(function () {
                        deferred.resolve();
                    });
                });

                return deferred.promise;
              
            }]
        }


    });

    $routeProvider.when('/:section/:tree/:id',
    {
        templateUrl: function (rp) { return baseSiteUrlPath + 'views/' + rp.section + '/' + rp.tree + '.html' },

        resolve: {

            load: ['$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                var path = $location.path().split("/");
                var directory = path[1];
                var controllerName = path[2];

                var controllerToLoad = "Views/" + directory + "/" + controllerName + "Controller.js";

                var deferred = $q.defer();
                require([controllerToLoad], function () {
                    $rootScope.$apply(function () {
                        deferred.resolve();
                    });
                });

                return deferred.promise;

            }]
        }

    });

    $routeProvider.when('/',
    {

        templateUrl: function (rp) { return baseSiteUrlPath + 'views/Home/Index.html'; },

        resolve: {

            load: ['$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                var controllerToLoad = "Views/Home/IndexController.js";

                var deferred = $q.defer();
                require([controllerToLoad], function () {
                    $rootScope.$apply(function () {
                        deferred.resolve();
                    });
                });

                return deferred.promise;

            }]
        }


    });

    $locationProvider.html5Mode(true);  

}]);




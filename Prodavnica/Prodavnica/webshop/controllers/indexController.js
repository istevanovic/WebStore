(function () {
    'use strict';
    app.controller('indexController', ['$scope', '$location', 'authService', 'dataService',function ($scope, $location, authService,dataService) {
        $scope.kategorije;
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/home');
        }
        $scope.authentication = authService.authentication;
        $scope.kategorije = [];
       dataService.getAll('api/kategorija/').then(function (results) {
            $scope.kategorije = results;
            
        }, function (error) {
            //alert(error.data.message);
        });
       
    }]);
    
    })();
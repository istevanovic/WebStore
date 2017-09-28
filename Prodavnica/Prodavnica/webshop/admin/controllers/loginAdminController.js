
    'use strict';
    app.controller('loginAdminController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    
        $scope.loginData = {
            username: "",
            password: ""
        };
    
        $scope.message = "";
        $scope.login = function () {
            authService.login($scope.loginData).then(function (response) {
                $location.path('/home');
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
    }]);
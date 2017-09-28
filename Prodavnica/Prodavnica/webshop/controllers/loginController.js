    'use strict';
    app.controller('loginController', ['$scope', '$routeParams','$location', 'authService', function ($scope, $routeParams,$location, authService) {
    
        $scope.loginData = {
            username: "",
            password: ""
        };
        $scope.whereto = $routeParams.order;
        $scope.authentication = authService.authentication;
        if($scope.authentication.isAuth && whereto=="order"){
            $location.path('/narudzbenica');
        }

       

        $scope.message = "";
        $scope.login = function () {
            authService.login($scope.loginData).then(function (response) {
                if( $scope.whereto=="order") $location.path('/narudzbenica');
                else $location.path('/home');
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };

        

        
    }]);

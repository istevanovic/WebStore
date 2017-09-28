'use strict';
app.controller('narudzbeniceAdminController', ['$scope', '$q', '$location', 'authService', 'dataService',
    function ($scope, $q, $location, authService, dataService) {
       $scope.itemshide=true;
        $scope.narudzbenice = [];
        $scope.authentication = authService.authentication;
        if ($scope.authentication.role != "admin") {
            $location.path('/login');
        }//!authService.authentication.isAuth
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/login');
        }
        
       
            dataService.getAll('api/narudzbenica/').then(function (results) {
                $scope.narudzbenice = results.data;
                console.log($scope.narudzbenice);
            }, function error(response) {
                if (response.status === 404) {
                    $scope.errorMessage = 'User not found!';
                }
                else {
                    $scope.errorMessage = "Error getting user!";
                }
            });
        
    }]);
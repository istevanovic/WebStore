
    'use strict';
    app.controller('adminController', ['$scope', '$routeParams','$location','authService', 'dataService',
                             function ($scope, $routeParams,$location,authService,dataService) {
        $scope.brojproizvoda;
        var error = $routeParams.error;
        if (error == "403") $scope.message = "Zabranjeno vam je da pristupite ovde";
        
        $scope.authentication = authService.authentication;
        if($scope.authentication.role!="admin"){
            $location.path('/login');
        }
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/login');
        }    
        dataService.getAll('api/proizvod/')
        .then(function (results) {
            $scope.brojproizvoda = results.data.length;
        }, function (error) {
            //alert(error.data.message);
        });
    }]);
    
  
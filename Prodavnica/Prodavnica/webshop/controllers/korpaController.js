'use strict';
app.controller('korpaController', ['$scope', '$routeParams','$location','authService', 'dataService','ngCart',
                         function ($scope, $routeParams,$location,authService,dataService,ngCart) {
                            ngCart.setTaxRate(20);
                            ngCart.setShipping(300);
   /* var error = $routeParams.error;
    if (error == "403") $scope.message = "Zabranjeno vam je da pristupite ovde";*/
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }
    $scope.authentication = authService.authentication;

    $scope.korpa_placanje=function(){
        if($scope.authentication.isAuth) $location.path('/narudzbenica');
        else $location.path('/login/order');
    }
    
   
}]);

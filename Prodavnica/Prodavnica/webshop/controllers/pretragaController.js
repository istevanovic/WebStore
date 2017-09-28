'use strict';
app.controller('pretragaController', ['$scope', '$routeParams', '$location', 'authService', 'dataService',
    function ($scope, $routeParams, $location, authService, dataService) {

        $scope.messageresults="";
        var error = $routeParams.error;
        if (error == "403") $scope.message = "Zabranjeno vam je da pristupite ovde";
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/home');
        }
        $scope.authentication = authService.authentication;

        $scope.term = $routeParams.term;
        if ( $scope.term) {
                dataService.getAll('api/pretraga/proizvod/?term=' +  $scope.term)
                    .then(function (results) {
                        $scope.proizvodi = results.data;
                        if( $scope.proizvodi.length<=0) $scope.messageresults="Nema resultata za traženi pojam ";
                    }, function (error) {
                        //alert(error.data.message);
                    });
            
        }
        else {
            
            dataService.getAll('api/proizvod/')
                .then(function (results) {
                    $scope.proizvodi = results.data;
                    // console.log($scope.proizvodi);
                }, function (error) {
                    //alert(error.data.message);
                });
        }

    
        $scope.getPrice = function (price, discount) {
        var total = 0;
        total = price - (price * (discount / 100));
        return total;
    }
       
    }]);


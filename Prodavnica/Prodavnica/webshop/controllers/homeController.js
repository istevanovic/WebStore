'use strict';
    app.controller('homeController', ['$scope', '$routeParams','$location','authService', 'dataService',
                             function ($scope, $routeParams,$location,authService,dataService) {
        
        var error = $routeParams.error;
        if (error == "403") $scope.message = "Zabranjeno vam je da pristupite ovde";
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/home');
        }
        $scope.authentication = authService.authentication;

        

        $scope.kategorije = [];
        dataService.getAll('api/kategorija/')
        .then(function (results) {
            $scope.kategorije = results.data;
        }, function (error) {
            //alert(error.data.message);
        });
        
        $scope.proizvodi = [];
        dataService.getAll('api/proizvod/')
        .then(function (results) {
            $scope.proizvodi = results.data;
           // console.log($scope.proizvodi);
        }, function (error) {
            //alert(error.data.message);
        });
        $scope.pretraga=function(){
            $location.path('/pretraga/'+$scope.term);
        }
        $scope.getPrice = function(price,discount){
            var total = 0;
            total=price-(price*(discount/100));
            return total;
        }

        $scope.Go2=function(url)
        { 
            window.location = url;
        }
       
    }]);
    
  
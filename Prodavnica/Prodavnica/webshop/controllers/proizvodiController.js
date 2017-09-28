'use strict';
app.controller('proizvodiController', ['$scope', '$routeParams', 'dataService', function ($scope, $routeParams, dataService) {
    $scope.proizvodi = [];
    $scope.proiz;
    $scope.karta_item;
    $scope.korpa_proizvodi=[];
    
   
    var id = $routeParams.id;
    var katId=$routeParams.kategorija;
    if (id) {
        dataService.getOne(id,'api/proizvod/')
        .then(function (results) {
            $scope.proiz = results.data;
           // console.log($scope.proiz);
        }, function (error) {
            //alert(error.data.message);
        });
        
    }else if (katId) {
        dataService.getAll('api/kat/proizvod/?katId='+katId)
        .then(function (results) {
            $scope.proizvodi = results.data;
            console.log($scope.proizvodi);
        }, function (error) {
            //alert(error.data.message);
        });
        
    }
    else{
        dataService.getAll('api/proizvod/').then(function (results) {
            $scope.proizvodi = results.data;
           
        }, function (error) {
            //alert(error.data.message);
        });
    }

    $scope.getPrice = function(price,discount){
        var total = 0;
        total=price-(price*(discount/100));
        return total;
    }

}]);
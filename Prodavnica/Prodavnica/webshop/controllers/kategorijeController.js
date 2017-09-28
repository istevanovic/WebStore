'use strict';
app.controller('kategorijeController', ['$scope', 'dataService', function ($scope, dataService) {
    $scope.kategorije = [];
    dataService.getAll('api/kategorija/').then(function (results) {
        $scope.kategorije = results.data;
    }, function (error) {
        //alert(error.data.message);
    });
}]);

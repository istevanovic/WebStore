'use strict';
app.controller('kategorijeAdminController', ['$scope', '$q', '$location', 'authService', 'dataService',
    function ($scope, $q, $location, authService, dataService) {
        $scope.formHide = true;
        $scope.kategorije = [];
        $scope.errorMessage;
        $scope.errorHide = true;
        $scope.formPromeni = false;


        $scope.authentication = authService.authentication;
        if ($scope.authentication.role != "admin") {
            $location.path('/login');
        }
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/login');
        }
        $scope.reset = function () {
            $scope.kategorijaParams = null;
            $scope.formHide = true;
            $scope.formPromeni = false;
            $scope.errorHide = true;
        }

        $scope.dajKategoriju = function (id) {
            dataService.getOne(id, 'api/kategorija/')
                .then(function (results) {
                    $scope.kategorijaParams = results.data;
                    $scope.formPromeni = !$scope.formPromeni;
                    $scope.formHide = !$scope.formHide;
                }, function error(response) {
                    if (response.status === 404) {
                        $scope.errorMessage = 'User not found!';
                    }
                    else {
                        $scope.errorMessage = "Error getting user!";
                    }
                });
        }
        $scope.dodajKategorija = function (data) {
            dataService.insert(data, 'api/kategorija/').then(function (response) {

                if (response) {
                    $scope.formHide = true;
                    $scope.kategorije.push(response.data);
                    $scope.reset();
                    //$location.path("/kategorije");
                }
                else {
                    $scope.errorMessage = "Greska pri dodavanju korisnika";
                    $scope.errorHide = false;
                };
            });

        }
        $scope.promeniKategoriju = function (id, data) {
            dataService.update(id, data, 'api/kategorija/').then(function (response) {
                if (response) {
                    $scope.formHide = true;
                    $scope.kategorije = dajKategorije();
                    $scope.formPromeni = false;
                    $scope.reset();
                }
                else {
                    $scope.errorMessage = "Greska pri promeni korisnika";
                    $scope.errorHide = false;
                };
            });

        }

        $scope.obrisiKategoriju = function (id) {
            dataService.delete(id, 'api/kategorija/').then(function (response) {
                if (response) {
                    if (response.data.error) {alert(response.data.error);return false;}
                    else {
                    $scope.formHide = true;
                        dajKategorije();
                    }
                    // $location.path("/korisnici");
                }
                else {
                    $scope.errorMessage = "Greska pri brisanju korisnika"
                    $scope.errorHide = false;
                };
            });

        }
        var dajKategorije = function () {
            dataService.getAll('api/kategorija/').then(function (results) {
                $scope.kategorije = results.data;
            }, function error(response) {
                if (response.status === 404) {
                    $scope.errorMessage = 'User not found!';
                }
                else {
                    $scope.errorMessage = "Error getting user!";
                }
            });
        }
        dajKategorije();
    }]);
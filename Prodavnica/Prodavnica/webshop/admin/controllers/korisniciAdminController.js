'use strict';
app.controller('korisniciAdminController', ['$scope', '$q', '$location', 'authService', 'dataService',
    function ($scope, $q, $location, authService, dataService) {
        $scope.formHide = true;
        $scope.korisnici = [];
        $scope.errorMessage;
        $scope.errorHide = true;
        $scope.formPromeni = false;

        $scope.authentication = authService.authentication;
        if ($scope.authentication.role != "admin") {
            $location.path('/login');
        }//!authService.authentication.isAuth
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/login');
        }
        $scope.reset = function () {
            $scope.korisnikParams = null;
            $scope.formHide = true;
            $scope.formPromeni = false;
            $scope.errorHide = true;
        }

        $scope.dajKorisnik = function (id) {
            dataService.getOne(id, 'api/korisnik/')
                .then(function (results) {
                    $scope.korisnikParams = results.data;
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
        $scope.dodajKorisnik = function (data) {
            dataService.insert(data, 'api/korisnik/').then(function (response) {

                if (response) {
                    $scope.formHide = true;
                   // $scope.korisnici.push(response);
                   $scope.reset();
                   $scope.korisnici = dajKorisnike();                   
                    $location.path("/korisnici");
                    
                }
                else {
                    $scope.errorMessage = "Greska pri dodavanju korisnika"
                    $scope.errorHide = false;
                };
            });

        }
        $scope.promeniKorisnik = function (id, data) {
            dataService.update(id, data, 'api/korisnik/').then(function (response) {
                if (response) {
                    $scope.formHide = true;
                    $scope.korisnici = dajKorisnike();
                    $scope.reset();
                    $scope.formPromeni = false;
                    //$location.path("/korisnici");
                }
                else {
                    $scope.errorMessage = "Greska pri promeni korisnika"
                    $scope.errorHide = false;
                };
            });

        }

      /*  $scope.obrisiKorisnik = function (id) {
            dataService.delete(id, 'api/korisnik/').then(function (response) {

                if (response) {
                    $scope.formHide = true;
                    $scope.reset();
                    dajKorisnike();
                    // $location.path("/korisnici");
                }
                else {
                    $scope.errorMessage = "Greska pri brisanju korisnika"
                    $scope.errorHide = false;
                };
            });

        }*/
        var dajKorisnike = function () {
            dataService.getAll('api/admin/korisnik/').then(function (results) {
                $scope.korisnici = results.data;
            }, function error(response) {
                if (response.status === 404) {
                    $scope.errorMessage = 'User not found!';
                }
                else {
                    $scope.errorMessage = "Error getting user!";
                }
            });
        }
        dajKorisnike();
    }]);
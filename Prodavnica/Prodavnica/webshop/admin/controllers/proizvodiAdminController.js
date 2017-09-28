'use strict';
app.controller('proizvodiAdminController', ['$scope', '$q', '$location','authService', 'dataService','ngAppSettings','Upload',
    function ($scope, $q, $location, authService, dataService,ngAppSettings,Upload) {

        var serviceBase = ngAppSettings.apiServiceBaseUri;
        $scope.formHide = true;
        $scope.proizvodi = [];
        $scope.kategorije = [];
        $scope.errorMessage;
        $scope.errorHide = true;
        $scope.formPromeni = false;
        $scope.status = [
            {'id':1,statusn:"Aktivan"},
            {'id':2,statusn:"Neaktivan"}
        ];

       
        $scope.authentication = authService.authentication;
        if($scope.authentication.role!="admin"){
            $location.path('/login');
        }
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/login');
        } 
        $scope.reset = function () {
            $scope.proizvodParams = null;
            $scope.formHide = true;
            $scope.formPromeni = false;
            $scope.errorHide = true;
            $scope.picFile=null;
        }

        



        $scope.dodajProizvod = function (data, params) {
            
            dataService.insert(params, 'api/proizvod/').then(function (response) {

                if (response) {
                    $scope.formHide = true;
                    console.log(response);
                    $scope.uploadFiles(data,response.data.id);

                   // $scope.proizvodi = dajProizvode();
                }
                else {
                    $scope.errorMessage = "Greska pri dodavanju korisnika"
                    $scope.errorHide = false;
                };
            });

        }

        $scope.uploadFiles = function (files,id) {
            $scope.files = files;
            if (files ) { //&& files.length
                Upload.upload({
                    url: serviceBase+'api/upload/?id='+id,
                    data: {
                        files: files
                    }
                }).then(function (response) {
                    $scope.proizvodi = dajProizvode();
                }, function (response) {
                    if (response.status > 0) {
                        $scope.errorMsg = response.status + ': ' + response.data;
                    }
                });
            }
            else{
            dajProizvode();
            }
        };

        $scope.promeniProizvod = function (id, data,params) {
            dataService.update(id, params, 'api/proizvod/').then(function (response) {
                if (response) {
                    $scope.formHide = true;
                    $scope.uploadFiles(data,id);
                    $scope.formPromeni = false;
                    
                }
                else {
                    $scope.errorMessage = "Greska pri promeni korisnika"
                    $scope.errorHide = false;
                };
            });

        }
        $scope.obrisiProizvod = function (id) {
            dataService.delete(id, 'api/proizvod/').then(function (response) {

                if (response) {
                   if(response.data.error){alert(response.data.error); return false;}
                   else{
                    $scope.formHide = true;
                    dajProizvode();
                   }
                    // $location.path("/korisnici");
                }
                else {
                    $scope.errorMessage = "Greska pri brisanju korisnika"
                    $scope.errorHide = false;
                };
            });
        }
        $scope.obrisiSliku = function (id, idProizvod) {
            dataService.delete(id, 'api/slike/').then(function (response) {

                if (response) {
                   // $scope.formHide = true;
                   dataService.getOne(idProizvod, 'api/proizvod/')
                   .then(function (results) {
                       $scope.proizvodParams = results.data;
                   });
                    // $location.path("/korisnici");
                }
                else {
                    $scope.errorMessage = "Greska pri brisanju korisnika"
                    $scope.errorHide = false;
                };
            });
        }

        $scope.dajProizvod = function (id) {
            dataService.getOne(id, 'api/proizvod/')
                .then(function (results) {
                    $scope.proizvodParams = results.data;
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
       /* 
       
       

        */
        var dajKategorije= function () {
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

        var dajProizvode= function () {
            dataService.getAll('api/proizvod/').then(function (results) {
                $scope.proizvodi = results.data;
                $scope.reset();
            }, function error(response) {
                if (response.status === 404) {
                    $scope.errorMessage = 'User not found!';
                }
                else {
                    $scope.errorMessage = "Error getting user!";
                }
            });
        }
        dajProizvode();
        dajKategorije();
    }]);
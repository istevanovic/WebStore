'use strict';
app.controller('narudzbenicaController', ['$scope', '$routeParams', '$location', 'authService', 'dataService', 'ngCart',
    function ($scope, $routeParams, $location, authService, dataService, ngCart) {
        $scope.authentication = authService.authentication;
        if(!$scope.authentication.isAuth){
            $location.path('/login/order');
        }


        $scope.message = "";
        $scope.narudzbenica;
        $scope.korpa=ngCart.toObject();
        $scope.formhide=false;
        var finnish = $routeParams.finnish;
        ngCart.setTaxRate(20);
        ngCart.setShipping(300);
        $scope.date = new Date();
        $scope.podaciKorisnik={};
        //console.log(ngCart.toObject());
       if(localStorage.getItem("narudzbenica")!=null) {
           $scope.formhide=true;
           $scope.narudzbenica=JSON.parse(localStorage.getItem("narudzbenica"));   
       }
       else if(localStorage.getItem("narPromena")!=null){
        $scope.formhide=false;
        $scope.order=JSON.parse(localStorage.getItem("narPromena"));
        localStorage.removeItem("narPromena");
       }


      /*  if (finnish &&) {
            dataService.getOne(id, 'api/narudzbenica/').then(function (results) {
                $scope.narudzbenica = results.data;
                 console.log($scope.narudzbenica);
            }, function error(response) {
                if (response.status === 404) {
                    $scope.errorMessage = 'User not found!';
                }
                else {
                    $scope.errorMessage = "Error getting user!";
                }
            });
        }*/
        $scope.unesiPodatkeKorisnik=function(order){
            if ($scope.formAdresa.$valid) {
            $scope.formhide=true;
            $scope.narudzbenica=order;
            $scope.korpa = ngCart.toObject();
            localStorage.setItem("narudzbenica", JSON.stringify($scope.narudzbenica));
            $location.path('/narudzbenica');
            }
            else{
                $scope.message="Morate popuniti sva polja!";
            }
           
        }
        $scope.promena= function(params){
            localStorage.removeItem("narudzbenica");
            localStorage.setItem("narPromena", JSON.stringify(params));
            $location.path('/narudzbenica/promena');
        }
        $scope.potvrdi = function (params) {
            params.idKorisnik = $scope.authentication.id;
            params.postarina = $scope.korpa.shipping;
            dataService.insert(params, 'api/narudzbenica/').then(function (response) {

                if (response) {
                    console.log(response);
                    angular.forEach($scope.korpa.items, function (value, key) {
                        insertNarItems({ kolicina:$scope.korpa.items[key].quantity, 
                            idProizvod:$scope.korpa.items[key].id, idNarudzbenica:response.data.id });
                    });

                    ngCart.empty();
                    localStorage.removeItem("narudzbenica");
                    $location.path('/narudzbenica/krajkupovine/finnish');
                    // $scope.proizvodi = dajProizvode();
                }
                else {
                    $scope.errorMessage = "Greska pri dodavanju korisnika"
                    $scope.errorHide = false;
                };
            });
        }

        var insertNarItems = function (params) {
            console.log(params);
            dataService.insert(params, 'api/narudzbenicaProizvod/').then(function (results) {
               }, function error(response) {
                if (response.status === 404) {
                    $scope.errorMessage = 'User not found!';
                }
                else {
                    $scope.errorMessage = "Error getting user!";
                }
            });
        }
    }]);
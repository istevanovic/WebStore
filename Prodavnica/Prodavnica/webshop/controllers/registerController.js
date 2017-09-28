(function () {
    'use strict';
    app.controller('registerController', ['$scope', '$routeParams','$location', '$timeout', 'authService', function ($scope,$routeParams, $location, $timeout, authService) {
    
        $scope.savedSuccessfully = false;
        $scope.message = "";
        var whereto = $routeParams.order;
    
        $scope.registration = {
            username: "",
            password: "",
            role: ""
        };
    
        $scope.register = function () {
            if ($scope.registerForm.$valid) {
            authService.saveRegistration($scope.registration).then(function (response) {
                $scope.savedSuccessfully = true;
                $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                startTimer();
    
            },
             function (response) {
                 $scope.message = "Failed to register user due to:" + response;
             });
            }
            else{
                $scope.message="Morate popuniti sva obavezna polja";
            }
        };
    
        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                if(whereto=="order") {
                    authService.login($scope.registration).then(function (response) {
                       $location.path('/narudzbenica');
                    },
                     function (err) {
                         $scope.message = err.error_description;
                     });
                    }
                    else 
                $location.path('/login');
            }, 2000);
        }
    
    }]);
    
   
    
    })();
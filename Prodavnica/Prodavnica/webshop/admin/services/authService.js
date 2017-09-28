    'use strict';
    app.factory('authService', ['$q', '$injector', 'localStorageService', 'ngAppSettings', function ($q, $injector, localStorageService, ngAppSettings) {
    
        var serviceBase = ngAppSettings.apiServiceBaseUri;
        var $http;
        var authServiceFactory = {};
    
        var _authData = {
            isAuth: false,
            username: "",
            role:"" ,
            refresh_token:"",
            token:"",
            id:""

        };

        // za sada ne koristimo - ali bi mogli za registraciju korisnika
        var _saveRegistration = function (registration) {
            _logOut();
            $http = $http || $injector.get('$http');
            return $http.post(serviceBase + 'api/korisnik/register', registration).then(function (response) {
                return response;
            });
    
        };
    
        var _login = function (loginData) {
            var data = "grant_type=password&username=" + loginData.username + "&password=" + loginData.password; 
            var deferred = $q.defer();
    
            $http = $http || $injector.get('$http');
            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(function (response) {
                            localStorageService.set('authorizationData', { token: response.data.access_token, 
                                                                           username: loginData.username, 
                                                                           refreshToken: response.data.refresh_token,
                                                                           role: response.data.role,
                                                                           id:response.data.id
                                                                        }) 
                            
                            var authData = localStorageService.get('authorizationData');
                            //console.log(authData);
                            _fillAuthData();

                            deferred.resolve(response);
                        }, 
                  function (err, status) {
                            _logOut();
                            deferred.reject(err);
                        });
               
            return deferred.promise;
    
        };
    
        var _logOut = function () {
            localStorageService.remove('authorizationData');
    
            _authData.isAuth = false;
            _authData.username = "";
            _authData.role = "";
            _authData.id=""
    
        };
    
        var _fillAuthData = function () {
    
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authData.isAuth = true;
                _authData.username = authData.username; 
                _authData.role = authData.role; 
                _authData.id=authData.id;
            }
    
        }; 
    
        var _refreshToken = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
            var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken;// + "&client_id=" + ngAuthSettings.clientId;
            var deferred = $q.defer();
            localStorageService.remove('authorizationData');
            
            $http = $http || $injector.get('$http');
            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(function (response) {
                            localStorageService.set('authorizationData', { token: response.data.access_token, 
                                                                           username:  response.data.username, 
                                                                           refreshToken: response.data.refresh_token,
                                                                           role: response.data.role,
                                                                           id:response.data.id
                                                                        }) 
                            
                            var authData = localStorageService.get('authorizationData');
                            //console.log(authData);
                            _fillAuthData();

                            deferred.resolve(response);
                        }, 
                  function (err, status) {
                            _logOut();
                            deferred.reject(err);
                        });
                    }
            return deferred.promise;
        };
       
         //promenljive
         authServiceFactory.authentication = _authData; 
        //funkcije
        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.refreshToken = _refreshToken;
       

        return authServiceFactory;
    }]);
    
    




'use strict';
app.factory('dataService', ['$http', 'ngAppSettings', function ($http, ngAppSettings) {
    var serviceBase = ngAppSettings.apiServiceBaseUri;
    var url='';
    var dataServiceFactory = {};
    var _getAll = function (url) {
        return $http.get(serviceBase + url).then(function (results) {
           // console.log("getall "+serviceBase + url);
            return results;
        });
    };

    var _getOne = function (id,url) {
        return $http.get(serviceBase + url + id).then(function (results) {
            //console.log("getone "+serviceBase + url+id);
            return results;
        });

    }
    var _getSearch = function (term,url) {
            return $http.get(serviceBase + url + decodeURIComponent(term)).then(function (results) {
                // console.log("getall "+serviceBase + url);
                 return results;
             });
        
    };

    var _insert = function (servicedata,url) {
        return $http.post(serviceBase + url, JSON.stringify(servicedata)).then(function (results) {
           // console.log("insert "+serviceBase + url);
            // if(results.data.idKorisnik!="")
            return results;
            // else return false;
        });
    }

    var _update = function (id, servicedata,url) {
        //console.log("update "+serviceBase + url); 
        return $http.put(serviceBase + url + id, JSON.stringify(servicedata)).then(function (results) {
            return results;
        });
    }
    var _delete = function (id,url) {
       // console.log("delete "+serviceBase + url+id);
        return $http.delete(serviceBase + url + id).then(function (results) {
            return results;
        });
    }

    dataServiceFactory.getAll = _getAll;
    dataServiceFactory.getSearch = _getSearch;
    dataServiceFactory.getOne = _getOne;
    dataServiceFactory.insert = _insert;
    dataServiceFactory.update = _update;
    dataServiceFactory.delete = _delete;

    return dataServiceFactory;
}]);
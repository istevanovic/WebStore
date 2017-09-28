    
  var app = angular.module('WebApiAdmin',  ['ngRoute', 'LocalStorageModule','confirm','ngFileUpload']);
  
  app.config(function ($routeProvider) {
      // Definišemo rute
      $routeProvider.when("/home", {
          controller: "adminController",
          templateUrl: "views/homeadmin.html"
      });
      $routeProvider.when("/login", {
          controller: "loginAdminController",
          templateUrl: "views/loginadmin.html"
      });
      $routeProvider.when("/korisnici", {
        controller: "korisniciAdminController",
        templateUrl: "views/korisniciadmin.html"
    });
    $routeProvider.when("/kategorije", {
        controller: "kategorijeAdminController",
        templateUrl: "views/kategorijeadmin.html"
    });
    $routeProvider.when("/narudzbenice", {
        controller: "narudzbeniceAdminController",
        templateUrl: "views/narudzbeniceadmin.html"
    });
    $routeProvider.when("/proizvodi", {
        controller: "proizvodiAdminController",
        templateUrl: "views/proizvodiadmin.html"
    });
  
  /*  $routeProvider.when("/admin", {
        controller: "loginAdminController",
        templateUrl: "/views/admin/loginadmin.html"
    });
    $routeProvider.when("/admin/home", {
        controller: "adminController",
        templateUrl: "/views/admin/homeadmin.html"
    });*/

    $routeProvider.when("/no-access", {
       // controller: "noAcceController",
        templateUrl: "views/no-access.html"
    });
  
      $routeProvider.otherwise({ redirectTo: "/home" });
  
  });
  
  //ovde treba da podesimo naš domen
  app.constant('ngAppSettings', {
      webServiceBaseUri: 'http://localhost:3827/webshop/admin/' ,
      apiServiceBaseUri: 'http://localhost:3827/' 
  });
   // aplikacija koristi servis koji nam služi za rad sa tokenima
   app.config(function ($httpProvider) {
      $httpProvider.interceptors.push('authInterceptorService');
  });
   // pri pokretanju aplikacije pozivamo funkciju koja popunjava potrebne 
   // podatke o tokenu ako ih im u sesiji – korisnik je logovan
   app.run(['authService', function (authService) {
      authService.fillAuthData();
  }]); 
  
 
    
  angular.module('confirm', [])
  .directive('confirm', [function () {
      return {
          priority: 100,
          restrict: 'A',
          link: {
              pre: function (scope, element, attrs) {
                  var msg = attrs.confirm || "Da li ste sigurni?";

                  element.bind('click', function (event) {
                      if (!confirm(msg)) {
                          event.stopImmediatePropagation();
                          event.preventDefault;
                      }
                  });
              }
          }
      };
  }]);
 /* 
 samo jedan fajl - treba nam vise*/
 angular.module('fileModel',[]).directive('fileModel', [function(){
    return {
        restrict: 'EA',
        scope:{
            selectedfile: "="
        },
        link: function (scope, element, attrs) { 

            element.bind("change", function (changeEvent) {
                scope.$apply(function () {
                    scope.selectedfile = changeEvent.target.files[0];
                });
            });

        },
        template: '<input type="file" multiple  />'
    };

   
  }]);
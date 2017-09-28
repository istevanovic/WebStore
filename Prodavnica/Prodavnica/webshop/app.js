    
  var appweb = angular.module('WebApiAuth', ['WebApiAdmin','ngRoute', 'LocalStorageModule','ngCart']);
  
  appweb.config(function ($routeProvider) {
      // Definišemo rute
      $routeProvider.when("/home", {
          controller: "homeController",
          templateUrl: "views/home.html"
      });
      $routeProvider.when("/home/:error", {
          controller: "homeController",
          templateUrl: "views/home.html"
      });
      $routeProvider.when("/login/:order", {
          controller: "loginController",
          templateUrl: "views/login.html"
      });
      $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "views/login.html"
    });
  
    $routeProvider.when("/register/:order", {
        controller: "registerController",
        templateUrl: "views/register.html"
    });
      $routeProvider.when("/register", {
          controller: "registerController",
          templateUrl: "views/register.html"
      });
  
      $routeProvider.when("/kategorije", {
          controller: "kategorijeController",
          templateUrl: "views/kategorije.html"
      });
      $routeProvider.when("/proizvodi", {
        controller: "proizvodiController",
        templateUrl: "views/proizvodi.html"
    }); 
    $routeProvider.when("/proizvodi/:kategorija/:id", {
        controller: "proizvodiController",
        templateUrl: "views/proizvod.html"
    }); 
    $routeProvider.when("/proizvodi/:kategorija", {
        controller: "proizvodiController",
        templateUrl: "views/proizvodi.html"
    }); 
    $routeProvider.when("/pretraga/:term", {
        controller: "pretragaController",
        templateUrl: "views/pretraga.html"
    }); 
    $routeProvider.when("/korpa", {
        controller: "korpaController",
        templateUrl: "views/korpa.html"
    }); 
    $routeProvider.when("/narudzbenica", {
        controller: "narudzbenicaController",
        templateUrl: "views/narudzbenica.html"
    }); 
    $routeProvider.when("/narudzbenica/krajkupovine/:finnish", {
        controller: "narudzbenicaController",
        templateUrl: "views/prikazNarudzbenice.html"
    }); 
    $routeProvider.when("/fb", {
         redirectTo: "www.facebook.com"  
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
    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "views/home.html"
    });
      $routeProvider.otherwise({ redirectTo: "/" });
  
  });
  
  //ovde treba da podesimo naš domen
  appweb.constant('ngAppSettings', {
     // webServiceBaseUri: 'http://localhost:3827/webshop/' ,
      apiServiceBaseUri: 'http://localhost/MyApp/'
  });
   // aplikacija koristi servis koji nam služi za rad sa tokenima
   appweb.config(function ($httpProvider) {
      $httpProvider.interceptors.push('authInterceptorService');
  });
   // pri pokretanju aplikacije pozivamo funkciju koja popunjava potrebne 
   // podatke o tokenu ako ih im u sesiji – korisnik je logovan
   appweb.run(['authService', function (authService) {
      authService.fillAuthData();
  }]); 
  
 
    
    
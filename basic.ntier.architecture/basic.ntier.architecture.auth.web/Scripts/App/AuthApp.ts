declare var app: ng.IModule;

var app = angular.module('AuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "HomeController",
        templateUrl: "/home.html"
    });

    $routeProvider.when("/login", {
        controller: "LoginController",
        templateUrl: "/login.html"
    });

    $routeProvider.otherwise({ redirectTo: "/login" });
});

app.run(['LoginService', function (LoginService: LoginModule.ILoginService) {
    LoginService.FillAuthData();
}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});


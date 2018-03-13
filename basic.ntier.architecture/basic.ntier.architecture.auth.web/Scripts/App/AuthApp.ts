﻿declare var app: ng.IModule;

var app = angular.module('AuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'toastr']);

app.config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = false;
}]);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "HomeController",
        templateUrl: "/home.html"
    });

    $routeProvider.when("/login", {
        controller: "LoginController",
        templateUrl: "/login.html"
    });

    $routeProvider.when("/roles", {
        controller: "RolesController",
        templateUrl: "/roles.html"
    });

    $routeProvider.when("/users", {
        controller: "UsersController",
        templateUrl: "/users.html"
    });

    $routeProvider.otherwise({ redirectTo: "/login" });
});

app.run(['AuthService', function (AuthService: AuthModule.IAuthService) {
    AuthService.FillAuthData();
}]);
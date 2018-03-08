module HomeModule {
    'use strict';

    export interface IHomeScope extends ng.IScope {
        vm: IAuthModel;
    }

    // Expose outside of the controller
    //export var HomeScope: IHomeScope;

    /*** ANGULAR CONTROLLER ***/
    export class HomeController {

        static $inject = ["$scope", "LoginService", "$location"];

        constructor($scope: HomeModule.IHomeScope, LoginService: LoginModule.ILoginService, $location: ng.ILocationService) {

            //HomeScope = $scope;

            $scope.vm = LoginService.Auth;
        }
    }
}

// Attach the controller to the app
app.controller("HomeController", HomeModule.HomeController);
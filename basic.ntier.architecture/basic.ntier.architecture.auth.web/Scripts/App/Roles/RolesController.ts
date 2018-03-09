module RolesModule {
    'use strict';

    export interface IRolesScope extends ng.IScope {
        vm: IAuthModel;
    }

    // Expose outside of the controller
    //export var RolesScope: IRolesScope;

    /*** ANGULAR CONTROLLER ***/
    export class RolesController {

        static $inject = ["$scope", "AuthService", "$location"];

        constructor($scope: RolesModule.IRolesScope, AuthService: AuthModule.IAuthService, $location: ng.ILocationService) {

            //RolesScope = $scope;

            $scope.vm = AuthService.Auth;
        }
    }
}

// Attach the controller to the app
app.controller("RolesController", RolesModule.RolesController);
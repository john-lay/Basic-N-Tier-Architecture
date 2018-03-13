module UsersModule {
    'use strict';

    export interface IUsersScope extends ng.IScope {
     
    }

    // Expose outside of the controller
    //export var UsersScope: IUsersScope;

    /*** ANGULAR CONTROLLER ***/
    export class UsersController {

        static $inject = ["$scope", "AuthService", "$location"];

        constructor($scope: UsersModule.IUsersScope, AuthService: AuthModule.IAuthService, $location: ng.ILocationService) {

            //UsersScope = $scope;
        }
    }
}

// Attach the controller to the app
app.controller("UsersController", UsersModule.UsersController);
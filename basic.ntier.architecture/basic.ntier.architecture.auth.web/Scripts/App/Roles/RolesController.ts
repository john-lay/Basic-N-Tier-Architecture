module RolesModule {
    'use strict';

    export interface IRolesScope extends ng.IScope {
        vm: IRolesViewModel;
    }

    // Expose outside of the controller
    //export var RolesScope: IRolesScope;

    /*** ANGULAR CONTROLLER ***/
    export class RolesController {

        static $inject = ["$scope", "AuthService", "$location", "RolesService"];

        constructor($scope: RolesModule.IRolesScope, AuthService: AuthModule.IAuthService, $location: ng.ILocationService, RolesService: RolesModule.IRolesService) {

            //RolesScope = $scope;

            $scope.vm = { Roles: [] };

            RolesService.GetRoles().then((response: IRoleModel[]) => {
                $scope.vm.Roles = response;
            });
        }
    }
}

// Attach the controller to the app
app.controller("RolesController", RolesModule.RolesController);
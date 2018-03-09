module NavModule {
    'use strict';

    export interface INavScope extends ng.IScope {
        vm: INavModel;

        logout(): void;
    }

    // Expose outside of the controller
    export var NavScope: INavScope;

    /*** ANGULAR CONTROLLER ***/
    export class NavController {

        static $inject = ["$scope", "AuthService", "$location"];

        constructor($scope: NavModule.INavScope, AuthService: AuthModule.IAuthService, $location: ng.ILocationService) {

            NavScope = $scope;

            $scope.vm = {
                Auth: AuthService.Auth,
                page: $location.path()
            };

            $scope.logout = () => {
                $scope.vm.Auth.isAuth = false; // hide the nav bar on logout
                AuthService.Logout();
                $location.path('/login');
            }
        }
    }
}

// Attach the controller to the app
app.controller("NavController", NavModule.NavController);
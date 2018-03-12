module NavModule {
    'use strict';

    export interface INavScope extends ng.IScope {
        vm: INavViewModel;

        logout(): void;
    }

    // Expose outside of the controller
    export var NavScope: INavScope;

    /*** ANGULAR CONTROLLER ***/
    export class NavController {

        static $inject = ["$scope", "AuthService", "$location", "toastr"];

        constructor($scope: NavModule.INavScope, AuthService: AuthModule.IAuthService, $location: ng.ILocationService, toastr: angular.toastr.IToastrService) {

            NavScope = $scope;

            $scope.vm = {
                Auth: AuthService.Auth,
                page: $location.path()
            };

            $scope.logout = () => {
                $scope.vm.Auth.isAuth = false; // hide the nav bar on logout
                AuthService.Logout();
                $location.path('/login');
                toastr.success('You have successfully logged out');
            }

            // update the active css class when routing
            $scope.$on('$locationChangeSuccess', (event) => {    
                $scope.vm.page = $location.path();
            });

            // log the user out after 30 minutes (the lifetime of a token)
            if ($scope.vm.Auth.isAuth) {
                setTimeout(() => {
                    $scope.logout();
                }, 1800000);
            }            
        }
    }
}

// Attach the controller to the app
app.controller("NavController", NavModule.NavController);
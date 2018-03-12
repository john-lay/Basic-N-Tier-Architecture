module LoginModule {
    'use strict';

    export interface ILoginScope extends ng.IScope {
        vm: ILoginViewModel;
        Auth: IAuthModel;
        submit(): void;
    }

    // Expose outside of the controller
    export var LoginScope: ILoginScope;

    /*** ANGULAR CONTROLLER ***/
    export class LoginController {

        static $inject = ["$scope", "AuthService", "$location"];

        constructor($scope: LoginModule.ILoginScope, AuthService: AuthModule.IAuthService, $location: ng.ILocationService) {

            LoginScope = $scope;

            $scope.vm = {
                username: 'test',
                password: 'secret',
                rememberMe: false
            };

            $scope.Auth = AuthService.Auth;
            
            $scope.submit = () => {
                AuthService.Login($scope.vm).then((response: ITokenModel) => {
                    $location.path('/home');
                    location.reload(); // show the nav bar on login
                });
            }
        }
    }
}

// Attach the controller to the app
app.controller("LoginController", LoginModule.LoginController);
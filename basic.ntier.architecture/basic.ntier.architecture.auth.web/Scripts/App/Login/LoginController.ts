module LoginModule {
    'use strict';

    export interface ILoginScope extends ng.IScope {
        vm: ILoginModel;
        submit(): void;
    }

    // Expose outside of the controller
    //export var LoginScope: ILoginScope;

    /*** ANGULAR CONTROLLER ***/
    export class LoginController {

        static $inject = ["$scope", "LoginService", "$location"];

        constructor($scope: LoginModule.ILoginScope, LoginService: LoginModule.ILoginService, $location: ng.ILocationService) {

            //LoginScope = $scope;

            $scope.vm = {
                username: 'test',
                password: 'secret',
                rememberMe: false
            };
            
            $scope.submit = () => {
                LoginService.Login($scope.vm).then((response: ITokenModel) => {
                    $location.path('/home');
                });                
            }
        }
    }
}

// Attach the controller to the app
app.controller("LoginController", LoginModule.LoginController);
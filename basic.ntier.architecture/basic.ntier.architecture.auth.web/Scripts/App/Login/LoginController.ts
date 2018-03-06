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

        static $inject = ["$scope", "LoginService"];

        constructor($scope: LoginModule.ILoginScope, LoginService: LoginModule.ILoginService) {

            //LoginScope = $scope;

            $scope.vm = {
                username: 'test',
                password: 'secret',
                rememberMe: false
            };
            
            $scope.submit = () => {
                console.log("Login form submitted", $scope.vm);
                LoginService.Login($scope.vm).then((response: ITokenModel) => {
                    console.log(response);
                });
                
            }
        }
    }
}

// Attach the controller to the app
app.controller("LoginController", LoginModule.LoginController);
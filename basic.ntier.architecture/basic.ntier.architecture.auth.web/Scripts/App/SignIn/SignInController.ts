module SignInModule {
    'use strict';

    export interface ISignInScope extends ng.IScope {

        submit(): void;
    }

    // Expose outside of the controller
    export var signInScope: ISignInScope;

    /*** ANGULAR CONTROLLER ***/
    export class SignInController {

        static $inject = ["$scope"];

        constructor($scope: SignInModule.ISignInScope) {

            //signInScope = $scope;
            
            $scope.submit = () => {
                console.log("SignIn form submitted");
            }
        }
    }
}

// Attach the controller to the app
app.controller("SignInController", SignInModule.SignInController);
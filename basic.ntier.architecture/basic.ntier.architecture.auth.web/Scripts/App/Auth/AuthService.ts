module AuthModule {
    'use strict';

    export interface IAuthService {
        Auth: IAuthModel;

        Login(model: ILoginModel): ng.IPromise<ITokenModel>;
        Logout(): void;
        FillAuthData(): void;
    }

    export class AuthService extends BaseModule.BaseService implements IAuthService {
        static $inject = ["$http", "$q", "$rootScope", "localStorageService"];

        localStorageService: angular.local.storage.ILocalStorageService;        

        constructor($http: ng.IHttpService, $q: ng.IQService, $rootScope: ng.IRootScopeService, localStorageService: angular.local.storage.ILocalStorageService) {
            super($http, $q, $rootScope);
            this.localStorageService = localStorageService;
        }

        // PROPERTIES
        Auth = {
            isAuth: false,
            username: ""
        }

        // METHODS
        Login(model: ILoginModel): ng.IPromise<ITokenModel> {

            var data = 'client_id=099153c2625149bc8ecb3e85e03f0022';
            data += '&grant_type=password';
            data += '&username=' + model.username;
            data += '&password=' + model.password;

            return this.$http.post(this.$baseUrl + "/oauth2/token", data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(
                (response: ng.IHttpPromiseCallbackArg<ITokenModel>) => {
                    this.localStorageService.set('authorizationData', { token: response.data.access_token, username: model.username });

                    this.Auth = {
                        isAuth: true,
                        username: model.username
                    };
                    
                    return this.$q.resolve(response.data);
                }, (response: ng.IHttpPromiseCallbackArg<any>) => {
                    return this.$q.reject(response.statusText);
                });
        }

        Logout() {
            this.localStorageService.clearAll();

            this.Auth = {
                isAuth: false,
                username: ""
            }
        }

        FillAuthData() {

            var authData: IAuthorizationData = this.localStorageService.get('authorizationData') as IAuthorizationData;

            if (authData) {
                this.Auth.isAuth = true;
                this.Auth.username = authData.username;
            }
        }
    }
}

app.service("AuthService", AuthModule.AuthService);
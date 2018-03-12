module RolesModule {
    'use strict';

    export interface IRolesService {
        GetRoles(): ng.IPromise<IRoleModel[]>;
    }

    export class RolesService extends BaseModule.BaseService implements IRolesService {
        static $inject = ["$http", "$q", "$rootScope", "AuthService"];

        AuthService: AuthModule.IAuthService;
        
        constructor($http: ng.IHttpService, $q: ng.IQService, $rootScope: ng.IRootScopeService, AuthService: AuthModule.IAuthService) {
            super($http, $q, $rootScope);
            this.AuthService = AuthService;
        }
        
        // METHODS
        GetRoles(): ng.IPromise<IRoleModel[]> {
            return this.$http.get(this.$baseUrl + "/api/account/claim", { headers: { 'Authorization': 'Bearer ' + this.AuthService.Auth.token } })
                .then(
                (response: ng.IHttpPromiseCallbackArg<IRoleModel[]>) => {
                    return this.$q.resolve(response.data);
                }, (response: ng.IHttpPromiseCallbackArg<any>) => {
                    return this.$q.reject(response.statusText);
                });
        }
    }
}

app.service("RolesService", RolesModule.RolesService);
module BaseModule {
    "use strict";

    export interface IBaseService {
    }

    export class BaseService implements IBaseService {

        static $inject = ["$http", "$q", "$rootScope"];

        $http: ng.IHttpService;
        $q: ng.IQService;
        $rootScope: ng.IRootScopeService;
        $baseUrl: string;

        constructor($http: ng.IHttpService, $q: ng.IQService, $rootScope: ng.IRootScopeService) {
            this.$http = $http;
            this.$q = $q;
            this.$rootScope = $rootScope;
            this.$baseUrl = "http://localhost.ntier.auth"
        }

    }
}
app.service("BaseService", BaseModule.BaseService);
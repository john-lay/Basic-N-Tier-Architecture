module LoginModule {
    export interface ILoginService {
        Auth: IAuthModel;

        Login(model: ILoginModel): ng.IPromise<ITokenModel>;
        FillAuthData(): void;
    }

    export class LoginService extends BaseModule.BaseService implements ILoginService {
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
                    this.localStorageService.set('authorizationData', { token: response.data.access_token, userName: model.username });

                    this.Auth.isAuth = true;
                    this.Auth.username = model.username;
                    
                    return this.$q.resolve(response.data);
                }, (response: ng.IHttpPromiseCallbackArg<any>) => {
                    return this.$q.reject(response.statusText);
                });
            //return this.$http({
            //    method: 'POST',
            //    url: this.$baseUrl + "/oauth2/token",
            //    headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Accept': 'application/json' },
            //    transformRequest: function (obj) {
            //        var str = [];
            //        for (var p in obj)
            //            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            //        return str.join("&");
            //    },
            //    data: {
            //        username: model.username,
            //        password: model.password,
            //        grant_type: 'password',
            //        client_id: '099153c2625149bc8ecb3e85e03f0022'
            //    }
            //}).then(
            //    (response: ng.IHttpPromiseCallbackArg<ITokenModel>) => {
            //        return this.$q.resolve(response.data);
            //    }, (response: ng.IHttpPromiseCallbackArg<any>) => {
            //        return this.$q.reject(response.statusText);
            //    });
        }

        FillAuthData() {

            var authData: IAuthModel = this.localStorageService.get('authorizationData') as IAuthModel;

            if (authData) {
                this.Auth.isAuth = true;
                this.Auth.username = authData.username;
            }
        }
    }
}

app.service("LoginService", LoginModule.LoginService);
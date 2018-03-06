module LoginModule {
    export interface ILoginService {
        Login(model: ILoginModel): ng.IPromise<ITokenModel>;
    }

    export class LoginService extends BaseModule.BaseService implements ILoginService {
        static $inject = ["$http", "$q", "$rootScope"];

        constructor($http: ng.IHttpService, $q: ng.IQService, $rootScope: ng.IRootScopeService) {
            super($http, $q, $rootScope);
        }

        Login(model: ILoginModel): ng.IPromise<ITokenModel> {
            
            return this.$http({
                method: 'POST',
                url: this.$baseUrl + "/oauth2/token",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Accept': 'application/json' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: {
                    username: model.username,
                    password: model.password,
                    grant_type: 'password',
                    client_id: '099153c2625149bc8ecb3e85e03f0022'
                }
            }).then(
                (response: ng.IHttpPromiseCallbackArg<ITokenModel>) => {
                    return this.$q.resolve(response.data);
                }, (response: ng.IHttpPromiseCallbackArg<any>) => {
                    return this.$q.reject(response.statusText);
                });
        }
    }
}

app.service("LoginService", LoginModule.LoginService);
using Microsoft.Owin;

[assembly: OwinStartup(typeof(basic.ntier.architecture.auth.Startup))]
namespace basic.ntier.architecture.auth
{
    using System.Web.Http;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(basic.ntier.architecture.web.Startup))]
namespace basic.ntier.architecture.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

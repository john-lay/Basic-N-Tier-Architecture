namespace basic.ntier.architecture.auth.Infrastructure
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using basic.ntier.architecture.auth.Models;
    using basic.ntier.architecture.auth.Stores;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.OAuth;

    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly UserManager<IdentityUser, string> userManager;

        public SimpleAuthorizationServerProvider()
        {
            var userStore = new UserStore<IdentityUser>();
            userManager = new UserManager<IdentityUser, string>(userStore);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            IdentityUser user = userManager.Find(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}
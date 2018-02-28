namespace basic.ntier.architecture.auth.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Models;
    using Stores;

    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly UserManager<IdentityUser, string> userManager;

        public AccountController()
        {
            var userStore = new UserStore<IdentityUser>();
            userManager = new UserManager<IdentityUser, string>(userStore);
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Claim")]
        public async Task<IHttpActionResult> AddClaim()
        {
            var claim = new Claim(ClaimTypes.Role, "Admin");
            //var result = await userManager.AddClaimAsync("7c999972-8d82-4aad-a780-c19989f548e2", claim);

            return Ok();
        }


    }
}

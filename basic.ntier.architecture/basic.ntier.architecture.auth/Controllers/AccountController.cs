namespace basic.ntier.architecture.auth.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Models;
    using Stores;

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
    }
}

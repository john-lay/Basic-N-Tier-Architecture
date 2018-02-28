namespace basic.ntier.architecture.auth.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        [HttpPost]
        [Route("Claim")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> AddClaim([FromBody] string role)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.GetUserId();
            var claim = new Claim(ClaimTypes.Role, role);

            var result = await userManager.AddClaimAsync(userId, claim);

            return Ok();
        }

        [HttpGet]
        [Route("Claim")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<object> GetClaim()
        {
            var identity = User.Identity as ClaimsIdentity;
            return identity.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
        }
    }
}

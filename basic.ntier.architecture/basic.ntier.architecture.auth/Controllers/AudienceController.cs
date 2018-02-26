namespace basic.ntier.architecture.auth.Controllers
{
    using System.Web.Http;
    using basic.ntier.architecture.auth.Entities;
    using basic.ntier.architecture.auth.Models;
    using basic.ntier.architecture.auth.Stores;

    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Audience newAudience = AudiencesStore.AddAudience(audienceModel.Name);

            return Ok<Audience>(newAudience);
        }
    }
}
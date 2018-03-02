namespace basic.ntier.architecture.auth.Controllers
{
    using System.Web.Http;
    using basic.ntier.architecture.auth.Entities;
    using basic.ntier.architecture.auth.Models;
    using basic.ntier.architecture.auth.Stores;

    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        private AudienceStore audienceStore;

        public AudienceController()
        {
            audienceStore = new AudienceStore();
        }

        [Route("")]
        public IHttpActionResult Post(AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Audience newAudience = audienceStore.AddAudience(audienceModel.Name);

            return Ok(newAudience);
        }
    }
}
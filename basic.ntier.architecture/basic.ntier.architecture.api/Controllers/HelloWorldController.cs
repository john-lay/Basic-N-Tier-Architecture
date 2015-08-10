using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using basic.ntier.architecture.business.HelloWorld;

namespace basic.ntier.architecture.api.Controllers
{
    [RoutePrefix("helloworld")]
    public class HelloWorldController : ApiController
    {
        private readonly IHelloWorldManager HelloWorld;

        public HelloWorldController(IHelloWorldManager HelloWorld)
        {
            this.HelloWorld = HelloWorld;
        }

        // GET: api/SayHello
        [HttpGet]
        [Route("sayhello")]
        public IHttpActionResult SayHello()
        {
            //return Ok(this.HelloWorld.SayHello());
            return Ok(this.HelloWorld.GetCustomer(1));
        }
    }
}

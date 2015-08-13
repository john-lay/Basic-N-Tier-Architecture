using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using basic.ntier.architecture.api.Models;
using basic.ntier.architecture.business.HelloWorld;
using basic.ntier.architecture.entity.Customer;

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
            var model = new CustomerModel
            {
                CustomerId = 1
            };

            var customer = Mapper.Map<CustomerModel, CustomerEntity>(model);

            return Ok(this.HelloWorld.GetCustomer(customer));
        }
    }
}

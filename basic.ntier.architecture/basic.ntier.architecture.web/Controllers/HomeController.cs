using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basic.ntier.architecture.business.HelloWorld;

namespace basic.ntier.architecture.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHelloWorldManager HelloWorld;

        public HomeController(IHelloWorldManager HelloWorld)
        {
            this.HelloWorld = HelloWorld;
        }

        public ActionResult Index()
        {
            ViewBag.Message = this.HelloWorld.SayHello();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
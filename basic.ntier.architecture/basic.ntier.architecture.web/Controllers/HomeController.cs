using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using basic.ntier.architecture.common.Abstract;

namespace basic.ntier.architecture.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHelloWorldEntity helloWorldEntity;

        public HomeController(IHelloWorldEntity helloWorldEntity)
        {
            this.helloWorldEntity = helloWorldEntity;
        }

        public ActionResult Index()
        {
            ViewBag.Message = this.helloWorldEntity.SayHello();

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
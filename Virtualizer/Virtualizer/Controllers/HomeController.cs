using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Virtualizer.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Message = "";
           
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Help page is not awailable";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Page contacts";

            return View();
        }

        [Authorize]
        public ActionResult Connections()
        {
            ViewBag.Message = "Not created yet";

            return View("Index");
        }
        [Authorize]
        public ActionResult BackPack()
        {
            ViewBag.Message = "Not created yet";

            return View("Index");
        }
    }
}
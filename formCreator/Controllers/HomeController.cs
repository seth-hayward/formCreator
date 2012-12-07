using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using formCreator.Models;

namespace formCreator.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {

            // load the character back we added in a previous Test Saving call
            var characters = RavenSession.Query<Form>();

            ViewBag.Message = String.Format("Loaded {0} Custom Forms", characters.Count());

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

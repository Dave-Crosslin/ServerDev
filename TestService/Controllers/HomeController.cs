using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TestService.Models;
using Newtonsoft.Json;

namespace TestService.Controllers
{
    public class HomeController : Controller
    {
	
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            return View();
        }
		
       public QueryHandler handler = new QueryHandler();

		public JsonResult Json()
        {
            List<string> spinnerNames = handler.spinnerFill();
            return Json(spinnerNames,JsonRequestBehavior.AllowGet);
		}
    }
}

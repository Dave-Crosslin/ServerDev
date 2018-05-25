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
		public static QueryHandler handler = new QueryHandler();

        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            return View();
        }

        public ActionResult ReturnJson(string query)
        {
            string commtext = JsonConvert.DeserializeObject(query).ToString();
            handler.ConnectionOpen();
            List<string> spinnerNames = handler.spinnerFill(commtext);
            return Json(spinnerNames);
		}
    }
}

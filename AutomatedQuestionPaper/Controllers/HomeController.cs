using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace AutomatedQuestionPaper.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            Session["Username"] = Session["Staff_Name"] = null;


            return View();
        }
    }
}
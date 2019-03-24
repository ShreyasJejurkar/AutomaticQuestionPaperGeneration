using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Office.Interop.Word;

namespace AutomatedQuestionPaper.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {

            List<int> unit1QuestionsList = new List<int>
            {
            
                10,
                2,8
               
            };


            var d = (from t in unit1QuestionsList where t < 10 select t).Take(2).ToList();

           














            Session["Username"] = Session["Staff_Name"] = null;
            return View();
        }
    }
}
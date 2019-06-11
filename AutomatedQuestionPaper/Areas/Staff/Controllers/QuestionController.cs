using AutomatedQuestionPaper.Controllers;
using System.Web.Mvc;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class QuestionController : AlertController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
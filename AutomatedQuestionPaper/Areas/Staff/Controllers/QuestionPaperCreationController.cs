using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class QuestionPaperCreationController : Controller
    {
        DatabaseContext _context = new DatabaseContext();

        public ActionResult Index()
        {
            var list = _context.ExamPapers.ToList();

            return View(list);
        }
    }
}
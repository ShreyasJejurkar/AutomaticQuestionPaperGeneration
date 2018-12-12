﻿using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class StaffHomePageController : Controller
    {
        /// <summary>
        /// EF context for database access
        /// </summary>
        private readonly DatabaseContext _context = new DatabaseContext();
        
        [HttpGet]
        public ActionResult Index()
        {
            var staffName = Session["Staff_Name"];

            // Get the staff details from database
            var staff = _context.Staffs.FirstOrDefault(u => u.Name == (string)staffName);

            return View(staff);
        }
    }
}